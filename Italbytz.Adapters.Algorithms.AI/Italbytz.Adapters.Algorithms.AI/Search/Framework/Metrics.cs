// The original version of this file is part of <see href="https://github.com/aimacode/aima-java"/> which is released under
// MIT License
// Copyright (c) 2015 aima-java contributors

using System.Collections.Generic;
using System.Globalization;
using Italbytz.Ports.Algorithms.AI.Search;

namespace Italbytz.Adapters.Algorithms.AI.Search.Framework
{
    public class Metrics : IMetrics
    {
        private readonly Dictionary<string, string> _dict = new();

        public string Get(string name) => _dict[name];

        public void Set(string name, int i)
        {
            _dict[name] = i.ToString();
        }


        public void IncrementInt(string name)
        {
            Set(name, GetInt(name) + 1);
        }

        public int GetInt(string name)
        {
            var value = _dict[name];
            return int.Parse(value);
        }

        public void Set(string name, double d)
        {
            _dict[name] = d.ToString(CultureInfo.InvariantCulture);
        }

        public void Set(string name, long l)
        {
            _dict[name] = l.ToString();
        }

        public double GetDouble(string name)
        {
            var value = _dict[name];
            return double.Parse(value);
        }

        public long GetLong(string name)
        {
            var value = _dict[name];
            return long.Parse(value);
        }
    }
}