using EntityFrameworkDemo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.EmpServiceContracts
{
    public interface IEmployeesRepository
    {
        IEnumerable<Employees> GetEmployees(int? id = null);

        IEnumerable<Employees> GetEmployeesByFiltercondition(Employees emp = null);

        int Add(Employees emp);

        Result Update(Employees emp);

        Result Delete(int id);
    }
}
