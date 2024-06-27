using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models.Comp;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;
using challenge.Data.CompCont;
using challenge.Repositories;
using challenge.Repositories.Comp;
using challenge.Services.Comp;

namespace challenge.Repositories.Comp
{
    public class CompensationRespository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<IEmployeeRepository> _logger;
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<ICompensationRepository> _logger1;



        public CompensationRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext, ILogger<ICompensationRepository> logger1, CompensationContext compensationContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
            _compensationContext = compensationContext;
        }

        public Compensation Add(Compensation compensation, string id)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            var employeeComp = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
            compensation.EmployeeId = employeeComp.EmployeeId;
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetComp(string id)
        {
            var compensation = _compensationContext.Compensations.SingleOrDefault(e => e.EmployeeId == id);
            if (compensation == null)
                return null;
            return compensation;
        }
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
