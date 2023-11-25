using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{

    [SugarTable("Skills")]
    public class Skills
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SiID { get; set; }

        public string Description { get; set; }

        public string RequiresTicket { get; set; }
    }
}
