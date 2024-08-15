using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class CalculatorService
    {
        JsonWriter writer;
        private List<string> calculations; // List to store the history of calculations
        private List<double> results; // List to store the results of calculations

        public CalculatorService()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();

            calculations = new List<string>(); // Initialize the history list
            results = new List<double>(); // Initialize the results list
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            string operationSymbol = GetOperationSymbol(op); // Get the symbol for the operation

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "e":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10^x");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sine");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosine");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangent");
                    break;
                default:
                    break;
            }

            // Store the operation in the list
            calculations.Add($"{num1} {operationSymbol} {num2} = {result}");
            results.Add(result); // Store the result in the results list

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        private string GetOperationSymbol(string op)
        {
            // Return the symbol corresponding to the operation code
            return op switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "p" => "^",
                "r" => "√",
                "e" => "10^",
                "sin" => "sin",
                "cos" => "cos",
                "tan" => "tan",
                _ => "?",
            };
        }

        public List<string> GetCalculations()
        {
            return calculations;
        }

        public List<double> GetResults()
        {
            return results;
        }

        public void ClearCalculations()
        {
            calculations.Clear(); // Clear the list
            results.Clear(); // Clear the results list
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }

}
