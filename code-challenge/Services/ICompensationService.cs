using challenge.Models;
using challenge.Models.Comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services.Comp
{
    public interface ICompensationService
    {
        Compensation GetCompById(string id);
        Compensation Create(Compensation compensation);
       
    }
}
