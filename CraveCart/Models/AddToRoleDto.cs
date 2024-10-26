using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CraveCart.Models
{
    public class AddToRoleDto
    {
        public String email { get; set; }
        public List<String> roles { get; set; }
        public String selectedRole { get; set; }
    }
}