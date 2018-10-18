

namespace Capstone.Web
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string HashedPassword { get; set; }

        public string GetRoleName(int RoleId)
        {
            string roleName = "";
            if(RoleId == 1)
            {
                roleName = "Student";
            }
            else if(RoleId == 2)
            {
                roleName = "Teacher";
            }

            return roleName;
        }
    }
}