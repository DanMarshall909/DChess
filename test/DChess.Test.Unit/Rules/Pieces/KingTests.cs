namespace DChess.Test.Unit.Rules.Pieces;

public class KingTests : GameTestBase
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Kings can only move diagonally, vertically or horizontally")]
    public void kings_can_only_move_diagonally_vertically_or_horizontally()
    {
        WhiteKing.ShouldOnlyBeAbleToMoveTo(new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }.ToMoveOffsets(), ErrorHandler);
    }

    [Fact(DisplayName = "A piece cannot move such that the king is in check")]
    public void a_piece_cannot_move_such_that_the_king_is_in_check()
    {
        Sut.Board.Place(WhiteKing, a1);
        Sut.Board.Place(WhiteBishop, a2);
        Sut.Board.Place(BlackRook, a8);

        var result = Sut.Pieces[a2].CheckMove(b3, Sut);

        result.Validity.Should().Be(CannotMoveIntoCheck);
    }

    [Fact(DisplayName = "King can castle kingside when conditions are met")]
    public void king_can_castle_kingside_when_conditions_are_met()
    {
        // Arrange: Set up standard castling position
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h1);
        
        // Act: Attempt castling move (king moves two squares toward rook)
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be valid castling move
        result.Validity.Should().Be(Ok);
        result.Should().NotBeNull();
        
        // Additional assertions for castling move
        // TODO: Add assertions for rook movement when castling move type is implemented
    }

    [Fact(DisplayName = "King can castle queenside when conditions are met")]
    public void king_can_castle_queenside_when_conditions_are_met()
    {
        // Arrange: Set up standard castling position
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, a1);
        
        // Act: Attempt castling move (king moves two squares toward rook)
        var result = Sut.Pieces[e1].CheckMove(c1, Sut);
        
        // Assert: Should be valid castling move
        result.Validity.Should().Be(Ok);
        result.Should().NotBeNull();
        
        // Additional assertions for castling move
        // TODO: Add assertions for rook movement when castling move type is implemented
    }

    [Fact(DisplayName = "King cannot castle if king has already moved")]
    public void king_cannot_castle_if_king_has_already_moved()
    {
        // Arrange: Set up castling position
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h1);
        
        // Move king and then move back to simulate having moved
        var moveResult = Sut.Pieces[e1].CheckMove(f1, Sut);
        moveResult.Validity.Should().Be(Ok);
        Sut.MoveHandler.Make(moveResult.Move, Sut);
        
        // Move king back
        var moveBackResult = Sut.Pieces[f1].CheckMove(e1, Sut);
        moveBackResult.Validity.Should().Be(Ok);
        Sut.MoveHandler.Make(moveBackResult.Move, Sut);
        
        // Switch back to white's turn
        Sut.SwitchActivePlayer();
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be invalid because king has moved
        result.Validity.Should().Be(CastlingKingHasMoved);
    }

    [Fact(DisplayName = "King cannot castle if rook has already moved")]
    public void king_cannot_castle_if_rook_has_already_moved()
    {
        // Arrange: Set up castling position
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h1);
        
        // Move rook and then move back to simulate having moved
        var moveResult = Sut.Pieces[h1].CheckMove(g1, Sut);
        moveResult.Validity.Should().Be(Ok);
        Sut.MoveHandler.Make(moveResult.Move, Sut);
        
        // Move rook back
        var moveBackResult = Sut.Pieces[g1].CheckMove(h1, Sut);
        moveBackResult.Validity.Should().Be(Ok);
        Sut.MoveHandler.Make(moveBackResult.Move, Sut);
        
        // Switch back to white's turn
        Sut.SwitchActivePlayer();
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be invalid because rook has moved
        result.Validity.Should().Be(CastlingRookHasMoved);
    }

    [Fact(DisplayName = "King cannot castle when in check")]
    public void king_cannot_castle_when_in_check()
    {
        // Arrange: Set up castling position with king in check
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h1);
        Sut.Board.Place(BlackRook, e8); // Putting king in check
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be invalid because king is in check
        result.Validity.Should().Be(CastlingKingInCheck);
    }

    [Fact(DisplayName = "King cannot castle when passing through check")]
    public void king_cannot_castle_when_passing_through_check()
    {
        // Arrange: Set up castling position with king passing through check
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h1);
        Sut.Board.Place(BlackRook, f8); // Attacking f1 square
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be invalid because king passes through check
        result.Validity.Should().Be(CastlingKingPassesThroughCheck);
    }

    [Fact(DisplayName = "King cannot castle when squares between king and rook are occupied")]
    public void king_cannot_castle_when_squares_between_king_and_rook_are_occupied()
    {
        // Arrange: Set up castling position with piece in the way
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h1);
        Sut.Board.Place(WhiteBishop, f1); // Blocking castling
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be invalid because squares are occupied
        result.Validity.Should().Be(CastlingSquaresOccupied);
    }

    [Fact(DisplayName = "King cannot castle when not in starting position")]
    public void king_cannot_castle_when_not_in_starting_position()
    {
        // Arrange: Set up king not in starting position
        Sut.Board.Place(WhiteKing, e2); // Not in starting position
        Sut.Board.Place(WhiteRook, h1);
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e2].CheckMove(g2, Sut);
        
        // Assert: Should be invalid because king is not in starting position
        result.Validity.Should().Be(CastlingKingNotInStartingPosition);
    }

    [Fact(DisplayName = "King cannot castle when rook not in starting position")]
    public void king_cannot_castle_when_rook_not_in_starting_position()
    {
        // Arrange: Set up rook not in starting position
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhiteRook, h2); // Not in starting position
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e1].CheckMove(g1, Sut);
        
        // Assert: Should be invalid because rook is not in starting position
        result.Validity.Should().Be(CastlingRookNotInStartingPosition);
    }

    [Fact(DisplayName = "Black king can castle kingside when conditions are met")]
    public void black_king_can_castle_kingside_when_conditions_are_met()
    {
        // Arrange: Set up standard castling position for black
        Sut.Board.Place(BlackKing, e8);
        Sut.Board.Place(BlackRook, h8);
        Sut.SwitchActivePlayer(); // Switch to black's turn
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e8].CheckMove(g8, Sut);
        
        // Assert: Should be valid castling move
        result.Validity.Should().Be(Ok);
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Black king can castle queenside when conditions are met")]
    public void black_king_can_castle_queenside_when_conditions_are_met()
    {
        // Arrange: Set up standard castling position for black
        Sut.Board.Place(BlackKing, e8);
        Sut.Board.Place(BlackRook, a8);
        Sut.SwitchActivePlayer(); // Switch to black's turn
        
        // Act: Attempt castling move
        var result = Sut.Pieces[e8].CheckMove(c8, Sut);
        
        // Assert: Should be valid castling move
        result.Validity.Should().Be(Ok);
        result.Should().NotBeNull();
    }
}