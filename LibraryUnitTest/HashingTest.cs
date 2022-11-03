using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryCommon;

namespace LibraryUnitTest
{
    [TestClass]
    public class HashingTest
    {
        [TestMethod]
        public void TestHashing()
        {
            string hash, salt;
            string password = "asdf";

            Hashing.GenerateSaltedHash(password, out hash, out salt);

            //Console.WriteLine(hash);
            //Console.WriteLine(salt);

            Assert.IsTrue(Hashing.VerifyPassword("asdf", hash, salt));
        }
    }
}
