using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AI_Project.NeuralNets
{
    class NeuralNet
    {
        public List<NeuronLayer> Layers = new List<NeuronLayer>();
        public List<Input> InputLayer = new List<Input>();

        public NeuralNet(int inputLength)
        {
            for (var i = 0; i < inputLength; i++)
            {
                InputLayer.Add(new Input(0));
            }
        }

        public void FeedInput(IEnumerable<double> inputs)
        {
            if (inputs.Count() != InputLayer.Count)
            {
                throw new ArgumentException("Input enumerable length not euqal to Input Layer size.", "inputs");
            }
            var i = 0;
            foreach (var input in inputs)
            {
                InputLayer[i].InputValue = input;
                i++;
            }
            foreach (var neuron in Layers.SelectMany(layer => layer.Neurons))
            {
                neuron.InputChanged = true;
            }
        }
        public void CreateLayers(List<int> layerSizes)
        {
            for (int i = 0; i < layerSizes.Count; i++)
            {
                if (i == 0)
                {
                    Layers.Add(new NeuronLayer(layerSizes[i], InputLayer.Cast<INeuronInput>()));
                }
                else
                {
                    Layers.Add(new NeuronLayer(layerSizes[i], Layers[i-1].Neurons));
                }
            }
        }

        public IEnumerable<double> Run()
        {
            var lastLayer = Layers[Layers.Count - 1];
            var lastNeurons = lastLayer.Neurons;
            
            var outputs = lastNeurons.Select(neuron => neuron.Output);
            return outputs;
        }
    }

}
