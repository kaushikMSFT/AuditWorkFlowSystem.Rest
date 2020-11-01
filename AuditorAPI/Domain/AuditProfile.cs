﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditorAPI.Domain
{
    public class AuditProfile: Entity
    {
        public int AuditProfileId { get; set; }
        
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
