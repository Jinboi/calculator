// -------------------------------------------------------------------------------------------------
// Calculator.OutputHandler
// -------------------------------------------------------------------------------------------------
// Displays results provided by CalculatorEngine to user.
// -------------------------------------------------------------------------------------------------

namespace CalculatorProgram;
public class OutputHandler
{
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
}
