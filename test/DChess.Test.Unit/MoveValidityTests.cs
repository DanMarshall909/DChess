namespace DChess.Test.Unit;

public class MoveValidityTests
{
    [Fact(DisplayName = "All enums have messages")]
    public void AllEnumsHaveMessages()
    {
        var values = Enum.GetValues<MoveValidity>();

        foreach (var value in values)
        {
            string message = value.Message();

            message.Should().NotBeNullOrWhiteSpace();
        }
    }
    
    [Fact(DisplayName = "Message for move validity works as expected")]
    public void OkMessageIsOk()
    {
        Ok.Message().Should().Be("OK");
    }
}