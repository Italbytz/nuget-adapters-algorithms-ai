using System;
using Italbytz.AI.Search.EA.Operator;
using Italbytz.AI.Search.GP;
using Italbytz.AI.Search.GP.Individuals;

namespace Italbytz.AI.Search.EA.Graph;

public class OperatorGraph : IOperator
{
    private Finish finish;
    private Start start;

    public IIndividualList Process(IIndividualList individuals)
    {
        throw new NotImplementedException();
    }
}