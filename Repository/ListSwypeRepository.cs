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
        # region User
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
                    // Check for associations and delete them
                    var connection = context.users.Where(c => c.ConnectionEmail== email).FirstOrDefault();
                    connection.ConnectionEmail = string.Empty;
                    context.SaveChanges();

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
        /// Check if any user with the given email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true or false</returns>
        internal bool checkUser(string email)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                var chkUser = context.users.Where(u => u.Email == email).FirstOrDefault();
                if(chkUser!=null)
                {
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
        /// <returns>true/false</returns>

        internal string SaveUser(UserDTO user)
        {
            try
            {
                using (listswypeEntities context = new listswypeEntities())
                {
                    // Add new user
                    if(checkUser(user.Email))
                    {
                        return "User already exists";
                    }
                    user newUser = new user();
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.Email = user.Email;

                    context.users.Add(newUser);
                    context.SaveChanges();
                    return "success";
                }
            }
        
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Saves a user's connection
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        internal string SaveConnection(UserDTO connection)
        {
            try
            {

                using (listswypeEntities context = new listswypeEntities())
                {
                      
                    user user = context.users.Where(u => u.Email == connection.ConnectionEmail).FirstOrDefault();
                    // if user does not exist, nothing to do here

                    if (user == null)
                    {
                        return "User does not exist";
                    }
                    // if connection already exists, associate user and connection
                    user connUser = context.users.Where(u => u.Email == connection.Email).FirstOrDefault();
                    if (connUser != null)
                    {
                        user.ConnectionEmail = connection.Email;
                        connUser.ConnectionEmail = user.Email;
                        context.SaveChanges();
                        return "success";
                    }
                       

                    // Create a user record for the connection and then create the association
                    user newUser = new user();
                    newUser.FirstName = connection.FirstName;
                    newUser.LastName = connection.LastName;
                    newUser.Email = connection.Email;
                    newUser.ConnectionEmail = user.Email;

                    context.users.Add(newUser);
                    context.SaveChanges();

                    user.ConnectionEmail = connection.Email;
                    context.SaveChanges();
                    return "success";
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }
#endregion
        #region CommonList
        /// <summary>
        /// get all the common items
        /// </summary>
        /// <returns>list items</returns>

        internal List<ListItemDTO> GetAllCommonItems()
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 

                var list = context.commonlists;
                List<ListItemDTO> listItems = new List<ListItemDTO>();

                if (list != null)
                {
                    foreach (commonlist item in list)
                    {
                        listItems.Add(DTOConverters.ConvertToDTO(item));
                    }
                }

                return listItems;
            }
        }
        #endregion
        #region CustomItems
        /// <summary>
        /// get all the common items
        /// </summary>
        /// <returns>list items</returns>

        internal List<CustomItemDTO> GetCustomItemsByEmail(string email)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 
                string connectionEmail = context.users.Where(u => u.Email == email).FirstOrDefault().ConnectionEmail;
                var list = context.customlists.Where(c => c.useremail == email).Distinct();
                if ((!(string.IsNullOrEmpty(connectionEmail))))
                {
                    list = context.customlists.Where(c => c.useremail == email || c.useremail == connectionEmail).Distinct();
                }

                List<CustomItemDTO> listItems = new List<CustomItemDTO>();

                if (list != null)
                {
                    foreach (customlist item in list)
                    {
                        listItems.Add(DTOConverters.ConvertToDTO(item));
                    }
                }

                return listItems;
            }
        }
        /// <summary>
        /// saves a custom item
        /// </summary>
        /// <returns>string</returns>

        internal string SaveCustomItem(CustomItemDTO item)
        {
            try
            {
                using (listswypeEntities context = new listswypeEntities())
                {
                    
                    customlist customItem = new customlist();
                    customItem.useremail = item.useremail;
                    customItem.uniqueid = item.uniqueid;
                    customItem.itemimage = item.itemimage;
                    customItem.itemname = item.itemname;
                    context.customlists.Add(customItem);
                    context.SaveChanges();
                    return "success";
                }
            }

            catch (Exception ex)
            {
                return "failed";
                //return ex.Message + ',' + ex.InnerException.Message + ',' + ex.InnerException.InnerException.Message;
            }
        }

        #endregion
        #region UserList

         /// <summary>
        /// get all the common items
        /// </summary>
        /// <returns>list items</returns>

        internal List<UserListItemDTO> GetUserListItemsByEmail(string email)
        {
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 
                string connectionEmail = context.users.Where(u => u.Email == email).FirstOrDefault().ConnectionEmail;
                var list = context.userlists.Where(c => c.useremail == email).Distinct();
                if((!(string.IsNullOrEmpty(connectionEmail))))
                {
                    list = context.userlists.Where(c => c.useremail == email || c.useremail == connectionEmail).Distinct();
                }
                List<UserListItemDTO> listItems = new List<UserListItemDTO>();

                if (list != null)
                {
                    foreach (userlist item in list)
                    {
                        listItems.Add(DTOConverters.ConvertToDTO(item));
                    }
                }

                return listItems;
            }
        }

        /// <summary>
        /// saves a custom item
        /// </summary>
        /// <returns>true/false</returns>

        internal string SaveUserListItem(UserListItemDTO item)
        {
            try
            {
                using (listswypeEntities context = new listswypeEntities())
                {

                    userlist userlistitem = new userlist();
                    userlistitem.useremail = item.useremail;
                    userlistitem.itemid = item.itemid;
                    userlistitem.type = item.type;
                    context.userlists.Add(userlistitem);
                    context.SaveChanges();
                    return "Success";
                }
            }

            catch (Exception ex)
            {
                //return false;
                return ex.Message + ',' + ex.InnerException.Message + ',' + ex.InnerException.InnerException.Message;
            }
        }

        /// <summary>
        /// Deletes an item from a user's list
        /// </summary>
        /// <param name="uniqueid">unique id of the item</param>
        /// <returns>true/false</returns>

        internal string DeleteUserListItemByUniqueID(string uniqueid,string email)
        {
            
            using (listswypeEntities context = new listswypeEntities())
            {
                // Build the LINQ query 
                var item = context.userlists.Where(u => u.itemid == uniqueid && u.useremail == email).FirstOrDefault();
                
                if (item != null)
                {
                    context.userlists.Remove(item);
                    context.SaveChanges();
                    return "success";
                }
                else
                {
                    return "failed";
                }
            }
        }
        #endregion
    }
}


