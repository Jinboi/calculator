using CalculatorLibrary;

namespace CalculatorProgram
{
    public class CalculatorEngine
    {
        private Calculator _calculator;
        private int _counter;

        public CalculatorEngine(Calculator calculator)
        {
            _calculator = calculator;
            _counter = 0;
        }
        public bool Run(UserInterface ui)
        {
            double cleanNum1 = ui.GetFirstNumber(_calculator);
            string operation = ui.GetOperation();
            double cleanNum2 = ui.GetSecondNumber(operation);

            try
            {
                double result = _calculator.DoOperation(cleanNum1, cleanNum2, operation);
                _counter++;
                ui.DisplayResult(result, _counter);
            }
            catch (Exception e)
            {
                ui.DisplayError(e.Message);
            }

            ui.DisplayCalculations(_calculator.GetCalculations());

            if (ui.AskToClear())
            {
                _calculator.ClearCalculations();
            }

            return ui.AskToEndApp();
        }
    }
}
