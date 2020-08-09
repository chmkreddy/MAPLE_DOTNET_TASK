using MAPLE_INSURANCE_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAPLE_INSURANCE_API.DataAccess
{
    public interface IContractDataAccess
    {
        Task<List<Contracts>> GetContracts();
        Task<int> AddContract(Contracts contract);
        Task UpdateContract(Contracts contract);
        Task<int> DeleteContract(int? contractId);
    }
}
