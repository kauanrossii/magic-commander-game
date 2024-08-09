using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCommander.Domain._Shared
{
	public class Audit
	{
        public DateTimeOffset CreatedAt { get; set; } = new();
        public DateTimeOffset UpdatedAt { get; set; } = new();
    }
}
