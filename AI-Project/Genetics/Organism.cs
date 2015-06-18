using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project.Genetics
{
    class Organism
    {
        public readonly List<Organism> Parents;
        public readonly Chromosome Chromosome;
        private double? fitness = null;

        public double Fitness
        {
            get
            {
                if (fitness == null)
                {
                    throw new Exception("Attempted to read fitness value when it has not been set.");
                }
                else
                {
                    return (double) fitness;
                }
            }
            set { fitness = value; }
    }

        public Organism(List<Organism> parents, Chromosome chromosome, double? fitness = null)
        {
            if (parents == null)
            {
                parents = new List<Organism>();
            }
            Parents = parents;
            Chromosome = chromosome;
            this.fitness = fitness;
        }

        /// <summary>
        /// AI mating may seem weird at first, because it's stripped down to the very core of exchanging genes.
        /// Because of this, mating always produces two children, each having the opposite gene from the other, 
        /// in such a way that where child 1 will have parent 2's gene, child 2 will have parent 1's gene instead,
        /// and vice versa. The only exception to this is when a mutation occurs.
        /// </summary>
        /// <param name="mate"></param>
        /// <returns></returns>
        public Tuple<Organism, Organism> Mate(Organism mate)
        {
            if (RNG.NextDouble() < GlobalParameters.CrossoverRate)
            {
                // Crossover point must be at least 1, otherwise no crossover occurs. This is not a problem per se,
                // but generally you'd want the crossover rate to not be dependent on chromosome length, because that would just
                // make it inconsistent. Instead, you'd probably want this rate to be constant and tweakable.
                // Because of this, whether crossover occurs is instead determined in the above if-statement.
                var crossoverPoint = RNG.Next(1, Chromosome.Genes.Count);
                var parent1Genes = Chromosome.Genes;
                var parent2Genes = mate.Chromosome.Genes;
                var chromosome1 = new Chromosome(parent1Genes.Take(crossoverPoint).Concat(parent2Genes.Skip(crossoverPoint)));
                var chromosome2 = new Chromosome(parent2Genes.Take(crossoverPoint).Concat(parent1Genes.Skip(crossoverPoint)));
                chromosome1 = chromosome1.Mutate();
                chromosome2 = chromosome2.Mutate();
                var child1 = new Organism(new List<Organism> { this, mate }, chromosome1);
                var child2 = new Organism(new List<Organism> { this, mate }, chromosome2);
                return new Tuple<Organism, Organism>(child1, child2);
            }
            else
            {
                // Crossover has occurred, and as such, no genes are exchanged.
                return new Tuple<Organism, Organism>(this.Duplicate(), mate.Duplicate());
            }
        }

        /// <summary>
        /// Creates a copy of this organism, setting this organism as its parent.
        /// </summary>
        /// <returns></returns>
        public Organism Duplicate()
        {
            return new Organism(new List<Organism> { this }, this.Chromosome);
        }

        /// <summary>
        /// Generates a 'left side' family tree, continuing up by following the  every time.
        /// </summary>
        /// <returns></returns>
        public List<Organism> GetLeftHistory(bool full = false)
        {
            var history = new List<Organism>();
            var currentOrganism = this;
            history.Add(currentOrganism);
            while (currentOrganism.Parents.Count != 0)
            {
                var nextOrganism = currentOrganism.Parents[0];
                if (full || currentOrganism.Chromosome != nextOrganism.Chromosome)
                {
                    history.Add(nextOrganism);
                }
                currentOrganism = nextOrganism;
            }
            return history;
        }

        public static Organism FromGenes(IEnumerable<double> genes, double fitness)
        {
            var chromosome = Chromosome.FromGenes(genes);
            return new Organism(null, chromosome, fitness);
        }

        /// <summary>
        /// Creates a new organism. This method should only be used when creating the first generation.
        /// All subsequent generations should be created through mating.
        /// </summary>
        /// <param name="geneCount">The amount of different values each gene can have</param>
        /// <param name="chromosomeLength">The number of genes in each chromosome</param>
        /// <param name="fitnessDecider">The fitness decider for this organism</param>
        /// <returns></returns>
        public static Organism Create(int chromosomeLength)
        {
            var chromosome = Chromosome.Create(chromosomeLength);
            return new Organism(null, chromosome);
        }

        public override string ToString()
        {
            return "Fitness: " + fitness;
        }

        public IEnumerable<double> ExtractGenes()
        {
            return Chromosome.Genes.Select(gene => gene.Genotype);
        }
    }
}
