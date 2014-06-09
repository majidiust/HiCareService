using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiCareService
{
    public class CheckupDescription
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String NationaliId { get; set; }
        public String Date { get; set; }
        public Boolean Prescribe { get; set; }
        public Boolean Recommandation { get; set; }
        public String VitalCategories { get; set; }
    }
}