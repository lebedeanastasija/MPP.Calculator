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
        private static readonly Dictionary<OperationKind, Func<double[], CalculatorClient, double>> CalculatorOperations = new Dictionary<OperationKind, Func<double[], CalculatorClient, double>>
        {
            { OperationKind.Add, (arguments, client) => client.Add(arguments[0], arguments[1]) },
            { OperationKind.Divide,  (arguments, client) => client.Divide(arguments[0], arguments[1]) },
            { OperationKind.Multiply,  (arguments, client) => client.Multiply(arguments[0], arguments[1]) },
            { OperationKind.Substract,  (arguments, client) => client.Substract(arguments[0], arguments[1]) },
            { OperationKind.Sqrt,  (arguments, client) => client.Sqrt(arguments[0]) }
        };

        private const string Help = 
            "Usage:" + 
            "\t<operation> <arguments>" + 
            "Operations:" + 
            "\t+, -, /, *, sqrt";

        public static void Main(string[] args)
        {
            using (CalculatorClient calculatorClient = new CalculatorClient())
            {
                CalculatorArgumentsParser argumentsParser = new CalculatorArgumentsParser();
                OperationInfo operationInfo = argumentsParser.GetOperationInfo(args);
                PerformCalculation(operationInfo, calculatorClient);
            }            
        }

        private static void PerformCalculation(OperationInfo operationInfo, CalculatorClient calculatorClient)
        {
            if (operationInfo.OperationKind != OperationKind.Invalid)
            {
                try
                {
                    double operationResult = CalculatorOperations[operationInfo.OperationKind](operationInfo.Arguments, calculatorClient);
                    Console.WriteLine(operationResult);
                }
                catch (FaultException<CalculationFault> e)
                {
                    Console.WriteLine($"{e.Detail.Message}");
                }
                catch (CommunicationException)
                {
                    Console.WriteLine("Connection error");
                }
            }
            else
            {                
                DislpayHelp();
            }
        }

        private static void DislpayHelp()
        {
            Console.WriteLine(Help);
        }
    }
}
