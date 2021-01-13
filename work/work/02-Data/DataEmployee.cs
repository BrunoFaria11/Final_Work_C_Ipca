using System;
using System.Collections.Generic;
using System.Text;

namespace work.Data
{
    class DataEmployee : Log
    {
        #region Properties
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public long contact { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime initWork { get; set; }
        public decimal salary { get; set; }
        #endregion
    }
  
}
