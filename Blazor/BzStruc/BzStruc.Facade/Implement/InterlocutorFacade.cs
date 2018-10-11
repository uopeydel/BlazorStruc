using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Facade.Implement
{
    public class InterlocutorFacade : IInterlocutorFacade
    {
        public string AddA(string nona)
        {
            return nona + "_a";
        }
    }

    public interface IInterlocutorFacade
    {
        string AddA(string nona);
    }
}
