using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeTransformer : ITransformer
{
    /*public DataViewSchema GetOutputSchema(DataViewSchema inputSchema)
    {
        // Define the output schema based on the input schema
        var schemaBuilder = new DataViewSchema.Builder();
        foreach (var column in inputSchema)
            schemaBuilder.AddColumn(column.Name, column.Type);
        return schemaBuilder.ToSchema();
    }

    public IDataView Transform(IDataView input)
    {
        // Implement the transformation logic here
        //return new DecisionTreeRowToRowMapper(_host, input);
        return null;
    }

    public bool IsRowToRowMapper => true;

    public IRowToRowMapper GetRowToRowMapper(DataViewSchema inputSchema)
    {
        return new DecisionTreeRowToRowMapper(_host, inputSchema);
    }

    public void Save(ModelSaveContext ctx)
    {
        // Implement saving logic if necessary
    }*/
    public void Save(ModelSaveContext ctx)
    {
        throw new NotImplementedException();
    }

    public DataViewSchema GetOutputSchema(DataViewSchema inputSchema)
    {
        throw new NotImplementedException();
    }

    public IDataView Transform(IDataView input)
    {
        throw new NotImplementedException();
    }

    public IRowToRowMapper GetRowToRowMapper(DataViewSchema inputSchema)
    {
        throw new NotImplementedException();
    }

    public bool IsRowToRowMapper { get; } = false;
}