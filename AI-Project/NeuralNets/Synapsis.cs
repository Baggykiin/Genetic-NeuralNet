using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project.NeuralNets
{
    class Synapsis
    {
        public INeuronInput Input { get; private set; }
        public double Weight { get; set; }

        public Synapsis(INeuronInput input, double weight)
        {
            Input = input;
            Weight = weight;
        }

        public override string ToString()
        {
            return "Activation: " + Input.Output * Weight + " Weight: " + Weight + " Input: " + Input.Output;
        }
    }
}
