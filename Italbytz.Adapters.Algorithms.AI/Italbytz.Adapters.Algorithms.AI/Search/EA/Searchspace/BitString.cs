using System;
using System.Collections;
using Italbytz.AI.Search.GP.Individuals;
using Italbytz.AI.Search.GP.SearchSpace;
using Italbytz.AI.Util;

namespace Italbytz.AI.Search.EA.Searchspace;

public class BitString : ISearchSpace
{
    public BitString(int dimension = 64)
    {
        Dimension = dimension;
    }

    public int Dimension { get; } = 64;

    public IGenotype GetRandomGenotype()
    {
        var bs = new BitArray(Dimension);
        var random = ThreadSafeRandomNetCore.LocalRandom;
        for (var i = 0; i < Dimension; i++) bs[i] = random.NextDouble() < 0.5;
        return new BitStringGenotype(bs, Dimension);
    }

    public IIndividualList GetAStartingPopulation()
    {
        throw new NotImplementedException();
    }
}