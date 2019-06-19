using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System;

namespace HHCoApps.Services.Implementation
{
    public class UserServices : IUserServices
    {
        //private IRepository<User> userRepository;
        //public UserServices()
        //{
        //    userRepository = new Repository<User>(dbContext);
        //}

        //public bool InsertUser(UserModel user)
        //{
        //    try
        //    {
        //        var entity = Mapper.Map<User>(user);
        //        entity.IsActive = true;
        //        entity.IsDeleted = false;
        //        userRepository.Insert(entity);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public UserModel GetUserByUserPassword(string user, string pass)
        //{
        //    try
        //    {
        //        User entity = userRepository.GetAll()
        //            .Where(u => u.UserName == user && u.PassWord == pass && !u.IsDeleted).FirstOrDefault();
        //        return Mapper.Map<UserModel>(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public bool InsertUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserByUserPassword(string user, string pass)
        {
            throw new NotImplementedException();
        }
    }
}
