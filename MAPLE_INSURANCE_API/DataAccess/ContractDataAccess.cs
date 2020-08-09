using MAPLE_INSURANCE_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAPLE_INSURANCE_API.DataAccess
{
    public class ContractDataAccess : IContractDataAccess
    {
        MAPLE_TESTContext dbContext;
        const string COUNTRY_USA = "USA";
        const string COUNTRY_CANADA = "CAN";
        const string GOLD_PLAN = "Gold";
        const string SILVER_PLAN = "Silver";
        const string PLATINUM_PLAN = "Platinum";

        public ContractDataAccess(MAPLE_TESTContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Contracts>> GetContracts()
        {
            if (dbContext != null)
            {
                return await dbContext.Contracts.ToListAsync();
            }
            return null;
        }
        public async Task<int> AddContract(Contracts contract)
        {
            if (dbContext != null)
            {
                contract.CoveragePlan = GetCoveragePlan(contract.CustomerCountry, contract.SaleDate);
                int? age = DateTime.Today.Year - contract.CustomerDob?.Year;
                contract.NetPrice = GetNetPrice(contract.CoveragePlan, contract.CustomerGender, age);

                await dbContext.Contracts.AddAsync(contract);
                await dbContext.SaveChangesAsync();
                return contract.ContractId;
            }
            return 0;
        }
        public async Task UpdateContract(Contracts contract)
        {
            if (dbContext != null)
            {
                contract.CoveragePlan = GetCoveragePlan(contract.CustomerCountry, contract.SaleDate);
                int? age = DateTime.Today.Year - contract.CustomerDob?.Year;
                contract.NetPrice = GetNetPrice(contract.CoveragePlan, contract.CustomerGender, age);

                dbContext.Contracts.Update(contract);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteContract(int? contractId)
        {
            int result = 0;

            if (dbContext != null)
            {
                var contract = await dbContext.Contracts.FirstOrDefaultAsync(x => x.ContractId == contractId);

                if (contract != null)
                {
                    //Delete that post
                    dbContext.Contracts.Remove(contract);

                    //Commit the transaction
                    result = await dbContext.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
        public string GetCoveragePlan(string country, DateTime? saleDate)
        {
            string coveragePlan = string.Empty;
            if (country == COUNTRY_USA)
            {
                if (saleDate >= Convert.ToDateTime("1/1/2009") && saleDate <= Convert.ToDateTime("1/1/2021"))
                    return GOLD_PLAN;
            }
            else if (country == COUNTRY_CANADA)
            {
                if (saleDate >= Convert.ToDateTime("1/1/2005") && saleDate <= Convert.ToDateTime("1/1/2023"))
                    return PLATINUM_PLAN;
            }
            else
            {
                if (saleDate >= Convert.ToDateTime("1/1/2001") && saleDate <= Convert.ToDateTime("1/1/2026"))
                    return SILVER_PLAN;
            }
            return coveragePlan;
        }

        public double? GetNetPrice(string coveragePlan, string gender, int? age)
        {
            double? netPrice = null;
            IRate rate;
            if (coveragePlan == GOLD_PLAN)
            {
                rate = new RateForGold();
                return rate.GetRate(gender, age);
            }
            if (coveragePlan == PLATINUM_PLAN)
            {
                rate = new RateForPlatinum();
                return rate.GetRate(gender, age);
            }
            if (coveragePlan == SILVER_PLAN)
            {
                rate = new RateForSilver();
                return rate.GetRate(gender, age);
            }

            return netPrice;
        }

        public interface IRate
        {
            public double? GetRate(string gender, int? age);
        }

        public class RateForGold : IRate
            {
            public double? GetRate(string gender, int? age)
            {
                double? netPrice = null;

                if (gender == "M")
                    netPrice = (age <= 40) ? 1000 : 2000;
                else if(gender == "F")
                    netPrice = (age <= 40) ? 1200 : 2500;
                return netPrice;
            }
            
        }
        public class RateForPlatinum : IRate
        {
            public double? GetRate(string gender, int? age)
            {
                double? netPrice = null;

                if (gender == "M")
                    netPrice = (age <= 40) ? 1900 : 2900;
                else if (gender == "F")
                    netPrice = (age <= 40) ? 2100 : 3200;
                return netPrice;
            }

        }
        public class RateForSilver : IRate
        {
            public double? GetRate(string gender, int? age)
            {
                double? netPrice = null;

                if (gender == "M")
                    netPrice = (age <= 40) ? 1500 : 2600;
                else if (gender == "F")
                    netPrice = (age <= 40) ? 1900 : 2800;
                return netPrice;
            }

        }
    }
}
