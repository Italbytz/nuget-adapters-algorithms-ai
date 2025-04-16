using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Italbytz.Adapters.Algorithms.AI.Learning.Framework;
using Italbytz.Ports.Algorithms.AI.Learning;
using Microsoft.ML;
using Microsoft.ML.Data;
using DataSet = Italbytz.Adapters.Algorithms.AI.Learning.Framework.DataSet;

namespace Italbytz.Adapters.Algorithms.AI.Util.ML;

/// <summary>
///     Extensions for <see cref="IDataView" />.
/// </summary>
/// <remarks>
///     This class contains extension methods for the <see cref="IDataView" />
///     interface.
///     It provides methods to convert an <see cref="IDataView" /> to a
///     <see cref="DataTable" />, save it as CSV,
///     and retrieve unique column entries.
/// </remarks>
public static class IDataViewExtensions
{
    /// <summary>
    ///     Converts an <see cref="IDataView" /> to a <see cref="DataTable" />.
    /// </summary>
    /// <returns>
    ///     A <see cref="DataTable" /> representing the data in the
    ///     <see cref="IDataView" />.
    /// </returns>
    /// <remarks>
    ///     This method creates a new <see cref="DataTable" /> and populates it with
    ///     the data from the <see cref="IDataView" />.
    ///     The columns of the <see cref="DataTable" /> are created based on the schema
    ///     of the <see cref="IDataView" />.
    ///     Each row in the <see cref="DataTable" /> corresponds to a row in the
    ///     <see cref="IDataView" />.
    ///     The method uses the <see cref="IDataView.Preview" /> method to get a
    ///     preview of the data and its schema.
    /// </remarks>
    public static DataTable? ToDataTable(this IDataView? dataView)
    {
        DataTable? dt = null;
        if (dataView == null) return dt;
        dt = new DataTable();
        var preview = dataView.Preview();
        dt.Columns.AddRange(preview.Schema
            .Select(x => new DataColumn(x.Name)).ToArray());
        foreach (var row in preview.RowView)
        {
            var r = dt.NewRow();
            foreach (var col in row.Values) r[col.Key] = col.Value;
            dt.Rows.Add(r);
        }

        return dt;
    }

    /// <summary>
    ///     Saves an <see cref="IDataView" /> as a CSV file.
    /// </summary>
    /// <param name="filePath">The file path where the CSV file will be saved.</param>
    /// <remarks>
    ///     This method uses the <see cref="MLContext.Data.SaveAsText" /> method to
    ///     save the <see cref="IDataView" /> as a CSV file.
    ///     The CSV file will be created at the specified file path.
    ///     The method uses a comma (',') as the separator character and does not
    ///     include the schema in the output.
    /// </remarks>
    public static void SaveAsCsv(
        this IDataView dataView,
        string filePath
    )
    {
        using var dataStream = new FileStream(
            filePath,
            FileMode.Create, FileAccess.Write);
        new MLContext().Data.SaveAsText(dataView, dataStream, ',',
            schema: false);
    }

    /// <summary>
    ///     Writes an <see cref="IDataView" /> to a CSV file.
    /// </summary>
    /// <param name="filePath">The file path where the CSV file will be saved.</param>
    /// <remarks>
    ///     This method creates a CSV file at the specified file path.
    ///     The CSV file will contain the data from the <see cref="IDataView" />.
    ///     The first line of the CSV file will contain the column names.
    ///     Each subsequent line will contain the values of a row in the
    ///     <see cref="IDataView" />.
    ///     The method uses a comma (',') as the separator character.
    /// </remarks>
    public static void WriteToCsv(
        this IDataView dataView,
        string filePath
    )
    {
        var dt = dataView.ToDataTable();

        var sb = new StringBuilder();

        Debug.Assert(dt != null, nameof(dt) + " != null");
        var columnNames = dt.Columns.Cast<DataColumn>()
            .Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in dt.Rows)
        {
            var fields =
                row.ItemArray.Select(InvariantCultureString);
            sb.AppendLine(string.Join(",", fields));
        }

