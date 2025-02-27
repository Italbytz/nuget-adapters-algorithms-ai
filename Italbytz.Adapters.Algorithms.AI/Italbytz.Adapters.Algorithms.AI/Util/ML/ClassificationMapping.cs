using Microsoft.ML.Data;

namespace Italbytz.Adapters.Algorithms.AI.Util.ML;

public class ClassificationMapping
{
    [ColumnName(@"Features")] public float[] Features { get; set; }

    [ColumnName(@"PredictedLabel")] public float PredictedLabel { get; set; }

    [ColumnName(@"Score")] public float[] Score { get; set; }
}