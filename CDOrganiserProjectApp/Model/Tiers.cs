using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Tiers
    {
        public int TierId { get; set; }
        public char TierTag { get; set; }
        public int TierNumericalValue {  get; set; }

        public Tiers(int tid, char tta, int tnv) 
        { 
            TierId = tid;
            TierTag = tta;
            TierNumericalValue = tnv;
        }
    }
}
