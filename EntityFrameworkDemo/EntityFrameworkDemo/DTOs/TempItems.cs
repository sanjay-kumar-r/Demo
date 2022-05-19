using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.DTOs
{
    [DataContract(Name = "TempItems")]
    public class TempItems
    {
        [DataMember(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "EmployeeId")]
        
        public int EmployeeId { get; set; }
        [DataMember(Name = "Employee")]
        [ForeignKey("EmployeeId")]
        public virtual Employees Employee { get; set; }
    }
}
