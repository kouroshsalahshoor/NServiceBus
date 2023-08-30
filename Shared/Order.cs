using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Order : ICommand
    {
        public string Id { get; set; } = string.Empty;
    }
}
