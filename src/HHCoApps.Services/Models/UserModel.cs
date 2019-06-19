using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHCoApps.Services.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PassWord { get; set; }
        public byte Sex { get; set; }
        public DateTime DOB { get; set; }
        public string IDCard { get; set; }
        public string Address { get; set; }
        public int HomeTown { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
