﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ListSwype.Base;

namespace ListSwype.DTO
{
    public class UserDTO:BaseDTO
    {
      
        /// <summary>
        /// Gets or sets the FirstName field
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the LastName field
        /// </summary>
        [DataMember]
        public string LastName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the Email field
        /// </summary>
        [DataMember]
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ConnectionEmail field
        /// </summary>
        [DataMember]
        public string ConnectionEmail
        {
            get;
            set;
        }
    }
}