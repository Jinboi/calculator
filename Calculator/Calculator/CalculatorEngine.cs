// -------------------------------------------------------------------------------------------------
// Calculator.CalculatorEngine
// -------------------------------------------------------------------------------------------------
// Program engine that interacts with user and calls InputHandler, OutputHandler and CalculatorService.
// -------------------------------------------------------------------------------------------------

using CalculatorLibrary;

namespace CalculatorProgram;
public class CalculatorEngine
{
    #region Fields

    private CalculatorService _calculatorService;
    private int _counter = 0;
    private readonly InputHandler _inputHandler;
    private readonly OutputHandler _outputHandler;

    #endregion
    #region Constructors
    public CalculatorEngine(CalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
        _inputHandler = new InputHandler();
        _outputHandler = new OutputHandler();
    }

    #endregion
    #region Methods: Internal
    internal bool Run()
    {
        double cleanNum1 = _inputHandler.GetFirstNumber(_calculatorService);
        string operation = _inputHandler.GetOperation();
        double cleanNum2 = _inputHandler.GetSecondNumber(operation);

        try
        {
            double result = _calculatorService.DoOperation(cleanNum1, cleanNum2, operation);
            _counter++;
            _outputHandler.DisplayResult(result, _counter);
        }
        catch (Exception e)
        {
            _outputHandler.DisplayError(e.Message);
        }

        _outputHandler.DisplayCalculations(_calculatorService.GetCalculations());

        if (_inputHandler.AskIfUserWantsToClearList())
        {
            _calculatorService.ClearCalculations();
        }

        return _inputHandler.AskIfUserWantsToCloseApp();
    }

    #endregion
}
