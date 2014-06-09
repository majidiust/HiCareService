using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiCareService
{
    public class PrescribeResult
    {
        public String DoctorId { get; set; }
        public String DoctorFisrtName { get; set; }
        public String DoctorLastName { get; set; }
        public String Date { get; set; }
        public String Desc { get; set; }
        public String PatientId { get; set; }
        public String CheckupId { get; set; }
        public String CheckkupDate { get; set; }
        public String ResultMessage { get; set; }
        public int ResultId { get; set; }
        public Boolean ResultState { get; set; }
    }
}