namespace AI_Project.NeuralNets
{
    class Input : INeuronInput
    {
        public double Output { get; private set; }

        public double InputValue
        {
            set
            {
                Output = value;
            }
        }

        public Input(double input)
        {
            this.Output = input;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
