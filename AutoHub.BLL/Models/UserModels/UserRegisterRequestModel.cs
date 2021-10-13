using System;
using AutoHub.DAL.Enums;

namespace AutoHub.BLL.Models.UserModels
{
    public class UserRegisterRequestModel
    {
        public UserRoleId UserRoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationTime { get; set; }
    }
}