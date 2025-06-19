using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Formats
    {
        public int FormatId { get; set; }
        public string FormatName { get; set; }

        public Formats(int fid, string fn) 
        {
            FormatId = fid;
            FormatName = fn;
        
        }

    }
}
