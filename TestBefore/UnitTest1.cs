
using Before.Pages;
using Bunit;

namespace TestBefore
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            using var ctx = new TestContext();
            var component = ctx.RenderComponent<Component>();

            var button = component.Find("button");
            var paragraph = component.Find("p");

            Assert.Equal("Increment", button.InnerHtml);
            Assert.Equal("Current count: 0", paragraph.InnerHtml);
        }
        [Fact]
        public void CounterComponentIncrementsCorrectly()
        {
            using var ctx = new TestContext();
            var component = ctx.RenderComponent<Component>();

            var button = component.Find("button");

            button.Click();

            var paragraph = component.Find("p");
            Assert.Equal("Current count: 1", paragraph.InnerHtml);
        }
    }
}