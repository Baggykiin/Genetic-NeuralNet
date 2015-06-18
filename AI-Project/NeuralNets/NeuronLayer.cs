using System.Collections.Generic;

namespace AI_Project.NeuralNets
{
    class NeuronLayer
    {
        public List<Neuron> Neurons = new List<Neuron>();

        public NeuronLayer(int numNeurons, IEnumerable<INeuronInput> inputs)
        {
            for (int i = 0; i < numNeurons; i++)
            {
                Neurons.Add(new Neuron(inputs));
            }
        }

        
    }
}
