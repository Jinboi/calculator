using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            CalculatorService calculatorService = new CalculatorService();
            CalculatorController calculatorController = new CalculatorController(calculatorService);
            UserInterface ui = new UserInterface(calculatorController);

            ui.StartApp();

            Console.WriteLine("Goodbye");
        }
    }
}