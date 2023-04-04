using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainbayConsoleApp.DomainModels.Abstractions
{
    public abstract class RootEntity
    {
        public Guid Id { get; set; }
    }
}
