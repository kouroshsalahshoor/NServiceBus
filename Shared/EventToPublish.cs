using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class EventToPublish : IEvent
    {
        public string Id { get; set; }
    }
}
