using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDUsingSQLCommand.Models.CRUDStoreProcedures
{
    public class CRUDStoreProcedures
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ClassM { get; set; }

        public IList<CRUDStoreProcedures> listData { get; set; }
    }
}