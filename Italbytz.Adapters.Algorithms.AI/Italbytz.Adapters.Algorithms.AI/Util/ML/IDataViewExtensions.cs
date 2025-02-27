using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Italbytz.Adapters.Algorithms.AI.Learning.Framework;
using Italbytz.Ports.Algorithms.AI.Learning;
using Microsoft.ML;
using Microsoft.ML.Data;
using DataSet = Italbytz.Adapters.Algorithms.AI.Learning.Framework.DataSet;

namespace Italbytz.Adapters.Algorithms.AI.Util.ML;

public static class IDataViewExtensions
{
    public static DataTable ConvertToDataTable(this IDataView dataView)
    {
        var dataTable = new DataTable();
        var preview = dataView.Preview();

        // Add columns to DataTable
        foreach (var column in preview.Schema)
            dataTable.Columns.Add(column.Name, column.Type.RawType);

        // Add rows to DataTable
        foreach (var row in preview.RowView)
        {
            var dataRow = dataTable.NewRow();
            foreach (var column in preview.Schema)
                dataRow[column.Name] = row.Values[column.Index].Value;
            dataTable.Rows.Add(dataRow);
        }

        return dataTable;
    }

    public static IEnumerable<string>? GetColumnAsString(
        this IDataView dataView,
        string columnName
    )
    {
        var column = dataView.Schema[columnName];

        return dataView.GetColumnAsString(column);
    }

    public static IEnumerable<string>? GetColumnAsString(
        this IDataView dataView, DataViewSchema.Column column)
    {
        var dataColumn = column.Type.RawType switch
        {
            { } floatType when floatType == typeof(float) => dataView
                .GetColumn<float>(column)
                .Select(entry => entry.ToString(CultureInfo.InvariantCulture)),
            { } intType when intType == typeof(int) => dataView
                .GetColumn<int>(column)
                .Select(entry => entry.ToString()),
            { } charType when charType == typeof(char) => dataView
                .GetColumn<char>(column)
                .Select(entry => entry.ToString()),
            { } stringType when stringType == typeof(string) => dataView
                .GetColumn<string>(column),
            _ => dataView
                .GetColumn<string>(column)
        };
        return dataColumn;
    }

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