using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSlice.Repository
{
    public interface IDisposedTracker
    {
        bool IsDisposed { get; set; }
    }
}