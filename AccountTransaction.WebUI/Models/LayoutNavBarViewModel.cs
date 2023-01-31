using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountTransaction.WebUI.Models
{
    public class LayoutNavBarViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Enabled { get; set; }
        public string Image { get; set; } = "https://s.gravatar.com/avatar/aleatory?d=mm&s=45";
    }
}
