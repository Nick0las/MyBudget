using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Models
{
    internal class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public bool? Child { get; set; }
    }
}
