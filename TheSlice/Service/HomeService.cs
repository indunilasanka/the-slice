using TheSlice.Models;
using TheSlice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSlice.Service
{
    public class HomeService
    {
        private readonly HomeRepository _repository;

        public HomeService()
        {
            _repository = new HomeRepository();
        }
        //user for login
        public User CheckUserExits(String Email, String Password)
        {
            User UserDetails = new User();
            try
            {
                UserDetails = _repository.CheckUserExits(Email,Password);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //network issue
                UserDetails.UserId = -1;
                return UserDetails;
            }

            catch (Exception ex)
            {
                throw;
            }

            return UserDetails;
        }

        public int Register(User user)
        {
            int id = 0;
            try
            {
                id = _repository.Register(user);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //network issue
                id = -1;
                return id;
            }

            catch (Exception ex)
            {
                throw;
            }

            return id;
        }

        public int CheckEmailExists(String Email)
        {
            int id = 0;
            try
            {
                id = _repository.CheckEmailExists(Email);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //network issue
                id = -1;
                return id;
            }

            catch (Exception ex)
            {
                throw;
            }

            return id;
        }
    }
}