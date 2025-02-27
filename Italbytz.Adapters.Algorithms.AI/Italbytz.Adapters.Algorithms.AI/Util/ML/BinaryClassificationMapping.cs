using Microsoft.ML.Data;

namespace Italbytz.Adapters.Algorithms.AI.Util.ML;

public class BinaryClassificationMapping
{
    [ColumnName(@"Features")] public float[] Features { get; set; }

    [ColumnName(@"PredictedLabel")] public float PredictedLabel { get; set; }

    [ColumnName(@"Score")] public float Score { get; set; }

    [ColumnName(@"Probability")] public float Probability { get; set; }
}