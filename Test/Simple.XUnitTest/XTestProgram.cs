using Furion.Xunit;
using Xunit.Abstractions;

[assembly: TestFramework("Simple.XUnitTest.XTestProgram", "Simple.XUnitTest")]
namespace Simple.XUnitTest;
public class XTestProgram : TestStartup
{
    public XTestProgram(IMessageSink messageSink) : base(messageSink)
    {
        Serve.Run(silence: true);
    }
}
