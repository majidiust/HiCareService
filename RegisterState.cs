using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiCareService
{
    public class RegisterState
    {
        public bool Result { get; set; }
        public String Message { get; set; }
        public String UserName { get; set; }
        public String CreatorId { get; set; }
        public int MessageId { get; set; }
    }
}