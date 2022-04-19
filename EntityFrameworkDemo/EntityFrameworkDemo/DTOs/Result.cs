using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.DTOs
{
    [DataContract(Name = "Result")]
    public class Result
    {
        [DataMember(Name = "Res")]
        public bool Res { get; set; }

        [DataMember(Name = "ResultMessage")]
        public object ResultMessage { get; set; }
    }
}
