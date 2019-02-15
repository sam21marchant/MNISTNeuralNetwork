using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MNISTNeuralNetwork;

namespace MNISTNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app!
            NeuralNetwork net = new NeuralNetwork(new List<int>{17,16});
            
            /*double [,] arr = net.getWeights();

            for(int i = 0; i < arr.GetLength(0); i++){
                for(int j = 0; j < arr.GetLength(1); j++){
                    Console.Write("{0:F2} ", arr[i,j]);
                }
                Console.WriteLine();
            }

            arr = net.getWeights2();
            for(int i = 0; i < arr.GetLength(0); i++){
                for(int j = 0; j < arr.GetLength(1); j++){
                    Console.Write("{0:F2} ", arr[i,j]);
                }
                Console.WriteLine();
            }*/
            net.forwardPropagation();
            Console.ReadKey();
        }
    }
}
