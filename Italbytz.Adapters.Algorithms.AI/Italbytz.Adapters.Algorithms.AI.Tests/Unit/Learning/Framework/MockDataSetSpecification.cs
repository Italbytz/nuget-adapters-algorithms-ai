using Italbytz.Adapters.Algorithms.AI.Learning.Framework;

namespace Italbytz.Adapters.Algorithms.Tests.Unit.Learning.Framework;

public class MockDataSetSpecification : DataSetSpecification
{
    public MockDataSetSpecification(string targetAttributeName)
    {
        TargetAttribute = targetAttributeName;
    }
}