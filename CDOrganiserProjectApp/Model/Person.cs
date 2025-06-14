using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId {  get; set; }
        
        public Person(int pid, string fn, string ln, string un, string pw, int rid) 
        {
            PersonId = pid;
            FirstName = fn;
            LastName = ln;
            Username = un;
            Password = pw;
            RoleId = rid;

        }
    }
}
