using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mcts.normalizer
{
    public interface INormalizer
    {
        double Normalize(double value);
    }

    public struct Linear : INormalizer
    {
        public double Normalize(double value)
        {
            return value;
        }
    }

    public struct ApproxSigmoid : INormalizer
    {
        public double Normalize(double value)
        {
            return ((value / (1.0 + Math.Abs(value))) + 1.0) * 0.5;
        }
    }

    public struct Sigmoid : INormalizer
    {
        public double Normalize(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }
    }
}
