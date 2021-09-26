using blasa.access.management.Core.Domain.Common;
using blasa.access.management.Core.Domain.Entities;
using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace blasa.travel.Core.Domain.Entities
{
   public class Book : AuditableBaseEntity
    {



        //we can use also TimeSpan as type for DepartureTime

        public Travel Travel { get; set; }

        public bool? IsAccepted { get; set; } = false;
       
        public string Userid         { get; set; }

    }
}
