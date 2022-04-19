using EntityFrameworkDemo.DTOs;
using EntityFrameworkDemo.EmpServiceContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkDemo.EmpUtils
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeesDBContext context;

        public EmployeesRepository(EmployeesDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employees> GetEmployees(int? id = null)
        {
            try
            {
                if (id == null)
                    return context.Employees.Include(x => x.Department).ToList();
                else
                    return new List<Employees>() { context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == id) };
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }

        }

        public IEnumerable<Employees> GetEmployeesByFiltercondition(Employees emp = null)
        {
            try
            {
                if (emp == null || (emp.Id <= 0 && string.IsNullOrWhiteSpace(emp.FirstName) && string.IsNullOrWhiteSpace(emp.LastName) && emp.DOJ == null
                    && (emp.Department == null || (emp.Department.Id <= 0 && string.IsNullOrWhiteSpace(emp.Department.Name)))))
                    return context.Employees.ToList();
                return context.Employees.Include(x => x.Department).Where(x => (emp.Id <= 0 || emp.Id == x.Id)
                    && (string.IsNullOrWhiteSpace(emp.FirstName) || x.FirstName.ToLower().Contains(emp.FirstName.ToLower()))
                    && (string.IsNullOrWhiteSpace(emp.LastName) || x.LastName.ToLower().Contains(emp.LastName.ToLower()))
                    && (emp.DOJ == null || emp.DOJ == x.DOJ)
                    && (emp.Department == null || ((emp.Department.Id <= 0 || emp.Department.Id == x.Department.Id)
                                                    && (string.IsNullOrWhiteSpace(emp.Department.Name)
                                                        || x.Department.Name.ToLower().Contains(emp.Department.Name.ToLower()))
                                                  )
                       )
                    );
            }
            catch(Exception ex)
            {
                //printlog
                throw;
            }
            
        }

        public int Add(Employees emp)
        {
            try
            {
                int id = 1;
                if (context.Employees.Count() > 0)
                    id = context.Employees.Max(x => x.Id) + 1;
                //emp.Id = id;
                context.Employees.Add(emp);
                context.SaveChanges();
                return id;
            }
            catch(Exception ex)
            {
                //printlog
                throw;
            }
        }
        public Result Update(Employees emp)
        {
            try
            {
                Result result = new Result();
                if (emp.Id > 0 && context.Employees.Count() > 0 && context.Employees.Any(x => x.Id == emp.Id))
                {
                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    var empExisting = context.Employees.Include(x => x.Department).First(x => x.Id == emp.Id);
                    var empModified = new Employees()
                    {
                        Id = emp.Id,
                        FirstName = emp.FirstName ?? empExisting.FirstName,
                        LastName = emp.LastName ?? empExisting.LastName,
                        DOJ = emp.DOJ ?? empExisting.DOJ,
                        Department = emp.Department ?? empExisting.Department,
                        IsDeleted = emp.IsDeleted
                    };
                    
                    var empUpdated = context.Employees.Attach(empModified);
                    empUpdated.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    result.Res = true;
                    result.ResultMessage = $"successfully updated employee date for id={emp.Id}";
                }
                else
                {
                    result.Res = false;
                    result.ResultMessage = $"unable to update employee date for id={emp.Id} \n "+
                        "Invalid_employeeId_or_No_data_exists_that_matches_employeeId";
                }
                return result;

            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        public Result Delete(int id)
        {
            try
            {
                Result result = new Result();
                if (id > 0 && context.Employees.Count() > 0 && context.Employees.Any(x => x.Id == id))
                {
                    Employees emp = new Employees() { Id = id };
                    context.Employees.Remove(emp);
                    context.SaveChanges();
                    result.Res = true;
                    result.ResultMessage = $"successfully deleted(permanent) employee date for id={id}";
                }
                else
                {
                    result.Res = false;
                    result.ResultMessage = $"unable to delete(permanent) employee date for id={id} \n " +
                        "Invalid_employeeId_or_No_data_exists_that_matches_employeeId";
                }
                return result;
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }
    }
}
