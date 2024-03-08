using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateAt { get; set; }
        //public int Rate { get; set; }
        public DateTime Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<Rent> Rents { get; set; }

    }
    public enum Gender
    {
        Female=1,
        Male=2
    }
}
