using EvolutionNN_Jump.NN.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN
{
    public class OutputNeurone : INeurone
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public List<Connector> Connections { get; set; }

        public OutputNeurone()
        {
            Connections = new List<Connector>();
        }

        public void Fire()
        {
            Console.WriteLine(Get());
        }

        public float Get()
        {
            return Expressions.Sigmoid(Value);
        }

        public void Set(float value)
        {
            Value += value;
        }

        public void Mutate(float rate)
        {
            Console.WriteLine("No connections");
        }
    }
}