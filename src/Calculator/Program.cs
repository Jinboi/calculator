// -------------------------------------------------------------------------------------------------
// Calculator.Program
// -------------------------------------------------------------------------------------------------
// Insertion point for the calculator application.
// -------------------------------------------------------------------------------------------------

using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
    #region Methods: Static
    static void Main(string[] args)
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        // Create an instance of CalculatorService and CalculatorEngine
        var calculatorService = new CalculatorService();
        var calculatorEngine = new CalculatorEngine(calculatorService);

        bool endApp = false;

        while (!endApp)
        {
            endApp = calculatorEngine.Run();
        }

        Console.WriteLine("Goodbye");
    }

    #endregion
}