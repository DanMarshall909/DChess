using System.Threading.Tasks;
using DChess.Core;
using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DChess.Function
{
    public static class GameFunction
    {
        [FunctionName("Game")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            var request = new Request(
                Colour: ColourExtensions.AsColour(req.Query["colour"]),
                CurrentPlayer: ColourExtensions.AsColour(req.Query["current-player"]),
                Move: MoveExtensions.AsMove(req.Query["move"]));

            var board = new Board();
            board.SetStandardLayout();

            var game = new Game(board, new ExceptionErrorHandler());
            game.CurrentPlayer = request.CurrentPlayer;
            game.Move(request.Move);
            await game.MakeBestMove(request.CurrentPlayer.Invert());
            game.Board.RenderToText();

            return new OkObjectResult(new Response(
                LastMove: game.LastMove.ToString(),
                Board: game.Board.RenderToText()));
        }

        public record Request(Colour Colour, Colour CurrentPlayer, Move Move);

        public record Response(string LastMove, string Board);
    }
}