using AuthentificationLibrary;

namespace Testing
{
    public class Hasher
    {
        [Fact]
        public void TestHash()
        {
            var hash = PasswordHasher.Hash("string", "string");
            var mustBe = "5e2b66ce4f42f50784824384c3bfe6872c21c08a9cbfb1397e6e34d7fac53997";
            Assert.Equal(mustBe, hash);
        }
        [Fact]
        public void CanCheckPassword1()
        {
            var hash = "5e2b66ce4f42f50784824384c3bfe6872c21c08a9cbfb1397e6e34d7fac53997";

            var result = PasswordHasher.CheckPassword("string", "string", hash);
            Assert.True(result);

        }
        [Fact]
        public void CanCheckPassword2()
        {
            var hash = "6e36dca5cf5a18afd0da996cc4003ae7d79019566228071b7f7d498b125fbe2b";

            var result = PasswordHasher.CheckPassword("string", "mLyExS4mgx8fDIALqvujyQ", hash);
            Assert.True(result);

        }
        [Fact]
        public void CanCheckIfWrongPassword()
        {
            var hash = "falsehash";

            var result = PasswordHasher.CheckPassword("string", "string", hash);
            Assert.False(result);
        }



    }
}