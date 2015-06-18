namespace AI_Project
{
    // Tweakable parameters that determine how the simulation will run
    class GlobalParameters
    {
        // Genetics parameters
        public const double CrossoverRate = 0.7;        // * 100 %chance of chromosome crossover
        public const double MutationRate = 0.1;       // * 100 %chance of mutation
        public const int GenerationSize = 100;           // organisms in every generation
        // Neural Net parameters
        public const double ActivationResponse = 1.0;
        public const double Bias = -1;
        public const double SigmoidP = 1.0;
    }
}
