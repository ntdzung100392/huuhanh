using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseApps.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}