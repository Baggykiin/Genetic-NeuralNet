using System;

namespace AI_Project.NeuralNets
{
    public delegate void InputChangeEvent();

    interface INeuronInput
    {
        double Output { get; }
    }
}
