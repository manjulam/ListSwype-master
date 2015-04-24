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
        /// Utility method to convert a Congress EF instance into an CongressDTO instance.
        /// </summary>
        /// <param name="congress">Contains the Congress EF instance to be converted.</param>
        /// <returns>Returns the converted CongressDTO instance.</returns>
        public static UserDTO ConvertToDTO(user user)
        {

            UserDTO result = new UserDTO();
            result.CopyFromModelEntity(user);

            return result;
        }

       
    }
}