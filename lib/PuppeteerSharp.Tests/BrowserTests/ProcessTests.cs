using System.Threading.Tasks;
using PuppeteerSharp.Tests.Attributes;
using PuppeteerSharp.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace PuppeteerSharp.Tests.BrowserTests
{
    [Collection(TestConstants.TestFixtureCollectionName)]
    public class ProcessTests : PuppeteerBrowserBaseTest
    {
        public ProcessTests(ITestOutputHelper output) : base(output) { }

        [PuppeteerTest("browser.spec.ts", "Browser.process", "should return child_process instance")]
        [PuppeteerFact]
        public void ShouldReturnProcessInstance()
        {
            var process = Browser.Process;
            Assert.True(process.Id > 0);
        }

        [PuppeteerTest("browser.spec.ts", "Browser.process", "should not return child_process for remote browser")]
        [PuppeteerFact]
        public async Task ShouldNotReturnChildProcessForRemoteBrowser()
        {
            var browserWSEndpoint = Browser.WebSocketEndpoint;
            var remoteBrowser = await Puppeteer.ConnectAsync(
                new ConnectOptions { BrowserWSEndpoint = browserWSEndpoint }, TestConstants.LoggerFactory);
            Assert.Null(remoteBrowser.Process);
            remoteBrowser.Disconnect();
        }
    }
}