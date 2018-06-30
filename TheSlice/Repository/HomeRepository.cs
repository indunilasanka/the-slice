using TheSlice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TheSlice.Repository
{
    public class HomeRepository : RepositoryBase<TheSliceContext>
    {
        public HomeRepository()
        {
        }

        public User CheckUserExits(String Email, String Password)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            User UserDetails = new User();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    UserDetails = db.Query<User>("exec slice_loginUser " + "@Email,@Password",
                            new { Email = Email, Password = Password }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return UserDetails;
        }


        public int Register(User user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int UserId = 0;

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    UserId = db.Query<int>("exec slice_Register " + "@Password,@Email,@FullName,@Address",
                            new {  Password = user.Password, Email = user.Email, FullName =user.FullName, Address= user.Address}).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return UserId;
        }

        public int CheckEmailExists(String Email)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int UserId = 0;

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    UserId = db.Query<int>("exec slice_CheckMail " + "@Email",
                            new { Email = Email }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return UserId;
        }

    }
}

