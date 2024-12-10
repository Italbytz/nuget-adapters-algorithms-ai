using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Framework;

public class StringAttribute : IAttribute
{
    private readonly IAttributeSpecification _spec;
    private readonly string _value;

    public StringAttribute(string value, StringAttributeSpecification spec)
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
        return _value.Trim();
    }
}