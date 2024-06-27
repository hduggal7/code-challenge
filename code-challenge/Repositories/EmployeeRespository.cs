using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        private void LoadDirectReports(Employee employee)
        {
            if (employee != null)
            {
                _employeeContext.Entry(employee).Collection(e => e.DirectReports).Load();

                foreach (var directReport in employee.DirectReports)
                {
                    LoadDirectReports(directReport);
                }
            }
        }
        public Employee GetById(string id)
        {
            //Due to Lazy loading issues with Entity Frameowrk, .Include is necessary to reference DirectReports
            var employee = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            if (employee == null)
                return null;
            LoadDirectReports(employee);
            return employee;
        }

        public TotalReports GetAllReports(string id)
        {
            var employee = new TotalReports();
            var employeeData = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            LoadDirectReports(employeeData);
            employee.Employee = employeeData;
            employee.NumberOfReports = GetNumberOfReports(employeeData);
            return employee;
        }

        public int GetNumberOfReports(Employee employee)
        {
            int totalReports = 0;
            var dReports = employee.DirectReports;
            if (employee != null)
            {
                if (employee.DirectReports != null)
                {
                    foreach (var directReports in dReports)
                    {
                     
                        totalReports += 1 + GetNumberOfReports(directReports);
                            
                    }
                }

            }
            return totalReports;
        }
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
