using System;
using System.Collections.Generic;
using System.IO;
using MNISTNeuralNetwork;

namespace MNISTNeuralNetwork
{
    public class NeuralNetwork
    {
        private NetworkLayer inputLayer;
        private NetworkLayer outputLayer;


        /*
         * Constructor for the NeuralNetwork class. numberOfNeuronsPerLayer determines
         * how many neurons will be in each inner layer. The length of the List will 
         * reflect how many inner layers there are.
         */
        public NeuralNetwork(IList<int> numberOfNeuronsPerLayer)
        {
            inputLayer = new NetworkLayer(784);
            NetworkLayer temp = inputLayer;
            for(int i = 0; i < numberOfNeuronsPerLayer.Count; i++)
            {
                NetworkLayer innerLayer = new NetworkLayer(numberOfNeuronsPerLayer[i], ref temp);
                temp = innerLayer;
            }
            outputLayer = new NetworkLayer(10, ref temp);
        }


        /*
         * Return a reference to the input layer of the network
         */
        public NetworkLayer getInputLayer()
        {
            return inputLayer;
        }


        /*
         * Propagate data forward through the network.
         */
        public void forwardPropagation()
        {
            NetworkLayer currentLayer = inputLayer;
            while(currentLayer.getNextLayer() != default(NetworkLayer))
            {
                currentLayer.forwardProp();
                currentLayer = currentLayer.getNextLayer();
            }
            currentLayer.printNeurons();
            trainNetwork();
        }


        /*
         * Train the network using MNIST dataset
         */
        public void trainNetwork()
        {
            Image[] trainingData = processTrainingData();
            Console.Write(trainingData[20].ToString());
        }


        /*
         * 
         */
        private Image[] processTrainingData()
        {
            FileStream imageFS = new FileStream(@"C:\Users\Sam\source\repos\MNISTNeuralNetwork\MNISTNeuralNetwork\TrainingData\train-images.idx3-ubyte", FileMode.Open);
            FileStream labelFS = new FileStream(@"C:\Users\Sam\source\repos\MNISTNeuralNetwork\MNISTNeuralNetwork\TrainingData\train-labels.idx1-ubyte", FileMode.Open);

            BinaryReader imageReader = new BinaryReader(imageFS);
            BinaryReader labelReader = new BinaryReader(labelFS);

            //Read header data for image file
            int imageMagicNumber = flipEndianAndReadInt32(imageReader);
            int numOfImages = flipEndianAndReadInt32(imageReader);
            int numOfRows = flipEndianAndReadInt32(imageReader);
            int numOfCols = flipEndianAndReadInt32(imageReader);

            //Read header data for label file
            int labelMagicNumber = flipEndianAndReadInt32(labelReader);
            int numOfLabels = flipEndianAndReadInt32(labelReader);

            Image[] retVal = new Image[numOfImages];
            for(int i = 0; i < numOfImages; i++)
            {
                byte[,] pixels = new byte[28,28];
                byte label = labelReader.ReadByte();
                for(int row = 0; row < numOfCols; row++) 
                {
                    for(int col = 0; col < numOfRows; col++)
                    {
                        pixels[row, col] = imageReader.ReadByte();
                    }
                }
                retVal[i] = new Image(label, pixels);
            }
            return retVal;
        }


        /*
         * Flips the endianness of a 32 bit integer
         * Credit to Hans Passant: https://stackoverflow.com/questions/20967088/what-did-i-do-wrong-with-parsing-mnist-dataset-with-binaryreader-in-c
         */
        private int flipEndianAndReadInt32(BinaryReader br)
        {
            var bytes = br.ReadBytes(sizeof(Int32));
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}