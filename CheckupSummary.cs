using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiCareService
{
    public class CheckupSummary
    {
        public int Id { get; set; }
        public String NationalCode { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Date { get; set; }
        public Boolean Prescribe { get; set; }
        public Boolean Recommandation { get; set; }
    }
}