using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Fault;

namespace WcfService
{ 
    [ServiceContract]
    public interface IService
    {
        [OperationContract]

    

        double Add(double a, double b);

        [OperationContract]
        double Substract(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);

        [OperationContract]
        [FaultContract(typeof(DividedByZeroFault))]
        double Divide(double a, double b);

        [OperationContract]
        [FaultContract(typeof(InvalidRootOperandFault))]
        double Sqrt(double a);
    }
}
