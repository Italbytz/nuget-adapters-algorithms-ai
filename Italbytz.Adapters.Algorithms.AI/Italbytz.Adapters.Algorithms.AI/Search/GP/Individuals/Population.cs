using System.Collections;
using System.Collections.Generic;
using Italbytz.AI.Search.EA.Individuals;
using Italbytz.AI.Util;

namespace Italbytz.AI.Search.GP.Individuals;

/// <inheritdoc cref="IIndividualList" />
public class Population : IIndividualList
{
    private readonly List<IIndividual> _individuals = [];

    public IIndividual this[int index] => _individuals[index];

    /// <inheritdoc />
    public void Add(IIndividual individual)
    {
        _individuals.Add(individual);
    }

    /// <inheritdoc />
    public IIndividual GetRandomIndividual()
    {
        return _individuals[
            ThreadSafeRandomNetCore.LocalRandom.Next(_individuals.Count)];
    }

    /// <inheritdoc />
    public IEnumerator<IIndividual> GetEnumerator()
    {
        return _individuals.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public List<IIndividual> ToList()
    {
        return [.._individuals];
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join("\n", _individuals);
    }
}