using System.Collections.Generic;

namespace Italbytz.Adapters.Algorithms.AI.Learning.ML;

/// <summary>
///     Represents a mapping of keys to their corresponding values.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <param name="key"></param>
public class LookupMap<TKey>(TKey key)
{
    private TKey Key { get; } = key;

    /// <summary>
    ///     Gets the reverse mapping of the lookup data.
    /// </summary>
    /// <param name="lookupData">The lookup data.</param>
    /// <returns>Reverse mapping.</returns>
    public static Dictionary<int, string> KeyToValueMap(
        LookupMap<TKey>[] lookupData)
    {
        var lookupMap = new Dictionary<int, string>();
        for (var i = 0; i < lookupData.Length; i++)
            lookupMap[i + 1] = lookupData[i].Key?.ToString() ?? string.Empty;
        return lookupMap;
    }
}