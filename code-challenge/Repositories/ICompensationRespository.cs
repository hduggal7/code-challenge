using challenge.Models;
using challenge.Models.Comp;
using System;
using System.Threading.Tasks;
using challenge.Repositories;

namespace challenge.Repositories.Comp
{
    public interface ICompensationRepository
    {
        
        Compensation GetComp(String id);
        Compensation Add(Compensation compensation, string id);  
        Task SaveAsync();
    }
}