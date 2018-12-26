using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class AppTimer
    {
        DateTime? st, end;
        public void Start()
        {
            st = DateTime.Now;
            end = DateTime.Now;
        }

        public double Stop()
        {
            if (st == null)
                throw new Exception("Please start the timer first.");

            var span = DateTime.Now.Subtract(st.Value);
            st = null;
            return span.TotalSeconds;
        }
    }
}
