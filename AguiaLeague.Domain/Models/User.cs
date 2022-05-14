using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguiaLeague.Domain.Models
{
    public class User : Entity
    {
        public string nome { get; set; } = null;
        public string password { get; set; } = null;
        public string email { get; set; } = null;
        public string phone { get; set; } = null;

    }
}