        File.WriteAllText(filePath, sb.ToString());
    }

    private static string InvariantCultureString(object field)
    {
        var returnValue = field.GetType() switch
        {
            { } floatType when floatType == typeof(float) => ((float)field)
                .ToString(CultureInfo.InvariantCulture),
            { } doubleType when doubleType == typeof(double) => ((double)field)
                .ToString(CultureInfo.InvariantCulture),
            _ => field.ToString()
        };

        return returnValue;
    }

    /// <summary>
    ///     Gets the slot names of the features column in the <see cref="IDataView" />.
    /// </summary>
    /// <param name="columnName">
    ///     The name of the features column. Default is
    ///     "Features".
    /// </param>
    /// <returns>An array of slot names.</returns>
    /// <remarks>
    ///     This method retrieves the slot names from the features column of the
    ///     <see cref="IDataView" />.
    ///     The slot names are stored as annotations in the features column.
    ///     The method uses the <see cref="DataViewSchema.Column.Annotations" />
    ///     property to access the annotations.
    ///     If the features column does not exist or does not contain annotations, an
    ///     exception is thrown.
    ///     The slot names are returned as an array of
    ///     <see cref="ReadOnlyMemory{char}" />.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///     Thrown when the features column does not exist or does not contain
    ///     annotations.
    /// </exception>
    public static ImmutableArray<ReadOnlyMemory<char>> GetFeaturesSlotNames(
        this IDataView dataView,
        string columnName = "Features"
    )
    {
        var featuresColumn = dataView.Schema.GetColumnOrNull(columnName);
        if (featuresColumn == null)
            throw new ArgumentException(
                "The data view does not contain a column named 'Features'.");
        var featuresAnnotations = featuresColumn?.Annotations;
        if (featuresAnnotations == null)
            throw new ArgumentException(
                "The 'Features' column does not contain annotations.");
        VBuffer<ReadOnlyMemory<char>> slotNames = default;
        featuresAnnotations.GetValue("SlotNames", ref slotNames);
        return [..slotNames.GetValues()];
    }

    /// <summary>
    ///     Gets the unique entries in a specified column of an
    ///     <see cref="IDataView" />.
    /// </summary>
    /// <param name="columnName">The name of the column to retrieve the entries from.</param>
    /// <returns>An enumerable of unique entries in the specified column.</returns>
    /// <remarks>
    ///     This method retrieves the unique entries in a specified column of an
    ///     <see cref="IDataView" />.
    ///     The entries are stored as strings in the column.
    ///     The method uses the <see cref="GetColumnAsString" /> method to convert the
    ///     column data to strings.
    ///     The unique entries are returned as an enumerable of strings.
    /// </remarks>
    public static IEnumerable<string>? GetOrderedUniqueColumnEntries(
        this IDataView dataView,
        string columnName
    )
    {
        var labelColumn = dataView.Schema[columnName];
        var labelColumnData =
            (GetColumnAsString(dataView, labelColumn) ??
             throw new InvalidOperationException(
                 $"Column {columnName} can not be read as strings.")).ToList();
        return new HashSet<string>(
            labelColumnData).OrderBy(c => c);
    }

    /// <summary>
    ///     Gets the entries in a specified column of an <see cref="IDataView" /> as
    ///     strings.
    /// </summary>
    /// <param name="columnName">The name of the column to retrieve the entries from.</param>
    /// <returns>An enumerable of entries in the specified column as strings.</returns>
    /// <remarks>
    ///     This method retrieves the entries in a specified column of an
    ///     <see cref="IDataView" /> as strings.
    ///     The entries are converted to strings based on their type.
    ///     The method uses the <see cref="GetColumnAsString" /> method to convert the
    ///     column data to strings.
    ///     The entries are returned as an enumerable of strings.
    ///     The method handles different data types, including float, int, uint, char,
    ///     and string.
    ///     If the column type is not supported, it falls back to using the string
    ///     representation of the data.
    /// </remarks>
    public static IEnumerable<string>? GetColumnAsString(
        this IDataView dataView,
        string columnName
    )
    {
        var column = dataView.Schema[columnName];

        return GetColumnAsString(dataView, column);
    }

    /// <summary>
    ///     Gets the entries in a specified column of an <see cref="IDataView" /> as
    ///     strings.
    /// </summary>
    /// <param name="column">The column to retrieve the entries from.</param>
    /// <returns>An enumerable of entries in the specified column as strings.</returns>
    /// <remarks>
    ///     This method retrieves the entries in a specified column of an
    ///     <see cref="IDataView" /> as strings.
    ///     The entries are converted to strings based on their type.
    ///     The entries are returned as an enumerable of strings.
    ///     The method handles different data types, including float, int, uint, char,
    ///     and string.
    ///     If the column type is not supported, it falls back to using the string
    ///     representation of the data.
    /// </remarks>
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
            { } uintType when uintType == typeof(uint) => dataView
                .GetColumn<uint>(column)
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