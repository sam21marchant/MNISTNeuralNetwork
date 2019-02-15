using System;


namespace MNISTNeuralNetwork
{
    public class Neuron
    {
        //Instance Variables
        private double activation;

        /*
         * Constructor for the Neuron Class
         */
        public Neuron(){ }


        /*
         * Getter method for the activation instance variable
         */
        public double getActivation()
        {
            return activation;
        }


        /*
         * Setter method for the activation instance variable
         */
        public void setActivation(double newActivation)
        {
            activation = newActivation;
        }


        /*
         * Adds the value passed into the method to the activation instance variable
         */
        public void addToActivation(double number)
        {
            activation += number;
        }


        /*
         * Sigmoid function to normalize the activation value
         */
        public void sigmoidActivation()
        {
            activation = activation / Math.Sqrt(1.0d + activation * activation);
        }
    }
}