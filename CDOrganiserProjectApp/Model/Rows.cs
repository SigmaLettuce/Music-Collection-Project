using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Model
{
    public class Rows
    {

        public int ShelfRowId { get; set; }
        public int ShelfRow { get; set; }
        public int ShelfTagId { get; set; }

        public Rows(int sroid, int sro, int staid)
        {
            ShelfRowId = sroid;
            ShelfRow = sro;
            ShelfTagId = staid;

        }
    }
}
