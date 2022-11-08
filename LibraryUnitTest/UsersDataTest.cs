using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData;
using LibraryCommon;
using LibraryBusinessLogic;

namespace LibraryUnitTest
{
    [TestClass]
    public class UsersDataTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            // Create a user in the database
            int userID = UsersData.CreateUser(2, "test1@gmail.com", "1Fname", "1Lname", "SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==", "GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==");

            // Check if the user was added
            Assert.AreEqual(UsersData.GetUserByEmail("test1@gmail.com").UserID, userID);

            // Delete the user
            UsersData.DeleteUser(userID);
        }

        [TestMethod]
        public void UpdateUserPasswordTest()
        {
            // Create a user in the database
            int userID = UsersData.CreateUser(2, "test2@gmail.com", "2Fname", "2Lname", "SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==", "GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==");

            // Update the users password
            UsersBL.UpdateUserPassword("test2@gmail.com", "testpass");

            // Check if the password was updated
            int outputUserID, outputRoleID;
            Assert.IsTrue(UsersBL.VerifyPassword("test2@gmail.com", "testpass", out outputUserID, out outputRoleID));

            // Delete the user
            UsersData.DeleteUser(userID);
        }
    }
}