using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculatorClient
{
    internal class CalculatorArgumentsParser
    {
        private const int MinArgumentsCount = 2;
        private const int MaxArgumentsCount = 3;
        private const int OperationParameterIndex = 0;
        private const int NonArgumentsParametersCount = 1;

        private static readonly Dictionary<string, OperationKind> OperationKinds = new Dictionary<string, OperationKind>
        {
            {"+", OperationKind.Add},
            {"-", OperationKind.Substract},
            {"/", OperationKind.Divide},
            {"*", OperationKind.Multiply},
            {"sqrt", OperationKind.Sqrt}
        };

        internal OperationInfo GetOperationInfo(string[] args)
        {
            double[] operationArguments = {};
            OperationKind operationKind = OperationKind.Invalid;

            int argumentsCount = args.Length;
            if ((argumentsCount >= MinArgumentsCount) && (argumentsCount <= MaxArgumentsCount))
            {
                if (OperationKinds.TryGetValue(args[OperationParameterIndex], out operationKind))
                {
                    if (!TryParseArguments(args, out operationArguments))
                    {
                        operationKind = OperationKind.Invalid;
                    }
                }

            }            

            return new OperationInfo(operationArguments, operationKind);
        }

        private bool TryParseArguments(string[] args, out double[] arguments)
        {
            bool result = true;

            arguments = new double[args.Length - NonArgumentsParametersCount];
            for (int i = OperationParameterIndex + 1; (i < args.Length) && result; i++)
            {
                if (!double.TryParse(args[i], out arguments[i - NonArgumentsParametersCount]))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
