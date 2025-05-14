using Italbytz.Adapters.Algorithms.AI.Learning.Inductive;
using Italbytz.Adapters.Algorithms.AI.Learning.Learners;
using Italbytz.Adapters.Algorithms.Tests.Unit.Learning.Framework;

namespace Italbytz.Adapters.Algorithms.AI.Tests.Unit.Learning.Inductive;

public class CrossValidationWrapperTest
{
    [Test]
    public void CrossValidationWrapperTestCase()
    {
        var validation = new CrossValidation(0.05);
        // This learner gives least error when size param is 70
        var result = validation.CrossValidationWrapper(
            new SampleParameterizedLearner(), 5,
            TestDataSetFactory.GetRestaurantDataSet());
        Assert.That(result.ParameterSize, Is.EqualTo(70));
    }
}