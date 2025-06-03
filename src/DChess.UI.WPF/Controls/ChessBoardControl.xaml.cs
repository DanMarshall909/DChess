using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using DChess.Core.Flyweights;
using DChess.Core.Game;
using static DChess.Core.Game.Piece;

namespace DChess.UI.WPF.Controls;

/// <summary>
/// Interaction logic for ChessBoardControl.xaml
/// </summary>
public partial class ChessBoardControl : UserControl
{
    private readonly Dictionary<Square, Border> _squareBorders = new();
    private readonly Dictionary<Square, TextBlock> _pieceTextBlocks = new();

    // Colors for the chess board
    private static readonly SolidColorBrush LightSquareColor = new(Colors.Wheat);
    private static readonly SolidColorBrush DarkSquareColor = new(Colors.SaddleBrown);
    private static readonly SolidColorBrush WhitePieceColor = new(Colors.White);
    private static readonly SolidColorBrush BlackPieceColor = new(Colors.Black);
    private static readonly SolidColorBrush HighlightColor = new(Color.FromArgb(128, 255, 255, 0)); // Semi-transparent yellow

    public ChessBoardControl()
    {
        InitializeComponent();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        BoardGrid.Children.Clear();
        _squareBorders.Clear();
        _pieceTextBlocks.Clear();

        // Create the chess board squares
        for (int rank = 8; rank >= 1; rank--)
        {
            for (char file = 'a'; file <= 'h'; file++)
            {
                var square = new Square(file, (byte)rank);
                var border = CreateSquareBorder(square);
                var textBlock = CreatePieceTextBlock();

                border.Child = textBlock;
                BoardGrid.Children.Add(border);

                _squareBorders[square] = border;
                _pieceTextBlocks[square] = textBlock;
            }
        }
    }

    private Border CreateSquareBorder(Square square)
    {
        bool isDarkSquare = (square.File - 'a' + square.Rank) % 2 == 1;
        
        return new Border
        {
            Background = isDarkSquare ? DarkSquareColor : LightSquareColor,
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(1)
        };
    }

    private TextBlock CreatePieceTextBlock()
    {
        return new TextBlock
        {
            FontSize = 32,
            FontFamily = new FontFamily("Segoe UI Symbol"),
            TextAlignment = TextAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
    }

    public void UpdateBoard(Board board)
    {
        foreach (var square in _squareBorders.Keys)
        {
            UpdateSquare(board, square);
        }
    }

    private void UpdateSquare(Board board, Square square)
    {
        var textBlock = _pieceTextBlocks[square];
        
        if (board.TryGetAtributes(square, out var attributes))
        {
            textBlock.Text = GetUnicodeCharForPiece(attributes.Kind, attributes.Colour);
            textBlock.Foreground = attributes.Colour == Colour.White ? WhitePieceColor : BlackPieceColor;
        }
        else
        {
            textBlock.Text = string.Empty;
        }
    }

    private string GetUnicodeCharForPiece(Kind type, Colour colour)
    {
        return type switch
        {
            Kind.Pawn => colour == Colour.White ? "♙" : "♟",
            Kind.Rook => colour == Colour.White ? "♖" : "♜",
            Kind.Knight => colour == Colour.White ? "♘" : "♞",
            Kind.Bishop => colour == Colour.White ? "♗" : "♝",
            Kind.Queen => colour == Colour.White ? "♕" : "♛",
            Kind.King => colour == Colour.White ? "♔" : "♚",
            _ => string.Empty
        };
    }

    public void HighlightSquare(Square square)
    {
        if (_squareBorders.TryGetValue(square, out var border))
        {
            bool isDarkSquare = (square.File - 'a' + square.Rank) % 2 == 1;
            border.Background = new LinearGradientBrush(
                isDarkSquare ? DarkSquareColor.Color : LightSquareColor.Color,
                HighlightColor.Color,
                new Point(0, 0),
                new Point(1, 1));
        }
    }

    public void ClearHighlights()
    {
        foreach (var kvp in _squareBorders)
        {
            var square = kvp.Key;
            var border = kvp.Value;
            
            bool isDarkSquare = (square.File - 'a' + square.Rank) % 2 == 1;
            border.Background = isDarkSquare ? DarkSquareColor : LightSquareColor;
        }
    }
}
