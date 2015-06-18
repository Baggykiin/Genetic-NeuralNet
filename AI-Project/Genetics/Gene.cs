namespace AI_Project.Genetics
{
    struct Gene
    {
        public readonly double Genotype;

        public Gene(double genotype)
        {
            Genotype = genotype;
        }

        public static double GetRandomGenotype()
        {
            return RNG.NextDouble()*2 - 1;
        }

        public Gene Mutate()
        {
            return new Gene(MathExtensions.RandomClamped());
        }

        public override string ToString()
        {
            return Genotype.ToString();
        }
    }
}
