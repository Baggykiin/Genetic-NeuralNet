using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using AI_Project.Genetics;
using AI_Project.NeuralNets;

namespace AI_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Program().RunGenetics(new NumberGeneratorFitnessDecider(42));
            new Program().RunNeuralNet();
        }

        public void LoadNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                string filename = "Characters/" + i + ".png";
                var img = (Bitmap)Image.FromFile(filename, false);
                
            }
        }

        private List<double> validInput = new List<double>();

        public void RunNeuralNet()
        {
            Generation currentGeneration = new Generation();

            // populate the generation
            NeuralNet net;
            double fitness;
            for (var i = 0; i < GlobalParameters.GenerationSize; i++)
            {
                net = new NeuralNet(validInput.Length);
                net.CreateLayers(new List<int> { 1 });

                fitness = GetFitness(net);
                var genes = net.ExtractChromosome();
                currentGeneration.Add(Organism.FromGenes(genes, fitness));
            }
            /*var newGenes = validInput.ToList();
            newGenes.Add(10);
            net = new NeuralNet(validInput.Length);
            net.CreateLayers(new List<int> {1});
            net.InsertChromosome(newGenes);
            fitness = GetFitness(net);
            var newOrganism = Organism.FromGenes(newGenes, fitness);
            currentGeneration.Add(newOrganism);*/
            do
            {
                currentGeneration = currentGeneration.CreateNextGeneration();
                var fitnesses = new List<double>();
                foreach (var organism in currentGeneration)
                {
                    net = new NeuralNet(validInput.Length);
                    net.CreateLayers(new List<int> { 1 });
                    var genes = organism.ExtractGenes();
                    net.InsertChromosome(genes);
                    fitness = GetFitness(net);
                    fitnesses.Add(fitness);
                    organism.Fitness = fitness;

                }
                Console.WriteLine("New generation created. Average fitness: " + fitnesses.Average());
            } while (!currentGeneration.Any(organism => organism.Fitness > 0.99));
            Console.WriteLine("An organism has succeeded in its evolution!");
            Debugger.Break();
        }

        public IEnumerable<double> GetInvalidInput()
        {
            return RNG.NextIntsD(validInput.Length, 0, 2);
        }

        public double GetFitness(NeuralNet net, int attempts = 100, double occurrence = 0.1)
        {
            int successCount = 0;
            // Decide fitness
            for (int j = 0; j < attempts; j++)
            {
                var success = false;
                //50% chance of a valid input
                if (RNG.NextDouble() < occurrence)
                {
                    net.FeedInput(validInput);
                    var output = net.Run().First();
                    success = output >= 0.5;

                }
                else // And 50% chance of an invalid input
                {
                    IEnumerable<double> invalidInput;
                    do
                    {
                        invalidInput = GetInvalidInput(); // 1.5ns
                        if (invalidInput.SequenceEqual(validInput))
                        {
                            Debugger.Break();
                        }
                    } while (invalidInput.SequenceEqual(validInput));
                    net.FeedInput(invalidInput); // 1.5ns
                    var output = net.Run().First();
                    success = output < 0.5; // 2.7ns

                }
                if (success) successCount++;
            }
            var successRate = (successCount / (double)attempts);
            return successRate;
        }

        /*public void RunGenetics(IFitnessDecider fitnessDecider)
        {
            if (GlobalParameters.GenerationSize % 2 == 1)
            {
                throw new ArgumentException("GlobalParameters.GenerationSize must be an even number!", "GlobalParameters.GenerationSize");
            }

            Console.WriteLine("Creating initial generation");
            // CreateLayers the initial generation
            var currentGeneration = new Generation();
            for (var i = 0; i < GlobalParameters.GenerationSize; i++)
            {
                currentGeneration.Add(Organism.CreateLayers(18, GlobalParameters.ChromosomeLength, fitnessDecider));
            }

            // The main generational cycle
            bool success;
            do
            {
                Console.WriteLine("Creating the next generation");
                var nextGeneration = new Generation();
                for (var i = 0; i < GlobalParameters.GenerationSize /2; i++)
                {
                    var parent1 = RouletteWheelSelect(currentGeneration);
                    var parent2 = RouletteWheelSelect(currentGeneration);
                    var children = parent1.Mate(parent2, fitnessDecider);
                    nextGeneration.Add(children.Item1);
                    nextGeneration.Add(children.Item2);
                }
                currentGeneration = nextGeneration;

                success = currentGeneration.Any(organism => fitnessDecider.IsSuccess(organism.Fitness));
            } while (!success);
            Console.WriteLine("An organism has succeeded in its evolution!");
            var winner = currentGeneration.First(organism => fitnessDecider.IsSuccess(organism.Fitness));
            var parents = winner.GetLeftHistory();
            var fullParents = winner.GetLeftHistory(true);
            Debugger.Break();
        }*/
    }
}
