using LibraryData;
using LibraryCommon;

namespace LibraryBusinessLogic
{
    public static class UsersBL
    {
        public static int CreateUser(int roleID, string email, string firstName, string lastName, string password)
        {
            string hashedPassword, salt;
            Hashing.GenerateSaltedHash(password, out hashedPassword, out salt);
            return UsersData.CreateUser(roleID, email, firstName, lastName, hashedPassword, salt);
        }

        public static int GetUsers()
        {
            return 0;
        }

        public static int GetUserByEmail()
        {
            return 0;
        }
    }
}
