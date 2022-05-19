using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.DTOs
{
    [DataContract(Name = "Employees")]
    public class Employees
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        [DataMember(Name = "DOJ")]
        [DataType(DataType.DateTime)]
        public DateTime? DOJ { get; set; }

        [DataMember(Name = "DepartmentId")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [DataMember(Name = "Department")]
        public Departments Department { get; set; }

        [DataMember(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }


    }
}
