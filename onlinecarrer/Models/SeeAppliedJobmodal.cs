using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlinecarrer.Models
{
    public class SeeAppliedJobmodal
    {

        public int JobId { get; set; }
        public string Companyname { get; set; }
        public string Jobindustry { get; set; }
        public string Jobkeyskills { get; set; }
        public string Jobtitle { get; set; }

        public string Jobdescrp { get; set; }

        public DateTime Appdate { get; set; }

    }
}