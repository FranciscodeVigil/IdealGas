using System;
using System.IO;
namespace IdealGasCalcWithClass
{
    class IdealGas
    {
        private double mass;
        private double volume = 1;
        private double temp;
        private double molecularWeight;
        private double pressure;

        private void Calc()//calculates the pressure of gas using the ideal gas law
        {
            double moles = mass / molecularWeight ;
            pressure = (moles * 8.3145 * (temp + 273.15)) / volume;
            
        }
        public double GetMass()
        {
            return mass;
        }

        public void SetMass(double value)
        {
            mass = value;
            Calc();
        }

        public double GetVolume()
        {
            return volume;
        }

        public void SetVolume(double value)
        {
            volume = value;
            Calc();
        }

        public double GetTemp()
        {
            return temp;
        }

        public void SetTemp(double value)
        {
            temp = value;
            Calc();
        }
        public double GetMolecularWieght()
        {
            return molecularWeight;
        }
        public void SetMolecularWieght(double value)
        {
            molecularWeight = value;
            Calc();
        }
        public double GetPressure()
        {
            return pressure;
        }
    }
    class Program
    {
        static void DisplayHeader()
        {
            Console.WriteLine("Hello welcome to the Ideal Gas Calculator!");
        }
        private static void DisplayGasNames(string[] gasNames, int countGases)
        //Prints the Gases in 3 columns 
        {
            for (int i = 1; i < 85; i++)
            {
                Console.Write("{0,-2}: {1,-27}", i, gasNames[i]);
                if (i % 3 == 0)
                {
                    Console.WriteLine("\n");
                }
                countGases = i;
            }
        }
        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        { // fills the gasNames array with the entries form the excel doc and seperates them
            int i = 1;
            int j = 1;
            string path = @"MolecularWeightsGasesAndVapors (2).csv";
            string text = File.ReadAllText(path);
            string[] splitText = text.Split(',', '\n');
            for (i = 1; i < (85); i++)
            {
                gasNames[i] = splitText[2 * i];
            }
            for (j = 2; j < (85); j++)
            {
                molecularWeights[j] = Convert.ToDouble(splitText[(2 * j) - 1]);
            }
            count = i + j;
        }
        private static double GetMolecularWeightsFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {    //Finds the entry number of the gas and returns its molecular weight.
           int gasNum = Array.IndexOf(gasNames,gasName);
            if (gasNum == -1) {
            return -1; 
             }
            Console.WriteLine(molecularWeights[gasNum + 1]);
            return molecularWeights[(gasNum + 1)];
        }
       
        private static void DisplayPressure(double pressure, double temp)
        {
            Console.WriteLine("Pressure is {0} pascals and {1} Degrees C ", pressure, temp);
        }
        static double PaToPSI(double pascals)// Pascals to Pounds per square inch
        {
            return pascals / 6895;
        }
        static void Main()
        {
            double molecularWeight = 0;
            string[] gasNames = new string[100];
            double[] molecularWeights = new double[100];
            int count = 0;
            int countGases = 0;
            bool calcState = true;
            string input = "";
            DisplayHeader();
            GetMolecularWeights(ref gasNames, ref molecularWeights, out count);
            DisplayGasNames(gasNames, countGases);
            while (calcState == true)
            {
                bool varPolice = true;
                while (varPolice == true)
                {
                    try
                    {
                        Console.WriteLine("Please select one of the following gases by entering the gases name");
                        input = Console.ReadLine();
                        molecularWeight = GetMolecularWeightsFromName((input), gasNames, molecularWeights, countGases);
                        // checks to see if the given name exists within gasNames
                        if (molecularWeight == -1)
                        {
                            Console.WriteLine("Put a proper name!");
                        } else
                        {
                            varPolice = false;
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Bro, you gotta enter the right kind of thing");
                        Console.WriteLine("Try Again");
                        varPolice = true;
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine("You've entered too much");
                        Console.WriteLine("Try Again");
                        varPolice = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("You've done something wrong");
                        Console.WriteLine("Try Again");
                        varPolice = true;
                    }
                }
          
                IdealGas gas = new IdealGas();// waits untill it knows that their is a gas with the given name to create a new ideal gas
                gas.SetMolecularWieght(molecularWeight);
                Console.WriteLine("Please enter the gases volume in cubic meters:");
                gas.SetVolume(Convert.ToDouble(Console.ReadLine()));
                
                Console.WriteLine("Please enter the gases mass in grams:");
                gas.SetMass(Convert.ToDouble(Console.ReadLine()));

                Console.WriteLine("Please enter the gases tempature in celsius:");
                gas.SetTemp(Convert.ToDouble(Console.ReadLine()));

                DisplayPressure(gas.GetPressure(), gas.GetTemp());
                bool varPolice2 = true;
                Console.WriteLine("Would you like to go again? [1] - Yes [0] - NO");
                while (varPolice2 == true)
                {
                    input = Console.ReadLine();
                    if (input == "1")
                    {
                        varPolice2 = false;
                        break;
                    }
                    if (input == "0")
                    {
                        Console.WriteLine("See Ya LATER");
                        varPolice2 = false;
                        calcState = false;
                    }
                    else
                    {
                        Console.WriteLine("TRY AGAIN :");
                    }
                }
            }
        }
    }
}
