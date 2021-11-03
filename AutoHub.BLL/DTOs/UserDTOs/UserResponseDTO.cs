using System;

namespace AutoHub.BLL.DTOs.UserDTOs
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }

        public string UserRole { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime RegistrationTime { get; set; }
    }
}