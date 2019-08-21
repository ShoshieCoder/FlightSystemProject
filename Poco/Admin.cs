using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class Admin : IPoco, IUser
    {

        public Int64 Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            Admin other = obj as Admin;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public static bool operator ==(Admin p1, Admin p2)
        {
            if (ReferenceEquals(p1, null) && ReferenceEquals(p2, null))
            {
                return true;
            }

            if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
            {
                return false;
            }

            if (p1.Id == p2.Id)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Admin p1, Admin p2)
        {
            return !(p1 == p2);
        }
    }
}
