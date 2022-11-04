using LibraryData;
using LibraryCommon;
using System;

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

        // Used by the register method to check for duplicate emails
        // Returns true if the email is valid
        public static bool VerifyEmail(string email)
        {
            User u = UsersData.GetUserByEmail(email);
            if (u == null)
            {
                return false;
            }
            else if (u.UserID == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method used for checking entered password when logging in
        // Returns true if the password is valid
        public static bool VerifyPassword(string email, string enteredPassword)
        {
            User u = UsersData.GetUserByEmail(email);
            if (u == null)
            {
                return false;
            }
            else if (u.UserID == 0)
            {
                return false;
            }
            else if (Hashing.VerifyPassword(enteredPassword, u.HashedPassword, u.Salt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
