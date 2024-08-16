using CalculatorLibrary;

namespace CalculatorProgram
{
    public class UserInterface
    {
        #region Fields
        private CalculatorController _calculatorController;
        #endregion
        #region Constructors
        public UserInterface(CalculatorController calculatorController)
        {
            _calculatorController = calculatorController;
        }

        #endregion
        #region Methods: public
        public double GetFirstNumber(CalculatorService calculator)
        {
            Console.WriteLine("Do you want to use a previous result for the first number? (y/n)");
            if (Console.ReadLine() == "y")
            {
                List<double> results = calculator.GetResults();
                if (results.Count > 0)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {results[i]}");
                    }

                    Console.Write("Select a result by number: ");
                    int index;
                    while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > results.Count)
                    {
                        Console.Write("Invalid selection. Please choose a valid result number: ");
                    }

                    return results[index - 1];
                }
                else
                {
                    Console.WriteLine("No previous results available.");
                    return GetNumberInput("Type a number, and then press Enter: ");
                }
            }
            else
            {
                return GetNumberInput("Type a number, and then press Enter: ");
            }
        }
        public string GetOperation()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power (num1 ^ num2)");
            Console.WriteLine("\tr - Square Root (√num1)");
            Console.WriteLine("\te - 10^x (10^num1)");
            Console.WriteLine("\tsin - Sine (sin(num1))");
            Console.WriteLine("\tcos - Cosine (cos(num1))");
            Console.WriteLine("\ttan - Tangent (tan(num1))");
            Console.Write("Your option? ");
            return Console.ReadLine();
        }
        public double GetSecondNumber(string operation)
        {
            if (operation == "a" || operation == "s" || operation == "m" || operation == "d" || operation == "p")
            {
                return GetNumberInput("Type another number, and then press Enter: ");
            }
            return 0; // Unary operations don't need a second number
        }
        public void DisplayResult(double result, int counter)
        {
            Console.WriteLine($"The calculator was used {counter} times");
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
            }
        }
        public void DisplayError(string message)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + message);
        }
        public void DisplayCalculations(IEnumerable<string> calculations)
        {
            Console.WriteLine("Recent Calculations:");
            foreach (var calc in calculations)
            {
                Console.WriteLine(calc);
            }
        }
        public bool AskIfUserWantsToClearList()
        {
            Console.Write("Press 'c' and Enter to clear the list, or press any other key and Enter to continue: ");
            return Console.ReadLine() == "c";//Returns True if "c" or Returns False if anything else
        }
        public bool AskIfUserWantsToCloseApp()
        {
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            return Console.ReadLine() == "n"; //Returns True if "n" or Returns False if anything else
        }

        #endregion        
        #region Methods: private
        private double GetNumberInput(string prompt)
        {
            Console.Write(prompt);
            string input;
            double number;
            while (!double.TryParse(input = Console.ReadLine(), out number))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
            }
            return number;
        }

        #endregion
    }
}
