using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerLib
{

    class Context_Impl : Context
    {
        private IOT_ProjectEntities _context;
        public Context_Impl(IOT_ProjectEntities context)
        {
            _context = context;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public Values SaveTemp(Values tmp)
        {
         var save =  _context.Values.Add(tmp);
            _context.SaveChanges();
            return save;

        }
    }
}
