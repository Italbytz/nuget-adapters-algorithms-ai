using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeTransformer : ITransformer
{
    private readonly IHost _host;

    public DecisionTreeTransformer(IHostEnvironment env)
    {
        _host = env.Register(nameof(DecisionTreeTransformer));
    }

    public DataViewSchema GetOutputSchema(DataViewSchema inputSchema)
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
    }
}