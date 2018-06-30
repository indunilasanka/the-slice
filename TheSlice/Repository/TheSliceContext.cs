using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace TheSlice.Repository
{
    public class TheSliceContext : DbContext, IDisposedTracker
    {
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }

        public bool IsDisposed { get; set; }
    }
}