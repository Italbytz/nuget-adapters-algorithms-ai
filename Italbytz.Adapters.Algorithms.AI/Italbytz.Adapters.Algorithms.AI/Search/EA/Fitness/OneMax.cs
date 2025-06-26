using System.Linq;
using Italbytz.AI.Search.EA.Searchspace;
using Italbytz.AI.Search.GP.Fitness;
using Italbytz.AI.Search.GP.Individuals;
using Microsoft.ML;

namespace Italbytz.AI.Search.EA.Fitness;

public class OneMax : IStaticSingleObjectiveFitnessFunction
{
    public double[] Evaluate(IIndividual individual, IDataView data)
    {
        var result = ((BitStringGenotype)individual.Genotype).BitArray;
        var count = result.Cast<bool>().Count(bit => bit);
        return [count];
    }

    public int NumberOfObjectives { get; }
    public string LabelColumnName { get; set; }
}