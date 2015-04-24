using System;
using System.Collections.Generic;
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
                        UserDTO congressDto = DTOConverters.ConvertToDTO(user);

                        return congressDto;
                    }

                    return null;
            }
        }
        /// <summary>
        /// saves a user
        /// </summary>
        /// <param name="Id">Id of the user</param>
        /// <returns>user data</returns>

        internal bool SaveUser(UserDTO user)
        {
            try
            {
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
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}


