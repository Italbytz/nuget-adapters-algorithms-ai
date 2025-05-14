using Microsoft.ML.Data;

namespace Italbytz.AI.Tests.Data;

/// <summary>
///     model input class for Restaurant.
/// </summary>
public class RestaurantModelInputLegacy
{
    [LoadColumn(0)]
    [ColumnName(@"alternate")]
    public bool Alternate { get; set; }

    [LoadColumn(1)] [ColumnName(@"bar")] public bool Bar { get; set; }

    [LoadColumn(2)]
    [ColumnName(@"fri/sat")]
    public bool Fri_sat { get; set; }

    [LoadColumn(3)]
    [ColumnName(@"hungry")]
    public bool Hungry { get; set; }

    [LoadColumn(4)]
    [ColumnName(@"patrons")]
    public string Patrons { get; set; }

    [LoadColumn(5)] [ColumnName(@"price")] public string Price { get; set; }

    [LoadColumn(6)]
    [ColumnName(@"raining")]
    public bool Raining { get; set; }

    [LoadColumn(7)]
    [ColumnName(@"reservation")]
    public bool Reservation { get; set; }

    [LoadColumn(8)] [ColumnName(@"type")] public string Type { get; set; }

    [LoadColumn(9)]
    [ColumnName(@"wait_estimate")]
    public string Wait_estimate { get; set; }

    [LoadColumn(10)]
    [ColumnName(@"will_wait")]
    public float Will_wait { get; set; }
}