using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            CalculatorEngine engine = new CalculatorEngine(calculator);
            UserInterface ui = new UserInterface();

            bool isRunning = true;
            
            do
            {
                engine.Run(ui);
            } while (isRunning);
        }
    }
}