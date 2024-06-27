using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models.Comp;
using Microsoft.Extensions.Logging;
using challenge.Repositories;
using challenge.Repositories.Comp;
using challenge.Models;
using challenge.Services.Comp;

namespace challenge.Services.Comp
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public CompensationService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            //_employeeRepository = employeeRepository;
            _logger = logger;       
            
        }

        public Compensation Create(Compensation compensation)
        {
            if(compensation != null)
            {
                Compensation employeeComp = _compensationRepository.Add(compensation, compensation.EmployeeId);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetCompById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetComp(id);
            }

            return null;
        }


        
    }
}
