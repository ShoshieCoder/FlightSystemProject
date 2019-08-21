using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class Customer : IPoco, IUser
    {
        public Customer()
        {
        }

        public Customer(string firstName, string lastName, string userName, string password, string address, string phoneNo, string creditNo)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Address = address;
            PhoneNo = phoneNo;
            CreditNo = creditNo;
        }

        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditNo { get; set; }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return this==other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Customer: {FirstName} {LastName}/n Address: {Address}/n Phone Number: {PhoneNo}";
        }

        public static bool operator ==(Customer p1, Customer p2)
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

        public static bool operator !=(Customer p1, Customer p2)
        {
            return !(p1 == p2);
        }
    }
}
