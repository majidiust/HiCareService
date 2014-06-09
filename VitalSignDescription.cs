using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiCareService
{
    public class VitalSignDescription
    {
        public int TypeId { get; set; }
        public String TypeName { get; set; }
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public String Value { get; set; }
        public String Description { get; set; }
    }
}