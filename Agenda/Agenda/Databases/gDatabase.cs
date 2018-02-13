using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Databases
{
    public class gDatabase
    {
        [PrimaryKey, AutoIncrement]
        public int gId { get; set; }
        public string gName { get; set; }
        public string gAmount { get; set; }
        
    }
}
