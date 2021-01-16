using System;
using System.Collections.Generic;
using System.Text;

namespace work.Data
{
    class Log
    {
        #region Properties
        public DateTime Added { get; set; }
        public long AddedBy { get; set; }
        public DateTime Updated { get; set; }
        public long UpdatedBy { get; set; }
        #endregion
    }
}
