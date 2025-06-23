using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Italbytz.Adapters.Algorithms.AI.Search.Framework;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.QSearch;
using Italbytz.Ports.Algorithms.AI.Problem;
using Italbytz.Ports.Algorithms.AI.Search;
using Italbytz.Ports.Algorithms.AI.Search.Informed;

namespace Italbytz.Adapters.Algorithms.AI.Search.Informed
{
    public class RecursiveBestFirstSearch<TState, TAction> : ISearchForActions<TState, TAction>
    {
        private bool avoidLoops;
        private readonly Func<INode<TState, TAction>, double> _evalFn;
        private readonly INodeFactory<TState, TAction> _nodeFactory;
        private HashSet<TState> explored = new HashSet<TState>();


        public static String METRIC_NODES_EXPANDED = "nodesExpanded";
        public static String METRIC_MAX_RECURSIVE_DEPTH = "maxRecursiveDepth";
        public static String METRIC_PATH_COST = "pathCost";

        public RecursiveBestFirstSearch(Func<INode<TState, TAction>, double> evalFn) : this(evalFn, false)
        {
        }
        public RecursiveBestFirstSearch(Func<INode<TState, TAction>, double> evalFn, bool avoidloops) : this(evalFn, avoidloops, new NodeFactory<TState, TAction>())
        {
        }
        public RecursiveBestFirstSearch(Func<INode<TState, TAction>, double> evalFn, bool avoidloops, NodeFactory<TState, TAction> nodeFactory) 
        {
            avoidLoops = avoidloops;
            _nodeFactory = nodeFactory;
            _evalFn = CreateEvalFn(evalFn);
            AddNodeListener(node => Metrics.IncrementInt(METRIC_NODES_EXPANDED));
            metrics = new Metrics();
        }

        public void AddNodeListener(Action<INode<TState, TAction>> listener)
        {
            _nodeFactory.AddNodeListener(listener);
        }

        private IMetrics metrics;

        public IMetrics Metrics
        {
            get { return metrics; }
            set { metrics = value; }
        }

        private class SearchResult<TState, TAction>
        {
            private INode<TState, TAction> solutionNode;
            private double fCostLimit;

            public SearchResult(INode<TState, TAction> sNode, double fCLimit) 
            {
                solutionNode = sNode;
                fCostLimit = fCLimit;
            }

            public bool hasSolution() 
            {
                return solutionNode != null;
            }

            public INode<TState, TAction> getSolutionNode()
            {
                return solutionNode;
            }

            public double getFCostLimit() 
            {
                return fCostLimit;
            }
        }

        private SearchResult<TState, TAction> rbfs(IProblem<TState, TAction> problem, INode<TState, TAction> node, double node_f, double fCostLimit, int recursiveDepth) 
        {
            updateMetrics(recursiveDepth);

            if (problem.TestSolution(node))
            {
                return getResult(null, node, fCostLimit);
            }

            List<INode<TState, TAction>> successors = expandNode(node, problem);

            if (successors.Count == 0 || successors == null)
            {
                return getResult(node, null, double.MaxValue);
            }

            double[] f = new double[successors.Count];

            for (int i = 0; i < successors.Count; i++)
            {
                f[i] = Math.Max(_evalFn(successors.ElementAt(i)), node_f);
            }

            while (true)
            {
                int bestIndex = getBestFValueInde(f);
                if (f[bestIndex] > fCostLimit)
                {
                    return getResult(node, null, f[bestIndex]);
                }
                int altIndex = getNextBestFValueIndex(f, bestIndex);
                SearchResult<TState, TAction> sResult = rbfs(problem, successors.ElementAt(bestIndex), f[bestIndex], Math.Min(fCostLimit, f[altIndex]), recursiveDepth +1);
                f[bestIndex] = sResult.getFCostLimit();
                if (sResult.hasSolution())
                {
                    return getResult(node, sResult.getSolutionNode(), sResult.getFCostLimit());
                }
            }
        }

        private void updateMetrics(int recursiveDepth)
        {
            int maxRecusriveDepth = metrics.GetInt(METRIC_MAX_RECURSIVE_DEPTH);
            if (recursiveDepth > maxRecusriveDepth)
            {
                metrics.Set(METRIC_MAX_RECURSIVE_DEPTH, recursiveDepth);
            }
        }

        private void clearMetrics() 
        {
            metrics.Set(METRIC_PATH_COST, 0);
            metrics.Set(METRIC_NODES_EXPANDED, 0);
            metrics.Set(METRIC_MAX_RECURSIVE_DEPTH, 0);
        }

        private int getNextBestFValueIndex(double[] f, int bestIndex)
        {
            int lidx = bestIndex;
            double lowestSoFar = double.MaxValue;
            for (int i = 0; i < f.Length; i++)
            {
                if (i != bestIndex && f[i] < lowestSoFar)
                {
                    lowestSoFar = f[i];
                    lidx = i;
                }
            }
            return lidx;
        }

        private int getBestFValueInde(double[] f)
        {
            int lidx = 0;
            double lowestSoFar = double.MaxValue;
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] < lowestSoFar)
                {
                    lowestSoFar = f[i];
                    lidx = i;
                }
            }
            return lidx;
        }

        private List<INode<TState, TAction>> expandNode(INode<TState, TAction> node , IProblem<TState, TAction> problem)
        {
            List<INode<TState, TAction>> res = _nodeFactory.GetSuccessors(node, problem);
            if (avoidLoops)
            {
                explored.Add(node.State);
                res = res.Where(n => !explored.Contains(n.State)).ToList();
            }
            return res;
        }

        private SearchResult<TState,TAction> getResult(INode<TState, TAction> currentNode, INode<TState, TAction> solutionNode, double fCostLimit) 
        {
            if (avoidLoops && currentNode != null)
            {
                explored.Remove(currentNode.State);
            }
            return new SearchResult<TState, TAction>(solutionNode, fCostLimit);
        }

        public IEnumerable<TAction>? FindActions(IProblem<TState, TAction> problem)
        {
            explored.Clear();
            clearMetrics();
            INode<TState, TAction> node = _nodeFactory.CreateNode(problem.InitialState);
            SearchResult<TState, TAction> searchresult = rbfs(problem, node, _evalFn(node), double.MaxValue, 0);
            if (searchresult.hasSolution())
            {
                INode<TState, TAction> solution = searchresult.getSolutionNode();
                metrics.Set(METRIC_PATH_COST, solution.PathCost);
                return SearchUtils.ToActions(solution);
            }
            return new LinkedList<TAction>();
        }

        private static Func<INode<TState, TAction>, double> CreateEvalFn(Func<INode<TState, TAction>, double> heuristicFn)
        {
            return node => node.PathCost + heuristicFn(node);
        }
    }
}
