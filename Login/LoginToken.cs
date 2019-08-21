using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class LoginToken<T> :ILoginToken where T:IUser
    {
        public T user { get; set; }
    }
}
