using System.Threading.Tasks;
using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DChess.Function;

public static class GameFunction
{
    public const int MaxAllowableDepth = 5;

    [FunctionName("Game")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req, ILogger log)
    
    {
        var request = new Request(
            ColourExtensions.AsColour(req.Query["colour"]),
            ColourExtensions.AsColour(req.Query["current-player"]),
            MoveExtensions.AsMove(req.Query["move"]));

        var board = new Board();
        board.SetStandardLayout();

        var game = new Game(board, new ExceptionErrorHandler(), MaxAllowableDepth);
        game.CurrentPlayer = request.CurrentPlayer;
        game.Make(request.Move);
        await game.MakeBestMove(request.CurrentPlayer.Invert());
        game.Board.RenderToText();

        return new OkObjectResult(new Response(
            game.LastMove.ToString(),
            game.Board.RenderToText()));
    }


    public record Request(Colour Colour, Colour CurrentPlayer, Move Move);

    public record Response(string LastMove, string Board);
}