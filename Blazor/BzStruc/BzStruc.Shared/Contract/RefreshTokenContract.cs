using System;
using System.Collections.Generic;
using System.Text;

namespace BzStruc.Shared.Contract
{

    public class RefreshTokenContract
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
