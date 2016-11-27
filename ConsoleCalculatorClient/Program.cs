using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleCalculatorClient.CalculatorService;

namespace ConsoleCalculatorClient
{
    class Program
    {
        private static readonly CalculatorClient CalculatorClient = new CalculatorClient();

        private static readonly Dictionary<OperationKind, Func<double[], double>> CalculatorOperations = new Dictionary<OperationKind, Func<double[], double>>
        {
            { OperationKind.Add, (arguments) => CalculatorClient.Add(arguments[0], arguments[1]) },
            { OperationKind.Divide,  (arguments) => CalculatorClient.Divide(arguments[0], arguments[1]) },
            { OperationKind.Multiply,  (arguments) => CalculatorClient.Multiply(arguments[0], arguments[1]) },
            { OperationKind.Substract,  (arguments) => CalculatorClient.Substract(arguments[0], arguments[1]) },
            { OperationKind.Sqrt,  (arguments) => CalculatorClient.Sqrt(arguments[0]) }
        };

        private static readonly string Help =
            "Usage:"+
            "\t<operation> <arguments>"+
            "Operations:"+
            "\t+, -, /, *, sqrt";
        
        public static void Main(string[] args)
        {
            CalculatorArgumentsParser argumentsParser = new CalculatorArgumentsParser();
            OperationInfo operationInfo = argumentsParser.GetOperationInfo(args);
            PerformCalculation(operationInfo);
        }

        private static bool PerformCalculation(OperationInfo operationInfo)
        {
            bool result = true;

            if (operationInfo.OperationKind != OperationKind.Invalid)
            {
                try
                {
                    double operationResult = CalculatorOperations[operationInfo.OperationKind](operationInfo.Arguments);
                    Console.WriteLine(operationResult);
                }
                catch (FaultException<CalculationFault> e)
                {
                    Console.WriteLine($"Error: {e.Detail.Message}");
                    result = false;
                }
            }
            else
            {                
                DislpayHelp();
                result = false;
            }

            return result;
        }

        private static void DislpayHelp()
        {
            Console.WriteLine(Help);
        }
    }
}
