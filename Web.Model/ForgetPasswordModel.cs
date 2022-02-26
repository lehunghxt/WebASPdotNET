﻿namespace Web.Model
{
    using System;
    public class ForgetPasswordModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public int ApplicationId { get; set; }
    }
}
