using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using AI_Project.NeuralNets;

namespace AI_Project
{
    static class ExtensionMethods
    {
        public static List<double> ExtractChromosome(this NeuralNet neuralNet)
        {
            var genes = new List<double>();
            foreach (var neuron in neuralNet.Layers.SelectMany(layer => layer.Neurons))
            {
                genes.AddRange(neuron.Inputs.Select(synapsis => synapsis.Weight));
                genes.Add(neuron.Threshold);
            }
            return genes;
        }

        public static float ElapsedMsAccurate(this Stopwatch stopwatch)
        {
            return stopwatch.CalculateTicks(stopwatch.ElapsedTicks);
        }

        public static float CalculateTicks(this Stopwatch stopwatch, long ticks)
        {
            return ticks/(float) Stopwatch.Frequency*1000;
        }

        public static void InsertChromosome(this NeuralNet neuralNet, IEnumerable<double> genesIe)
        {
            var genes = new List<double>(genesIe);
            foreach (var neuron in neuralNet.Layers.SelectMany(layer => layer.Neurons))
            {
                foreach (var input in neuron.Inputs)
                {
                    input.Weight = genes[0];
                    genes.RemoveAt(0);
                }
                neuron.Threshold = genes[0];
                genes.RemoveAt(0);
            }
        }
    }
}
