using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_Project.Genetics
{
    class Chromosome
    {
        public readonly List<Gene> Genes;

        public Chromosome(IEnumerable<Gene> genes)
        {
            Genes = genes.ToList();
        }

        public static Chromosome Create(int length)
        {
            var genes = new Gene[length];
            for (int i = 0; i < length; i++)
            {
                genes[i] = new Gene(RNG.NextDouble());
            }
            return new Chromosome(genes);
        }

        public Chromosome Mutate()
        {
            var newGenes = new List<Gene>(Genes);
            for (int i = 0; i < Genes.Count; i++)
            {
                if (RNG.NextDouble() < GlobalParameters.MutationRate)
                {
                    newGenes[i] = Genes[i].Mutate();
                }
            }
            return new Chromosome(newGenes);
        }

        public static bool operator !=(Chromosome a, Chromosome b)
        {
            return ! (a == b);
        }

        public static bool operator ==(Chromosome a, Chromosome b)
        {
            return a.Genes.SequenceEqual(b.Genes);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            return string.Join(" ", Genes.Select(gene => gene.ToString()));
        }

        public static Chromosome FromGenes(IEnumerable<double> genes)
        {
            return new Chromosome(genes.Select(genotype => new Gene(genotype)));
        }
    }
}
