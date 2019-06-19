using HHCoApps.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHCoApps.Services.Interfaces
{
    public interface IUserServices
    {
        bool InsertUser(UserModel user);
        UserModel GetUserByUserPassword(string user, string pass);
    }
}
