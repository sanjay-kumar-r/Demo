using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.DTOs
{
    [DataContract(Name = "Departments")]
    public class Departments
    {
        /// <summary>
        /// Department Id
        /// </summary>
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// check if departnent is deleted
        /// </summary>
        [DataMember(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        //public List<Employees> Employees;
    }

    public enum Dept
    {
        IT =1,
        HR,
        SALES,
        DEV,
        QA
    }
}
