using System;
using System.ServiceModel;

namespace CalculatorService
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return PerformCalculationSafe(() => a + b);
        }

        public double Substract(double a, double b)
        {
            return PerformCalculationSafe(() => a - b);
        }

        public double Multiply(double a, double b)
        {
            return PerformCalculationSafe(() => a * b);
        }

        public double Divide(double a, double b)
        {
            return PerformCalculationSafe(() => a / b);
        }

        public double Sqrt(double a)
        {
            return PerformCalculationSafe(() => Math.Sqrt(a));
        }

        // Internals

        private double PerformCalculationSafe(Func<double> calculation)
        {
            double result = calculation();

            if (double.IsNaN(result))
            {
                CalculationFault calculationFault = new CalculationFault("Result is NaN.");
                throw new FaultException<CalculationFault>(calculationFault);
            }
            if (double.IsInfinity(result))
            {
                CalculationFault calculationFault = new CalculationFault("Result is infinity. Possible division by zero or overflow.");
                throw new FaultException<CalculationFault>(calculationFault);
            };
            
            return result;
        }
    }
}
