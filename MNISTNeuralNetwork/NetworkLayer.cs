using System;
using MNISTNeuralNetwork;

namespace MNISTNeuralNetwork
{
    public class NetworkLayer
    {
        private Neuron[] neurons;
        private double[,] weights;
        private NetworkLayer previousLayer;
        private NetworkLayer nextLayer;
        
        /*
         * Constructor for all layers except the input layer.
         */
        public NetworkLayer(int numberOfNeurons, ref NetworkLayer prevLayer)
        {
            previousLayer = prevLayer;
            neurons = new Neuron[numberOfNeurons];

            for(int i = 0; i < numberOfNeurons; i++)
            {
                neurons[i] = new Neuron();
            }

            if(previousLayer != default(NetworkLayer))
            {
                defineWeights();
            }
            nextLayer = default(NetworkLayer);
        }


        /*
         * Constructor for the input layer. It does not require an input for the previous layer as
         * the input layer does not have a layer preceding it.
         */
        public NetworkLayer(int numberOfNeurons)
        {
            previousLayer = default(NetworkLayer);
            neurons = new Neuron[numberOfNeurons];
            for (int i = 0; i < numberOfNeurons; i++)
            {
                neurons[i] = new Neuron();
            }
            nextLayer = default(NetworkLayer);
        }

        /*
         * Return the number of Neurons managed by the NetworkLayer
         */
        public int numOfNeurons()
        {
            return neurons.Length;
        }

        /*
         * Assigns weights to the layer. Only used in the constructor.
         */
        private void defineWeights()
        {
            int prevLayerLength = previousLayer.numOfNeurons();
            previousLayer.weights = new double[prevLayerLength, numOfNeurons()];

            Random random = new Random();
            for(int row = 0; row < prevLayerLength; row++)
            {
                for(int col = 0; col < numOfNeurons(); col++)
                {
                    previousLayer.weights[row, col] = random.NextDouble();
                }
            }
            previousLayer.nextLayer = this;
        }


        /*
         * Returns a reference to the nextLayer instance variable
         */
        public NetworkLayer getNextLayer()
        {
            return nextLayer;
        }

        /*
         *Returns a reference to the previousLayer instance variable
         */
        public NetworkLayer getPreviousLayer()
        {
            return previousLayer;
        }


        /*
         * Return weight at the given row and column
         */
        public double getWeight(int row, int col)
        {
            return weights[row, col];
        }


        /*
         * Propagate data from one layer to the next.
         */
        public void forwardProp()
        {
            for(int col = 0; col < weights.GetLength(1); col++)
            {
                double activation = 0;
                for(int row = 0; row < weights.GetLength(0); row++)
                {
                    activation += neurons[row].getActivation() * weights[row, col]; //Value to be forwarded to a Neuron in the next layer.
                }
                nextLayer.forwardPropToNeuron(col, activation);
            }
        }


        /*
         * Forward data to a specific Neuron in the next layer.
         */
        public void forwardPropToNeuron(int index, double val)
        {
            neurons[index].setActivation(val);
            neurons[index].sigmoidActivation();
        }

        /*
         * Prints the values held by the neurons to the console.
         */
        public void printNeurons()
        {
            for(int i = 0; i < neurons.Length; i++)
            {
                Console.WriteLine(neurons[i].getActivation());
            }
        }

        public void setNeurons(byte[,] pixels)
        {
            int count = 0;
            for(int i = 0; i < pixels.GetLength(0); i++)
            {
                for(int j = 0; j < pixels.GetLength(1); j++)
                {
                    neurons[count].setActivation((double)pixels[i,j]);
                    count++;
                }
            }
        }


        public Neuron[] getNeurons()
        {
            return neurons;
        }
    }
}
