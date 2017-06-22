using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN.Activation
{
    public static class Expressions
    {

        public static Random Random = new Random();

        public static Func<float, float> Sigmoid = (x) =>
        {
            return 1.0f / (1.0f + (float)Math.Exp(-x));
        };
        public static Func<float, float> Linear = (x) =>
        {
            return x;
        };
    }
}
