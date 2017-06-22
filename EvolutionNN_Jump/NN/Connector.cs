using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN
{
    public class Connector
    {
        public INeurone Axon { get; set; }
        public INeurone Dendrite { get; set; }

        public float Weight { get; set; }

        public Connector(INeurone a, INeurone b, float weight)
        {
            Axon = a;
            Dendrite = b;

            Weight = weight;
        }

        public void Fire()
        {
            Dendrite.Set(Axon.Get() * Weight);
        }

    }
}
