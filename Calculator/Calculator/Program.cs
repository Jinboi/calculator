using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace CalculatorProgram
{
    class Program
    {
        // The Calculator class is now part of Program.cs
        public class Calculator
        {
            JsonWriter writer;
            private List<string> calculations; // List to store the history of calculations
            private List<double> results; // List to store the results of calculations

            public Calculator()
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

        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            int counter = 0;

            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to choose whether to use a previous result as the first operand
                Console.WriteLine("Do you want to use a previous result for the first number? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    // Display the list of results
                    List<double> results = calculator.GetResults();
                    if (results.Count > 0)
                    {
                        for (int i = 0; i < results.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {results[i]}");
                        }

                        Console.Write("Select a result by number: ");
                        int index;
                        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > results.Count)
                        {
                            Console.Write("Invalid selection. Please choose a valid result number: ");
                        }

                        numInput1 = results[index - 1].ToString(); // Use the selected result as the first number
                    }
                    else
                    {
                        Console.WriteLine("No previous results available.");
                        continue;
                    }
                }
                else
                {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                }

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // If the user chooses a unary operation (like Square Root, 10^x, or Trigonometry), skip the second number                
                double cleanNum2 = 0;

                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - Power (num1 ^ num2)");
                Console.WriteLine("\tr - Square Root (√num1)");
                Console.WriteLine("\te - 10^x (10^num1)");
                Console.WriteLine("\tsin - Sine (sin(num1))");
                Console.WriteLine("\tcos - Cosine (cos(num1))");
                Console.WriteLine("\ttan - Tangent (tan(num1))");
                Console.Write("Your option? ");

                string op = Console.ReadLine();

                // For binary operations, ask for the second number
                if (op == "a" || op == "s" || op == "m" || op == "d" || op == "p")
                {
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                }

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                    // Display how many times the calculator was used    
                    counter++;
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
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                // Display the list of calculations
                Console.WriteLine("Recent Calculations:");
                foreach (var calc in calculator.GetCalculations())
                {
                    Console.WriteLine(calc);
                }

                // Ask the user if they want to clear the list
                Console.Write("Press 'c' and Enter to clear the list, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "c")
                {
                    calculator.ClearCalculations();
                    Console.WriteLine("Calculation list cleared.");
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }

            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }
    }
}
