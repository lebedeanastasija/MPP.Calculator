using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Fault;

namespace WcfService
{
    public class Service : IService
    {
        private double result;
        public double Add(double a, double b)
        {
            result = a + b;
            return result;
        }

        public double Substract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

       
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new FaultException<DividedByZeroFault>(new DividedByZeroFault("Invalid second operand: dividing by zero is forbidden!"));
            }                

            return a / b;
        }

        
        public double Sqrt(double a)
        {
            if (a < 0)
            {
                throw new FaultException<InvalidRootOperandFault>(new InvalidRootOperandFault("Invalid first operand: root calculation of a negative number is forbidden!"));
            }
            
            return System.Math.Sqrt(a);
        }
    }
}
