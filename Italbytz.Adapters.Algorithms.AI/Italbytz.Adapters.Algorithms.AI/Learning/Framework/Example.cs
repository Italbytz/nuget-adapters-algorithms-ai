using System.Collections.Generic;
using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Framework;

/// <inheritdoc cref="IExample" />
public class Example : IExample
{
    private readonly IAttribute _targetAttribute;

    public Example(Dictionary<string, IAttribute> attributes,
        IAttribute targetAttribute)
    {
        Attributes = attributes;
        _targetAttribute = targetAttribute;
    }

    public Dictionary<string, IAttribute> Attributes { get; }

    public string TargetValue()
    {
        return GetAttributeValueAsString(_targetAttribute.Name());
    }

    public string GetAttributeValueAsString(string attributeName)
    {
        return Attributes[attributeName].ValueAsString();
    }
}