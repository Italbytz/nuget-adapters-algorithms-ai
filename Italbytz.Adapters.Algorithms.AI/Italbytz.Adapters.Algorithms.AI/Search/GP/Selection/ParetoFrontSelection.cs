using System.Linq;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.GP.Selection;

/// <inheritdoc cref="ISelection" />
public class ParetoFrontSelection : ISelection
{
    /// <inheritdoc />
    public IIndividualList Process(IIndividualList individuals)
    {
        var individualList = individuals.ToList();
        var maxGeneration =
            individualList.Max(individual => individual.Generation);
        var i = 0;
        while (i < individualList.Count)
        {
            var individual = individualList[i];
            if (individual.Generation < maxGeneration) break;
            var j = i + 1;
            while (j < individualList.Count)
            {
                var otherIndividual = individualList[j];
                if (individual.IsDominating(otherIndividual))
                {
                    individualList.RemoveAt(j);
                }
                else if (otherIndividual.IsDominating(individual))
                {
                    individualList.RemoveAt(i);
                    i--;
                    break;
                }
                else
                {
                    j++;
                }
            }

            i++;
        }

        var population = new Population();
        foreach (var individual in individualList) population.Add(individual);
        return population;
    }

    public int Size { get; set; }
}