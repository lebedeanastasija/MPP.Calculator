using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.Fault
{
    [DataContract]
    public class DividedByZeroFault
    {
        [DataMember]
        public string errorMessage;
        public DividedByZeroFault()
        {
        }
        public DividedByZeroFault(string error)
        {
            errorMessage = error;
        }
    }
}
