using CalculatorLibrary;

namespace CalculatorProgram
{
    public class CalculatorController
    {
        #region Fields

        private CalculatorService _calculatorService;
        private int _counter = 0;

        #endregion
        #region Constructors
        public CalculatorController(CalculatorService calculatorService)
        {
            _calculatorService = calculatorService;  // Assignment in constructor
        }

        #endregion
        #region Methods: Public
        public bool Run(UserInterface ui)
        {
            double cleanNum1 = ui.GetFirstNumber(_calculatorService);
            string operation = ui.GetOperation();
            double cleanNum2 = ui.GetSecondNumber(operation);

            try
            {
                double result = _calculatorService.DoOperation(cleanNum1, cleanNum2, operation);
                _counter++;
                ui.DisplayResult(result, _counter);
            }
            catch (Exception e)
            {
                ui.DisplayError(e.Message);
            }

            ui.DisplayCalculations(_calculatorService.GetCalculations());

            if (ui.AskIfUserWantsToClearList())
            {
                _calculatorService.ClearCalculations();
            }

            return ui.AskIfUserWantsToCloseApp();
        }

        #endregion
    }
}
