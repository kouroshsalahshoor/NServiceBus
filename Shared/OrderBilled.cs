using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; } = string.Empty;
    }
}
