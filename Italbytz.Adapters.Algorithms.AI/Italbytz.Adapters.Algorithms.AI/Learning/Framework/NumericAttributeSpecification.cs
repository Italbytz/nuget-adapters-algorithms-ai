using System;
using Italbytz.Ports.Algorithms.AI.Learning;

namespace Italbytz.Adapters.Algorithms.AI.Learning.Framework;

/// <inheritdoc cref="IAttributeSpecification" />
public class NumericAttributeSpecification : IAttributeSpecification
{
    public NumericAttributeSpecification(string attributeName)
    {
        AttributeName = attributeName;
    }

    public IAttribute CreateAttribute(string rawValue)
    {
        return new NumericAttribute(double.Parse(rawValue), this);
    }


    public bool IsValid(string value)
    {
        try
        {
            double.Parse(value);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string AttributeName { get; }
}