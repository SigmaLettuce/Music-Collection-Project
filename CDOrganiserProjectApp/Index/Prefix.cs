﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDOrganiserProjectApp.Index
{
    public class Prefix
    {

        // Rudimentary prefixes ≻ Commands

        // Globally used prefix

            public const string @view = "view";

 
        // Admin only prefix

            public const string @up = "up";
            public const string @ins = "ins";
            public const string @del = "del";
            public const string @lost = "lost";

                // Account related prefix

                    public const string @create = "create";
                    public const string @log = "log";

    }
}
