using EvolutionNN_Jump.NN.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN
{
    public class WorkingNeurone : INeurone
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public List<Connector> Connections { get; set; }

        public WorkingNeurone()
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
            return Expressions.Sigmoid(Value);
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
