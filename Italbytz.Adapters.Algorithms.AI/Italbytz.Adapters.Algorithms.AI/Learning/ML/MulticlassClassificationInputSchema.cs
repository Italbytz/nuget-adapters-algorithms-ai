using Italbytz.Ports.Algorithms.AI.Learning.ML;

namespace Italbytz.Adapters.Algorithms.AI.Learning.ML;

/// <inheritdoc />
public class MulticlassClassificationInputSchema : ICustomMappingInputSchema
{
    /// <inheritdoc />
    public required float[] Features { get; set; }
}