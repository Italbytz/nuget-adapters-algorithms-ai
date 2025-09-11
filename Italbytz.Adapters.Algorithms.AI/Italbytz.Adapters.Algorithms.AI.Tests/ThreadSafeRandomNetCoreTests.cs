namespace Italbytz.AI.Tests;

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

    [Test]
    public void TestMultipleThreadsWithFixedSeed()
    {
        ThreadSafeRandomNetCore.Seed = 42;
        var results = new List<int[]>();
        var tasks = new List<Task>();

        for (var t = 0; t < 5; t++)
            tasks.Add(Task.Run(() =>
            {
                var random = ThreadSafeRandomNetCore.LocalRandom;
                var numbers = new int[10];
                for (var i = 0; i < 10; i++) numbers[i] = random.Next(1, 100);
                lock (results)
                {
                    results.Add(numbers);
                }
            }));

        Task.WaitAll(tasks.ToArray());

        // Every thread should have its own independent random sequence
        for (var i = 0; i < results.Count; i++)
        for (var j = i + 1; j < results.Count; j++)
            Assert.That(results[i], Is.Not.EqualTo(results[j]));
    }

    [Test]
    public void TestMultipleThreadsWithDefaultSeed()
    {
        ThreadSafeRandomNetCore.Seed = null;
        var results = new List<int[]>();
        var tasks = new List<Task>();

        for (var t = 0; t < 5; t++)
            tasks.Add(Task.Run(() =>
            {
                var random = ThreadSafeRandomNetCore.LocalRandom;
                var numbers = new int[10];
                for (var i = 0; i < 10; i++) numbers[i] = random.Next(1, 100);
                lock (results)
                {
                    results.Add(numbers);
                }
            }));

        Task.WaitAll(tasks.ToArray());

        // Every thread should have its own independent random sequence
        for (var i = 0; i < results.Count; i++)
        for (var j = i + 1; j < results.Count; j++)
            Assert.That(results[i], Is.Not.EqualTo(results[j]));
    }

    [Test]
    public void TestNoRepeatingSequence()
    {
        ThreadSafeRandomNetCore.Seed = 42;
        var random = ThreadSafeRandomNetCore.LocalRandom;

        // Generate a large sequence of random numbers
        var numbers = new int[10000];
        for (var i = 0; i < numbers.Length; i++)
            numbers[i] = random.Next(1, 10);

        // Check for long sequences of the same number (e.g., more than 5 in a row)
        const int maxAllowedRepetitions = 10;

        for (var i = 0; i < numbers.Length - maxAllowedRepetitions; i++)
        {
            var allSame = true;
            for (var j = 1; j <= maxAllowedRepetitions; j++)
                if (numbers[i] != numbers[i + j])
                {
                    allSame = false;
                    break;
                }

            // If we found more than maxAllowedRepetitions in a row, the test fails
            Assert.That(allSame, Is.False,
                $"Found a sequence of more than {maxAllowedRepetitions} identical numbers starting at index {i}");
        }
    }
}