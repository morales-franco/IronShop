﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Base
{
    public interface IAuditable
    {
         string AuditUserName { get; set; }
         DateTime AuditDate { get;  set; }
    }
}
