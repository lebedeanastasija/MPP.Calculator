using System.Runtime.Serialization;

namespace CalculatorService
{
    [DataContract]
    public class CalculationFault
    {
        [DataMember]
        public string Message { get; internal set; }

        // Internals

        internal CalculationFault(string message)
        {
            Message = message;
        }
    }
}