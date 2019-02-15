using System;
using MNISTNeuralNetwork;

namespace MNISTNeuralNetwork
{
    public class Image
    {
        private byte label;
        private byte[,] data;

        public Image(byte label, byte[,] pixels)
        {
            this.label = label;
            data = new byte[28, 28];

            for(int i = 0; i < 28; i++)
            {
                for(int j = 0; j < 28; j++)
                {
                    data[i, j] = pixels[i, j];
                }
            }
        }

        /*
         * Print the digit to the screen using ascii characters
         */
        
        override public string ToString()
        {
            string imgToString = label + ": ";
            //Console.WriteLine(label + ": ");
            for(int row = 0; row < data.GetLength(0); row++)
            {
                for(int col = 0; col < data.GetLength(1); col++)
                {
                    if(data[row, col] == 0)
                    {
                        //Console.Write(" ");
                        imgToString += " ";
                    }
                    else if(data[row, col] >= 250)
                    {
                        //Console.Write("O");
                        imgToString += "O";
                    }
                    else
                    {
                        //Console.Write(".");
                        imgToString += ".";
                    }
                }
                Console.WriteLine();
                imgToString += '\n';
            }
            return imgToString;
        }

        public byte[,] getPixels()
        {
            return data;
        }
    }
}