// -------------------------------------------------------------------------------------------------
// Calculator.InputHandler
// -------------------------------------------------------------------------------------------------
// Collects input from user and return results to CalculatorEngine.
// -------------------------------------------------------------------------------------------------

using CalculatorLibrary;

namespace CalculatorProgram;
public class InputHandler
{
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
    public bool AskIfUserWantsToClearList()
    {
        Console.Write("Press 'c' and Enter to clear the list, or press any other key and Enter to continue: ");
        return Console.ReadLine() == "c";
    }
    public bool AskIfUserWantsToCloseApp()
    {
        Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
        return Console.ReadLine() == "n";
    }
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
}
