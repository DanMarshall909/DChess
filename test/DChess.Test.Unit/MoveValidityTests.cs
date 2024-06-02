using DChess.Core.Moves;

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

            Assert.NotNull(message);
            Assert.NotEmpty(message);
        }
    }
}