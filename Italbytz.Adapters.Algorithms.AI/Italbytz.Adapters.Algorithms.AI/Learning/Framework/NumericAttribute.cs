using System.Globalization;
using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Framework;

/// <inheritdoc cref="IAttribute" />
public class NumericAttribute : IAttribute
{
    private readonly IAttributeSpecification _spec;
    private readonly double _value;

    public NumericAttribute(double value, IAttributeSpecification spec)
    {
        _value = value;
        _spec = spec;
    }

    public string Name()
    {
        return _spec.AttributeName.Trim();
    }

    public string ValueAsString()
    {
        return _value.ToString(CultureInfo.InvariantCulture);
    }

    public double ValueAsDouble()
    {
        return _value;
    }
}