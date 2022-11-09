using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData;
using LibraryCommon;
using LibraryBusinessLogic;

namespace LibraryUnitTest
{
    [TestClass]
    public class UsersDataTest
    {
        private const string hash = "SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==";
        private const string salt = "GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==";

        [TestMethod]
        public void CreateAndDeleteUserTest()
        {
            // Create a user in the database
            int _userID = UsersData.CreateUser(2, "test1@gmail.com", "1Fname", "1Lname", hash, salt);

            // Check if the user was added
            Assert.AreEqual(UsersData.GetUserByEmail("test1@gmail.com").UserID, _userID);

            // Delete the user
            UsersData.DeleteUser(_userID);

            // Check if the user was added
            Assert.IsNull(UsersData.GetUserByEmail("test1@gmail.com"));
        }

        [TestMethod]
        public void UpdateAndVerifyPasswordTest()
        {
            // Create a user in the database
            int _userID = UsersData.CreateUser(2, "test2@gmail.com", "2Fname", "2Lname", hash, salt);

            // Update the users password
            UsersBL.UpdateUserPassword("test2@gmail.com", "testpass");

            // Check if the password was updated
            int outputUserID, outputRoleID;
            Assert.IsTrue(UsersBL.VerifyPassword("test2@gmail.com", "testpass", out outputUserID, out outputRoleID));

            // Delete the user
            UsersData.DeleteUser(_userID);
        }

        [TestMethod]
        public void VerifyEmailTest()
        {
            // Create a user in the database
            int _userID = UsersData.CreateUser(2, "test3@gmail.com", "3Fname", "3Lname", hash, salt);

            // Check if the output of VerifyEmail
            Assert.IsTrue(UsersBL.VerifyEmail("unused@email.com"));
            Assert.IsFalse(UsersBL.VerifyEmail("test3@gmail.com"));

            // Delete the user
            UsersData.DeleteUser(_userID);
        }
    }
}