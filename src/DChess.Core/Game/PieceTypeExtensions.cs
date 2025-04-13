namespace DChess.Core.Game;

public static class Weights
{
    public static class Material
    {
        public const int Pawn = 1;
        public const int Knight = 3;
        public const int Bishop = 3;
        public const int Rook = 5;
        public const int Queen = 9;
        public const int King = 0; // King is not assigned a value in chess
    }

    public static class GameState
    {
        public const int Stalemate = 0;
        public const int Checkmate = 1_000_000;
        public const int Check = 10;
        public const int InPlay = 0;
        public const int Default = 0;
    }
}

public static class PieceKindExtensions
{
    public static int Value(this Kind pieceKind) => pieceKind switch
    {
        Kind.Pawn => Weights.Material.Pawn,
        Kind.Knight => Weights.Material.Knight,
        Kind.Bishop => Weights.Material.Bishop,
        Kind.Rook => Weights.Material.Rook,
        Kind.Queen => Weights.Material.Queen,
        Kind.King => Weights.Material.King,
        _ => throw new ArgumentOutOfRangeException(nameof(pieceKind), pieceKind, null)
    };
}