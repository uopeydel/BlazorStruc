using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Server.Service
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton>
            lazy =
            new Lazy<Singleton>
                (() => new Singleton());

        public static Singleton Instance { get { return lazy.Value; } }

        private Singleton()
        {

        }

        private DateTime StartupDate { get; set; }
        public void SetStartupDateTime(DateTime dateTime)
        {
            StartupDate = dateTime;
        }

        public DateTime GetStartupDateTime()
        {
            return StartupDate;
        }
    }
}
