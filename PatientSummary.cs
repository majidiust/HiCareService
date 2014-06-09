using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiCareService
{
    public class PatientSummary
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String NationalCode { get; set; }
        public Boolean ValidationStatus { get; set; }
    }
}