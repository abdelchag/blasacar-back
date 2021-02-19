using System;
using System.Collections.Generic;
using System.Text;

namespace blasa.travel.Core.Domain.Common
{
    public abstract class BaseEntity:AuditableBaseEntity
    {
        public int Id { get; set; }
    }
}  
