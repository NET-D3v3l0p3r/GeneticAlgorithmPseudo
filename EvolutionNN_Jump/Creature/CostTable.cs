using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.Creature
{
    public static class CostTable
    {
        public static Dictionary<int, double> Costs = new Dictionary<int, double>();
        private static bool isInitialized;

        public static void Initialize()
        {

            if (isInitialized)
                return;
            isInitialized = true;

            Costs.Add(1, -1);
            Costs.Add(2, 5);
            Costs.Add(-1, -200.0);
            Costs.Add(3, 150);
            Costs.Add(0, 0);
        }
    }
}
