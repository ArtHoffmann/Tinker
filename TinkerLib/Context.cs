using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerLib
{

   public interface Context: IDisposable
    {
        Values SaveTemp(Values tmp);
    }
}
