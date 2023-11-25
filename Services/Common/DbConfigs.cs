using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class DbConfigs
    {
        public string Conn { get; set; }
        public int DbType { get; set; }
        public string ConfigId { get; set; }
        public bool IsAutoCloseConnection { get; set; }
        public string DbName { get; set; }
    }

}
