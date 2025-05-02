using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Italbytz.Adapters.Algorithms.AI.Learning.Framework;
using Italbytz.ML;
using Italbytz.Ports.Algorithms.AI.Learning;
using Microsoft.ML;

namespace Italbytz.Adapters.Algorithms.AI.Util.ML;

/// <summary>
///     Extensions for <see cref="IDataView" />.
/// </summary>
public static class IDataViewExtensions
{
    /// <summary>
    ///     Gets the data set specification for the <see cref="IDataView" />.
    /// </summary>
    public static IDataSetSpecification GetDataSetSpecification(
        this IDataView dataView, string target)
    {
        var dss = new DataSetSpecification();
        foreach (var column in dataView.Schema)
            if (column.Type.RawType == typeof(bool))
            {
                dss.DefineStringAttribute(column.Name, Util.Yesno());
            }
            else
            {
                var values = dataView
                    .GetColumnAsString(column);
                var uniqueValues =
                    new HashSet<string>(
                        values).ToArray();
                dss.DefineStringAttribute(column.Name, uniqueValues);
            }

        dss.TargetAttribute = target;
        return dss;
    }

    /// <summary>
    ///     Converts an <see cref="IDataView" /> to a <see cref="DataSet" />.
    /// </summary>
    /// <param name="target">The name of the target attribute.</param>
    /// <param name="spec">The data set specification.</param>
    /// <returns>
    ///     A <see cref="DataSet" /> representing the data in the
    ///     <see cref="IDataView" />.
    /// </returns>
    /// <remarks>
    ///     This method creates a new <see cref="DataSet" /> and populates it with
    ///     the data from the <see cref="IDataView" />.
    /// </remarks>
    public static IDataSet AsDataSet(this IDataView dataView, string target,
        IDataSetSpecification spec)
    {
        var dataSet = new DataSet(spec);

        using var cursor = dataView.GetRowCursor(dataView.Schema);
        while (cursor.MoveNext())
        {
            Dictionary<string, IAttribute> attributes = new();
            var targetValue = "";
            foreach (var column in dataView.Schema)
            {
                var columnSpecification =
                    spec.GetAttributeSpecFor(column.Name);

                var value = "";
                switch (column.Type.RawType)
                {
                    case { } floatType when floatType == typeof(float):
                        float floatValue = 0;
                        cursor.GetGetter<float>(column)
                            .Invoke(ref floatValue);
                        value = floatValue.ToString(CultureInfo
                            .InvariantCulture);
                        break;
                    case { } intType when intType == typeof(int):
                        var intValue = 0;
                        cursor.GetGetter<int>(column).Invoke(ref intValue);
                        value = intValue.ToString();
                        break;
                    case { } charType when charType == typeof(char):
                        var charValue = '\0';
                        cursor.GetGetter<char>(column)
                            .Invoke(ref charValue);
                        value = charValue.ToString();
                        break;
                    case { } boolType when boolType == typeof(bool):
                        var boolValue = false;
                        cursor.GetGetter<bool>(column)
                            .Invoke(ref boolValue);
                        value = boolValue ? Util.Yes : Util.No;
                        break;
                    case { } stringType when stringType == typeof(string):
                        cursor.GetGetter<string>(column)
                            .Invoke(ref value);
                        break;
                    case { } romCharType when romCharType ==
                                              typeof(ReadOnlyMemory<char>):
                        ReadOnlyMemory<char> romCharValue = default;
                        cursor.GetGetter<ReadOnlyMemory<char>>(column)
                            .Invoke(ref romCharValue);
                        value = romCharValue.ToString();
                        break;
                    default:
                        ReadOnlyMemory<char> romValue = default;
                        cursor.GetGetter<ReadOnlyMemory<char>>(column)
                            .Invoke(ref romValue);
                        value = romValue.ToString();
                        break;
                }

                attributes.Add(column.Name,
                    new StringAttribute(value, columnSpecification));

                if (column.Name == target) targetValue = value;
            }

            var example = new Example(attributes,
                new StringAttribute(targetValue,
                    spec.GetAttributeSpecFor(target)));
            dataSet.Examples.Add(example);
        }

        return dataSet;
    }
}