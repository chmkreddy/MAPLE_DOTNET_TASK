using System;
using System.Collections.Generic;

namespace MAPLE_INSURANCE_API.Models
{
    public partial class Contracts
    {
        public int ContractId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerCountry { get; set; }
        public DateTime? CustomerDob { get; set; }
        public DateTime? SaleDate { get; set; }
        public string CoveragePlan { get; set; }
        public double? NetPrice { get; set; }
    }
}
