namespace Italbytz.Adapters.Algorithms.AI.Util.ML;

public class BinaryClassificationMapping
{
    public float[] Features { get; set; }
    public float PredictedLabel { get; set; }
    public float Score { get; set; }
    public float Probability { get; set; }
}