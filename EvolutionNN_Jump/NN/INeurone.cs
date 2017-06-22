using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN
{
    public interface INeurone
    {
        string Name { get; set; }
        float Value { get; set; }
        List<Connector> Connections { get; set; }

        void Set(float value);
        float Get();
        void Fire();
        void Mutate(float rate);
    }
}
