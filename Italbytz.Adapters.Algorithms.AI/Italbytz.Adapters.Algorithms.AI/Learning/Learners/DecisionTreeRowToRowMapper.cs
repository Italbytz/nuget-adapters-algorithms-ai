using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Learners;

public class DecisionTreeRowToRowMapper : IRowToRowMapper
{
    private readonly IHost _host;

    public DecisionTreeRowToRowMapper(IHost host, DataViewSchema inputSchema)
    {
        _host = host;
        InputSchema = inputSchema;
    }

    public IEnumerable<DataViewSchema.Column> GetDependencies(
        IEnumerable<DataViewSchema.Column> dependingColumns)
    {
        throw new NotImplementedException();
    }

    public DataViewRow GetRow(DataViewRow input,
        IEnumerable<DataViewSchema.Column> activeColumns)
    {
        throw new NotImplementedException();
    }

    public DataViewSchema InputSchema { get; }

    public DataViewSchema OutputSchema => InputSchema;

    public DataViewRow GetRow(DataViewRow input, Func<int, bool> activeOutput)
    {
        return new DecisionTreeRow(input);
    }

    private class DecisionTreeRow : DataViewRow
    {
        private readonly DataViewRow _input;

        public DecisionTreeRow(DataViewRow input)
        {
            _input = input;
        }

        public override DataViewSchema Schema => _input.Schema;

        public override long Position => _input.Position;

        public override long Batch => _input.Batch;

        public override ValueGetter<TValue> GetGetter<TValue>(
            DataViewSchema.Column column)
        {
            var getter = _input.GetGetter<TValue>(column);
            return (ref TValue value) =>
            {
                getter(ref value);
                // Apply custom transformation logic here
            };
        }

        public override ValueGetter<DataViewRowId> GetIdGetter()
        {
            throw new NotImplementedException();
        }

        public override bool IsColumnActive(DataViewSchema.Column column)
        {
            return _input.IsColumnActive(column);
        }
    }
}