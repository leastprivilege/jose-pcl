using System.Diagnostics;
using JosePCL;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace win8_tests
{
    [TestClass]
    public class CompatibilityTestSuite
    {
        private static readonly byte[] shaKey = { 97, 48, 97, 50, 97, 98, 100, 56, 45, 54, 49, 54, 50, 45, 52, 49, 99, 51, 45, 56, 51, 100, 54, 45, 49, 99, 102, 53, 53, 57, 98, 52, 54, 97, 102, 99 };

        [TestMethod]
        public void DecodePlaintext()
        {
            //given
            string token = "eyJhbGciOiJub25lIn0.eyJoZWxsbyI6ICJ3b3JsZCJ9.";

            //when
            string test = Jwt.Decode(token, null);

            Debug.WriteLine("test = {0}", test);

            //then
            Assert.AreEqual(@"{""hello"": ""world""}", test);
        }

        [TestMethod]
        public void EncodePlaintext()
        {
            //given
            string payload = @"{""hello"" : ""world""}";

            //when
            string test = Jwt.Encode(payload, JwsAlgorithms.None, null);

            Debug.WriteLine("test = {0}", test);

            //then
            Assert.AreEqual("eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJoZWxsbyIgOiAid29ybGQifQ.", test);
        }


        [TestMethod]
        public void DecodeHS256()
        {
            //given
            string token = "eyJhbGciOiJIUzI1NiIsImN0eSI6InRleHRcL3BsYWluIn0.eyJoZWxsbyI6ICJ3b3JsZCJ9.chIoYWrQMA8XL5nFz6oLDJyvgHk2KA4BrFGrKymjC8E";

            //when
            string test = Jwt.Decode(token, shaKey);

            //then
            Assert.AreEqual(@"{""hello"": ""world""}", test);
        }

        [TestMethod]
        public void DecodeHS384()
        {
            //given
            string token = "eyJhbGciOiJIUzM4NCIsImN0eSI6InRleHRcL3BsYWluIn0.eyJoZWxsbyI6ICJ3b3JsZCJ9.McDgk0h4mRdhPM0yDUtFG_omRUwwqVS2_679Yeivj-a7l6bHs_ahWiKl1KoX_hU_";

            //when
            string test = Jwt.Decode(token, shaKey);

            //then
            Assert.AreEqual(@"{""hello"": ""world""}", test);
        }
    }
}
