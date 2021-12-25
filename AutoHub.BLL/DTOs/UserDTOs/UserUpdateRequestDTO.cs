namespace AutoHub.BLL.DTOs.UserDTOs
{
    public class UserUpdateRequestDTO
    {
        public int UserRoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}