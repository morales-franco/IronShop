using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Base
{
    public interface IPageEntity
    {
        [JsonIgnore]
        int TotalRows { get; set; }
    }
}
