using CommonDTOs;
using EntityFrameworkDemo.DTOs;
using EntityFrameworkDemo.EmpServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Result = EntityFrameworkDemo.DTOs.Result;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntityFrameworkDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeesRepository employeesRepository;
        private ILogger logger;
        private TraceData _traceData;
        private TraceData traceData
        {
            get
            {
                if (_traceData != null)
                    return _traceData;
                else
                {
                    HeaderInfo header = new HeaderInfo()
                    {
                        UserId = HttpContext?.Request?.Headers["UserId"],
                        TenantId = HttpContext?.Request?.Headers["tenantId"],
                        AccessToken = HttpContext?.Request?.Headers["token"]
                    };
                    string operationName = string.Empty;
                    if (HttpContext.Request?.RouteValues != null)
                    {
                        string controllerName = (string)HttpContext.Request?.RouteValues["controller"];
                        string actionName = (string)HttpContext.Request?.RouteValues["action"];
                        operationName = controllerName + "->" + actionName;
                    }
                    else if (!string.IsNullOrWhiteSpace(HttpContext?.Request?.Path))
                        operationName = HttpContext?.Request?.Path;
                    _traceData =  new TraceData()
                    {
                        headerInfo = header,
                        traceId = Activity.Current?.RootId ?? HttpContext?.TraceIdentifier,
                        operationName = operationName
                    };
                    return _traceData;
                }
            }
        }
        public EmployeesController(IEmployeesRepository employeesRepository, ILogger logger)
        {
            this.employeesRepository = employeesRepository;
            this.logger = logger;
            //SetLogicalThreadContext();
        }

        [HttpGet]
        [Route("Ping")]
        public string Ping()
        {
            return "EmployeesController -> Pong";
        }

        [HttpGet]
        public IEnumerable<Employees> Get()
        {
            try
            {
                logger.Log(LogLevel.INFO, "Employees.Get method called :", traceData);
                return employeesRepository.GetEmployees();
            }
            catch(Exception ex)
            {
                //printlog
                throw;
            }
        }

        [HttpGet("{id}")]
        [Route("GetEmployee/{id}")]
        public IEnumerable<Employees> Get(int id)
        {
            try
            {
                return employeesRepository.GetEmployees(id);
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        [HttpPost]
        [Route("GetEmployeesByFilter")]
        public IEnumerable<Employees> GetEmployeesByFilter([FromBody]Employees emp)
        {
            try
            {
                return employeesRepository.GetEmployeesByFiltercondition(emp);
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        [HttpPost]
        [Route("Add")]
        [Route("AddEmployee")]
        public int Add([FromBody] Employees emp)
        {
            try
            {
                logger.Log(LogLevel.INFO, "Employees.Add method called :", traceData);
                return employeesRepository.Add(emp);
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        [HttpPost]
        [Route("Update")]
        [Route("UpdateEmployee")]
        public Result Update([FromBody] Employees emp)
        {
            try
            {
                return employeesRepository.Update(emp);
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        [HttpPost]
        [Route("Delete")]
        [Route("DeleteEmployee")]
        public Result Delete([FromBody] int id)
        {
            try
            {
                var emp = new Employees() { Id = id, IsDeleted = true };
                var result = employeesRepository.Update(emp);
                result.ResultMessage = "UpdateAsDeleted:-\n" + result.ResultMessage;
                return result;
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        [HttpPost]
        [Route("PermanentDelete")]
        [Route("PermanentDeleteEmployee")]
        public Result PermanentDelete([FromBody] int id)
        {
            try
            {
                return employeesRepository.Delete(id);
            }
            catch (Exception ex)
            {
                //printlog
                throw;
            }
        }

        //// DELETE api/<EmployeesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
