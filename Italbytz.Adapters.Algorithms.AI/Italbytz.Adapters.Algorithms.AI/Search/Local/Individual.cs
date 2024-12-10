using System.Collections.Generic;
using Italbytz.Ports.Algorithms.AI.Search.Local;

namespace Italbytz.Adapters.Algorithms.AI.Search.Local
{
    public class Individual<TAlphabet> : IIndividual<TAlphabet>
    {
        public Individual(List<TAlphabet> representation) =>
            Representation = representation;

        public List<TAlphabet> Representation { get; }
        public int Descendants { get; set; }
    }
}