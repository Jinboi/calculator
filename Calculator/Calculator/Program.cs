using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        #region Methods: Static
        static void Main(string[] args)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            CalculatorService calculatorService = new CalculatorService();
            CalculatorController calculatorController = new CalculatorController(calculatorService);
            UserInterface ui = new UserInterface(calculatorController);

            // Start the app
            bool endApp = false;

            while (!endApp)
            {
                endApp = calculatorController.Run(ui);
            }

            Console.WriteLine("Goodbye");
        }

        #endregion
    }
}