using Portfolio.Areas.News.Component;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio_UnitTests.Areas.News.Data.Post
{
    public class ContentParserTests
    {
        private readonly ContentParser contentParser;

        public ContentParserTests()
        {
            contentParser = new ContentParser();
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void MultiplyTest(string content, string expected)
        {
            string? res = contentParser.Invoke(content).EncodedContent.ToString();

            Assert.Equal(expected, res);
        } 

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] {"[br]", "<br>"};
            yield return new object[] { "HARD[h1]AHS[/h1]CORE", "HARD<h1>AHS</h1>CORE" };
            yield return new object[] { "[h1][h1]AHS[/h1]", "<h1>[h1]AHS</h1>" };
            yield return new object[] { "[script]AHS[/script]", "[script]AHS[/script]" };
        }

    }
}
