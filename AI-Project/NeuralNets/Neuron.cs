using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project.NeuralNets
{
    class Neuron : INeuronInput
    {
        // Maps inputs to weights
        //public Dictionary<INeuronInput, double> Inputs = new Dictionary<INeuronInput, double>();
        public List<Synapsis> Inputs = new List<Synapsis>(); 


        private double? output = null;
        public double Output
        {
            get
            {
                // We have already stored a precalculated value, return it
                if (output != null) return (double) output;

                return CalculateOutput();
            }
        }
        public double CalculateOutput()
        {
            // We need to calculate the activation
            double activation = 0;
            for (var i = 0; i < Inputs.Count; i++)
            {
                var syn = Inputs[i];
                activation += (syn.Weight*syn.Input.Output);
            }
            // 0 for equal values of activation and threshold.
            // > 0 if activation > threshold
            // < 0 if activation < threshold
            var zeroCentered = activation + (Threshold * GlobalParameters.Bias);
            // Run through sigmoid function to ensure output is never greater than 1
            // and never smaller than 0
            output = MathExtensions.Sigmoid(zeroCentered);
            return (double)output;
        }

        public double Threshold { get; set; }

        public bool InputChanged
        {
            set { output = null; }
        }

        public Neuron(IEnumerable<INeuronInput> inputs)
        {
            foreach (var input in inputs)
            {
                Inputs.Add(new Synapsis(input, MathExtensions.RandomClamped()));
            }
            Threshold = MathExtensions.RandomClamped();
        }

        public override string ToString()
        {
            return "Threshold: " + Threshold + "Output: " + output;
        }
    }
}
