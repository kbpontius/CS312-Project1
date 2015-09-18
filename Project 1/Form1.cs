using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
    ALL SPACE AND TIME COMPLEXITY EVALUATIONS
    WERE INCLUDED IN THE SUBMISSION FILE, BY METHOD.
    PLEASE REFER TO THAT FILE FOR THE ASSOCIATED
    BIG-O NUMBERS AND EXPLANATIONS.
*/

namespace Project_1
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        HashSet<int> set = new HashSet<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            bool result = false;

            if (txtInput.Text.Length > 0 && txtKValue.Text.Length > 0)
            {
                int input = Convert.ToInt32(txtInput.Text);
                int kValue = Convert.ToInt32(txtKValue.Text);

                double probability = getProbability(kValue);
                result = isPrime(input, kValue);

                if (result == true)
                {
                    txtOutput.Text = "Yes with a probability of " + probability.ToString() + ".";
                }
                else
                {
                    txtOutput.Text = "No.";
                }
            }
            else
            {
                txtOutput.Text = "ERROR: Please fill all fields.";
            }
        }

        private bool isPrime(int input, int kValue)
        {
            if (input == 1)
            {
                return true;
            }
            
            // There are only up to (input - 2) random numbers that we're interested in using. Thus, 
            // if kValue > (input - 2), we need to limit kValue to (input - 2).
            if (kValue > (input - 2))
            {
                kValue = input - 2; 
            }

            // Initialized to false for case when input == 0.
            bool isPrime = false;

            for (int i = 0; i < kValue; i++)
            {
                int a = getRand(input);
                int result = modularExponentiation(a, input - 1, input);

                if (result == 1)
                {
                    isPrime = true;
                }
                else
                {
                    isPrime = false;
                    break;
                }
            }

            // Clear all values from the HashSet that keeps track of the random numbers used.
            // A new set should be generated each time "Solve" is clicked.
            set.Clear();
            return isPrime;
        }

        private int modularExponentiation(int x, int y, int original)
        {
            int newY = y;

            if (y == 0)
            {
                return 1;
            }
            else if (y % 2 != 0)
            {
                newY -= 1;
            }

            // Recursive call.
            int result = modularExponentiation(x, newY / 2, original);
            int returnValue = (int)Math.Pow(result, 2);

            // If even.
            if (y % 2 != 0)
            {
                returnValue *= x;
            }

            // If mod is needed.
            if (returnValue >= original)
            {
                returnValue = returnValue % original;
            }

            return returnValue;
        }

        // MARK: - HELPER METHODS
        private int getRand(int upperBound)
        {
            int randNum = rand.Next(2, upperBound);

            // Checks to see if the number has already 
            // been used as a random number.
            if (set.Contains(randNum))
            {
                randNum = getRand(upperBound);
            }

            // Adds the random number to the HashSet after being
            // used in the computation.
            set.Add(randNum);
            return randNum;
        }

        private double getProbability(int kValue)
        {
            // This is the probability formula given to us 
            // in the book, implemented in code.
            return 1 - (1 / (Math.Pow(2, kValue)));
        }
    }
}