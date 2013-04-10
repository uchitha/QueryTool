using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qt.Core;

namespace Qt.Web.Models
{
    public class QueryMainModel
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public List<BaseDto> AllGroups { get; set; }
    }
}