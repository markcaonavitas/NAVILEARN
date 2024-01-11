using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class SupplierChain
    {
        public string ProfileNumber { get; set; }
        public string ModelName { get; set; }
        public InitialSC.CurrentRating CurrentRating { get; set; }
        public InitialSC.Categories Category { get; set; }
        public string CompanyName { get; set; }
        public string FriendlyCompanyName { get; set; }
        public bool IsMemberApprovalRequired { get; set; }
    }
    
    public static class InitialSC
    {
        public enum Categories
        {
            Not_Implemented = 0,
            Manufacturers = 1,
            OEMs = 2,
            Distributors = 3,
            Dealers = 4
        }
        public enum CurrentRating
        {
            TAC440A = 7,
            TAC600A = 9,
            TSX440A = 12,
            TSX600A = 10,
        };
        public static List<SupplierChain> SupplierChains { get; set; }
    }
}
