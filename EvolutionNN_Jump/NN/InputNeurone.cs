using EvolutionNN_Jump.NN.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN
{
    public class InputNeurone : INeurone
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public List<Connector> Connections { get; set; }

        public InputNeurone()
        {
            Connections = new List<Connector>();
        }

        public void Fire()
        {
            foreach (var connection in Connections)
                connection.Fire();
        }

        public float Get()
        {
            return Expressions.Linear(Value);
        }

        public void Set(float value)
        {
            Value += value;
        }

        public void Mutate(float rate)
        {
            int index = Expressions.Random.Next(0, Connections.Count);
            Connections[index].Weight += (float)Expressions.Random.NextDouble() * 2 * rate - rate;
        }
    }
}
