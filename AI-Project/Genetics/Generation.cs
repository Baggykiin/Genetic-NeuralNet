using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project.Genetics
{
    internal class Generation : List<Organism>
    {
        public override string ToString()
        {
            var avg = this.Average(org => org.Fitness);
            return "Average Fitness: " + avg;
        }

        public Organism RouletteWheelSelect()
        {
            var total = this.Sum(organism => organism.Fitness);
            var selection = RNG.NextDouble(total);

            using (var enumerator = this.GetEnumerator())
            {
                enumerator.MoveNext();
                while (selection > enumerator.Current.Fitness)
                {
                    selection -= enumerator.Current.Fitness;
                    enumerator.MoveNext();
                }
                return enumerator.Current;
            }
        }

        public Tuple<Organism, Organism> Mate()
        {
            var parent1 = RouletteWheelSelect();
            var parent2 = RouletteWheelSelect();
            return parent1.Mate(parent2);
        }

        public Generation CreateNextGeneration()
        {
            var nextGeneration = new Generation();
            for (var i = 0; i < GlobalParameters.GenerationSize / 2; i++)
            {
                var children = Mate();
                nextGeneration.Add(children.Item1);
                nextGeneration.Add(children.Item2);
            }
            return nextGeneration;
        }
    }
}
