using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public Role(int rid, string rn)
        {
            RoleId = rid;
            RoleName = rn;
 
        }

    }
}
