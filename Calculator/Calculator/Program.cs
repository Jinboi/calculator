using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
    #region Methods: Static
    static void Main(string[] args)
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        // Create an instance of CalculatorService and UserInterface
        var calculatorService = new CalculatorService();
        var userInterface = new UserInterface(calculatorService);

        bool endApp = false;

        while (!endApp)
        {
            endApp = userInterface.Run();
        }

        Console.WriteLine("Goodbye");
    }

    #endregion
}