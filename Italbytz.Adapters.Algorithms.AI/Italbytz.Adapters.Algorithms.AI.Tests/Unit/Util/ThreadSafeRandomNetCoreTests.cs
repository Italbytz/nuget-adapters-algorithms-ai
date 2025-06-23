using Italbytz.AI.Util;

namespace Italbytz.AI.Tests.Unit.Util;

public class ThreadSafeRandomNetCoreTests
{
    private static readonly int[] expected =
    [
        67, 14, 13, 52, 17, 26, 72, 51, 18, 76
    ];

    [Test]
    public void TestThreadSafeRandomFixedSeed()
    {
        ThreadSafeRandomNetCore.Seed = 42;
        var random = ThreadSafeRandomNetCore.LocalRandom;

        // Generate an array of 10 random numbers
        var numbers = new int[10];
        for (var i = 0; i < 10; i++) numbers[i] = random.Next(1, 100);

        // Assert that the numbers match the expected values when using seed 42
        Assert.That(numbers,
            Is.EqualTo(expected));
    }

    [Test]
    public void TestThreadSafeRandomDefaultSeed()
    {
        ThreadSafeRandomNetCore.Seed = null;
        var random = ThreadSafeRandomNetCore.LocalRandom;

        // Generate an array of 10 random numbers
        var numbers = new int[10];
        for (var i = 0; i < 10; i++) numbers[i] = random.Next(1, 100);

        // Assert that the numbers do not match the expected values when using default seed
        Assert.That(numbers,
            Is.Not.EqualTo(expected));
    }
}