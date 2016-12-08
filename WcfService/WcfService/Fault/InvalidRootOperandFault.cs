using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    [DataContract]
    public class InvalidRootOperandFault
    {
        [DataMember]
        public string errorMessage;
        public InvalidRootOperandFault()
        {
        }
        public InvalidRootOperandFault(string error)
        {
            errorMessage = error;
        }
    }
}
