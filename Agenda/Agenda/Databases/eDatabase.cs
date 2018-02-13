using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Databases
{
    public class eDatabase
    {
        [PrimaryKey, AutoIncrement]
        public int eId { get; set; }
        public string ex1 { get; set; }
        public string ex2 { get; set; }
        public string ex3 { get; set; }
        public string rep1 { get; set; }
        public string rep2 { get; set; }
        public string rep3 { get; set; }
    }
}
