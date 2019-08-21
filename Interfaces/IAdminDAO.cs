using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public interface IAdminDAO:IBasicDB<Admin>
    {
        Admin GetAdminByUser(string user);
    }
}
