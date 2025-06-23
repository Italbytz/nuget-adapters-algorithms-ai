﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Italbytz.Adapters.Algorithms.AI.Search.Agent;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.Problem;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.QSearch;
using Italbytz.Adapters.Algorithms.AI.Search.Informed;
using Italbytz.Adapters.Algorithms.Tests.Environment.Map;
using Italbytz.Ports.Algorithms.AI.Agent;
using Italbytz.Ports.Algorithms.AI.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Italbytz.Adapters.Algorithms.AI.Tests.Unit.Search.Informed
{
    public class GreedyBestFirstSearchTest
    {
        [Test]
        public void TestSimplifiedRoadMapOfRomaniaFromArad()
        {
            var romaniaMap = new SimplifiedRoadMapOfPartOfRomania();
            var search = new GreedyBestFirstSearch<string, MoveToAction>(
                new GraphSearch<string, MoveToAction>(),
                MapFunctions.CreateSLDHeuristicFunction(
                    SimplifiedRoadMapOfPartOfRomania.BUCHAREST, romaniaMap));
            var actions = TestSimplifiedRoadMapOfRomania(search, romaniaMap,
                SimplifiedRoadMapOfPartOfRomania.ARAD);

            Assert.Multiple(() =>
            {
                Assert.That(actions,
                    Is.EqualTo(
                        "MoveToAction[name=moveTo, location=Sibiu], MoveToAction[name=moveTo, location=Fagaras], MoveToAction[name=moveTo, location=Bucharest]"));
                Assert.That(
                    search.Metrics.Get(QueueSearch<string, MoveToAction>
                        .MetricPathCost), Is.EqualTo("450"));
                Assert.That(
                    search.Metrics.Get(QueueSearch<string, MoveToAction>
                        .MetricNodesExpanded), Is.EqualTo("3"));
            });
        }

        private static string TestSimplifiedRoadMapOfRomania(
        ISearchForActions<string, MoveToAction> search, IMap romaniaMap,
        string initialState)
        {
            var problem = new GeneralProblem<string, MoveToAction>(initialState,
                MapFunctions.CreateActionsFunction(romaniaMap),
                MapFunctions.GetResult, MapFunctions.TestGoal,
                MapFunctions.CreateDistanceStepCostFunction(romaniaMap));
            var agent = new SearchAgent<IPercept, string, MoveToAction>(problem,
                search);
            var actions = agent.Actions;
            return string.Join(", ", actions);
        }
    }
}
