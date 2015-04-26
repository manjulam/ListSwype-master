using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListSwype.DTO;
using ListSwype.Base;
using ListSwype.Models;

namespace ListSwype.Repository
{
    /// <summary>
    /// Provides functionality for converting from an EF model object into the corresponding DTO.
    /// </summary>
    public static class DTOConverters
    {
        /// <summary>
        /// Utility method to convert a user EF instance into an UserDTO instance.
        /// </summary>
        /// <param name="congress">Contains the user EF instance to be converted.</param>
        /// <returns>Returns the converted UserDTO instance.</returns>
        public static UserDTO ConvertToDTO(user user)
        {

            UserDTO result = new UserDTO();
            result.CopyFromModelEntity(user);

            return result;
        }

        /// <summary>
        /// Utility method to convert a commonlist EF instance into an ListItemDTO instance.
        /// </summary>
        /// <param name="congress">Contains the commonlist EF instance to be converted.</param>
        /// <returns>Returns the converted ListItemDTO instance.</returns>
        public static ListItemDTO ConvertToDTO(commonlist item)
        {

            ListItemDTO result = new ListItemDTO();
            result.CopyFromModelEntity(item);

            return result;
        }

        /// <summary>
        /// Utility method to convert a customlist EF instance into an CustomItemDTO instance.
        /// </summary>
        /// <param name="congress">Contains the customlist EF instance to be converted.</param>
        /// <returns>Returns the converted CustomItemDTO instance.</returns>
        public static CustomItemDTO ConvertToDTO(customlist item)
        {

            CustomItemDTO result = new CustomItemDTO();
            result.CopyFromModelEntity(item);

            return result;
        }

        /// <summary>
        /// Utility method to convert a userlist EF instance into an UserListItemDTO instance.
        /// </summary>
        /// <param name="congress">Contains the userlist EF instance to be converted.</param>
        /// <returns>Returns the converted UserListItemDTO instance.</returns>
        public static UserListItemDTO ConvertToDTO(userlist item)
        {

            UserListItemDTO result = new UserListItemDTO();
            result.CopyFromModelEntity(item);

            return result;
        }
    }
}