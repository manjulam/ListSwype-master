using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListSwype.DTO;
using ListSwype.Models;

namespace ListSwype.Repository
{
    public class ListSwypeRepository
    {

        /// <summary>
        /// gets the user by ID
        /// </summary>
        /// <param name="Id">Id of the user</param>
        /// <returns>user data</returns>

        internal UserDTO GetUserbyId(int Id)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                    // Build the LINQ query 

                    var user = context.users.Where(u => u.ID == Id).FirstOrDefault();

                    if (user != null)
                    {
                        UserDTO userDto = DTOConverters.ConvertToDTO(user);

                        return userDto;
                    }

                    return null;
            }
        }
        /// <summary>
        /// gets the user by email
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <returns>user data</returns>

        internal UserDTO GetUserbyEmail(string email)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 

                var user = context.users.Where(u => u.Email == email).FirstOrDefault();

                if (user != null)
                {
                    UserDTO userDto = DTOConverters.ConvertToDTO(user);

                    return userDto;
                }

                return null;
            }
        }
        /// <summary>
        /// Deletes the user by ID
        /// </summary>
        /// <param name="Id">Id of the user</param>
        /// <returns>true/false</returns>

        internal bool DeleteUserByID(int Id)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 

                var user = context.users.Where(u => u.ID == Id).FirstOrDefault();

                if (user != null)
                {
                    // Once other related tables are added, remove associations before attempting to delete user
                    context.users.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Deletes the user by Email
        /// </summary>
        /// <param name="Email">Email of the user</param>
        /// <returns>true/false</returns>

        internal bool DeleteUserByEmail(string email)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 

                var user = context.users.Where(u => u.Email == email).FirstOrDefault();

                if (user != null)
                {
                    // Once other related tables are added, remove associations before attempting to delete user
                    context.users.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// saves a user
        /// </summary>
        /// <param name="Id">Id of the user</param>
        /// <returns>user data</returns>

        internal string SaveUser(UserDTO user)
        {
            try
            {
                int userId = 0;

                using (listswypeEntities context = new listswypeEntities())
                {
                    // Add new user
                    if (user.ID == 0)
                    {
                        user newUser = new user();
                        newUser.FirstName = user.FirstName;
                        newUser.LastName = user.LastName;
                        newUser.Email = user.Email;

                        context.users.Add(newUser);
                        userId = newUser.ID;
                    }
                    context.SaveChanges();
                    return "Success";
                }
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder error = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    error.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error.AppendFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return error.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}


