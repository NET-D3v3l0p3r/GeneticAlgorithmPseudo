using EvolutionNN_Jump.NN.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionNN_Jump.NN
{
    public class NeuralNetwork
    {
        public InputNeurone[] L0 { get; set; }
        public WorkingNeurone[] L1 { get; set; }
        public OutputNeurone[] L2 { get; set; }

        public List<INeurone[]> FullNeurones { get; set; }

        public NeuralNetwork()
        {
            FullNeurones = new List<INeurone[]>();
        }

        public void CreateInputLayer(int amount, double bias)
        {
            L0 = new InputNeurone[amount + 1];

            for (int i = 0; i < L0.Length; i++)
                L0[i] = new InputNeurone();
            
            L0[0].Set((float)bias);

            FullNeurones.Add(L0);
        }
        public void CreateHiddenLayer(int amount, double bias)
        {
            L1 = new WorkingNeurone[amount + 1];
            for (int i = 0; i < L1.Length; i++)
                L1[i] = new WorkingNeurone();

            L1[0].Set((float)bias);

            FullNeurones.Add(L1);
        }
        public void CreateOutputLayer(int amount)
        {
            L2 = new OutputNeurone[amount];
            for (int i = 0; i < L2.Length; i++)
                L2[i] = new OutputNeurone();

            FullNeurones.Add(L2);
        }

        public void CreateMesh(INeurone[] l0_connections = null, INeurone[] l1_connections = null)
        {

            if (l0_connections != null)
            {
                for (int i = 0; i < L0.Length; i++)
                {
                    for (int j = 1; j < L1.Length - 1; j++)
                    {
                        L0[i].Connections.Add(new Connector(L0[i], L1[j], l0_connections[i].Connections[j - 1].Weight));
                    }
                }
            }
            else
            {
                for (int i = 0; i < L0.Length; i++)
                {
                    for (int j = 1; j < L1.Length - 1; j++)
                    {
                        L0[i].Connections.Add(new Connector(L0[i], L1[j], (float)(Expressions.Random.NextDouble() * 2) - 1.0f));
                    }
                }
            }

            if (l1_connections != null)
            {
                for (int i = 0; i < L1.Length; i++)
                {
                    for (int j = 0; j < L2.Length; j++)
                    {
                        L1[i].Connections.Add(new Connector(L1[i], L2[j], l1_connections[i].Connections[j].Weight));
                    }
                }
            }
            else
            {
                for (int i = 0; i < L1.Length; i++)
                {
                    for (int j = 0; j < L2.Length; j++)
                    {
                        L1[i].Connections.Add(new Connector(L1[i], L2[j], (float)(Expressions.Random.NextDouble() * 2) - 1.0f));
                    }
                }
            }
        }

        public float[] StimulateL0(float[] inputs)
        {
            if (L0.Length - 1 != inputs.Length)
                throw new Exception();

            for (int i = 1; i < L0.Length - 1; i++)
                L0[i].Set(inputs[i - 1]);

            foreach (var input in L0)
                input.Fire();
            foreach (var hidden in L1)
                hidden.Fire();

            float[] outputs = new float[L2.Length];
            for (int i = 0; i < L2.Length; i++)
                outputs[i] = L2[i].Get();

            return outputs;
        }

  

        public List<INeurone[]> CloneLayers()
        {
            return MutateNeuralNetwork(Expressions.Random.Next(10, 15), (float)(Expressions.Random.NextDouble()) * 0.1f);
        }

        public List<INeurone[]> MutateNeuralNetwork(int amount, float rate)
        {
            int layer = Expressions.Random.Next(0, FullNeurones.Count - 1);
            int index = Expressions.Random.Next(0, FullNeurones[layer].Length);

            for (int i = 0; i < amount; i++)
                FullNeurones[layer][index].Mutate(rate);

            return FullNeurones;

        }
    }
}
