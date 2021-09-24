using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class User
    {
        public User()
        {
            UserLots = new List<Lot>();
        }
        
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        
        public IEnumerable<Lot> UserLots { get; set; }
    }
}