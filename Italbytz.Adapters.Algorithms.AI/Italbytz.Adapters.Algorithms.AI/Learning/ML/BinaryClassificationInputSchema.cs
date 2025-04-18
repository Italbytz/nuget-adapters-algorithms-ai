using Italbytz.Ports.Algorithms.AI.Learning.ML;

namespace Italbytz.Adapters.Algorithms.AI.Learning.ML;

/// <inheritdoc cref="ICustomMappingInputSchema" />
public class BinaryClassificationInputSchema : ICustomMappingInputSchema
{
    /// <inheritdoc />
    public float[] Features { get; set; }
}