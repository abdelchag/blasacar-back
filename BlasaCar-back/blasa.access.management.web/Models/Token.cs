using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.access.management.web.Models
{
    public class Token : IToken
    {
        public DateTime Expiration { get; set; }
        string IToken.Token { get; set; }

    }
}
