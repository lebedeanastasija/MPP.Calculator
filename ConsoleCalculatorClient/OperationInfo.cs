namespace ConsoleCalculatorClient
{
    internal class OperationInfo
    {
        public double[] Arguments { get; }
        public OperationKind OperationKind { get; }

        public OperationInfo(double[] arguments, OperationKind operationKind)
        {
            Arguments = arguments;
            OperationKind = operationKind;
        }
    }
}