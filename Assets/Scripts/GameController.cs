using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameController 
{
    public static Action<Piece> onPieceMadeMove;
    public static Action<Piece> onGameEnded;
    public static Action<Piece> onGameStarted;

    private static Piece actualPiece = Piece.X;
    private static Board actualBoard;

    public static bool hasGameEnded { get; private set; } = false;

    public static void StartGame() 
    {
        actualBoard.Clear();
        hasGameEnded = false;
        onGameStarted?.Invoke(actualPiece);
    }

    public static void PieceMove(Tile selected) 
    {
        selected.Occupy(actualPiece);
        HandleEndGame();

        if (hasGameEnded) 
            return;

        actualPiece = actualPiece.GetOpponentPiece();
        onPieceMadeMove.Invoke(actualPiece);
    }

    public static void HandleEndGame() 
    {
        if (HasWon(actualPiece)) 
        {
            EndGame(actualPiece);
            return;
        }

        if (HasTie()) 
        {
            EndGame(Piece.None);
            return;
        }
    }

    public static bool HasTie() 
    {
        return actualBoard.GetTiles().All(row => row.All(t => t.IsOccupied));
    }

    public static bool HasWon(Piece checkedPiece) 
    {
        return WonHorizontal(checkedPiece) || WonVertical(checkedPiece) || WonDiagonal(checkedPiece);
    }

    private static bool WonHorizontal(Piece checkedPiece) 
    {
        return actualBoard.GetTiles().Any(row => row.Count(t => t.OccupiedBy == checkedPiece) == row.Count);
    }

    private static bool WonVertical(Piece checkedPiece) 
    {
        return actualBoard.GetTilesTransposed().Any(row => row.All(t => t.OccupiedBy == checkedPiece));
    }

    private static bool WonDiagonal(Piece checkedPiece)
    {
        return actualBoard.GetDiagonals().Any(diag => diag.All(t => t.OccupiedBy == checkedPiece));
    }

    public static void SetActualBoard(Board board) 
    {
        actualBoard = board;
    }

    public static Board GetActualBoard() 
    {
        return actualBoard;
    }

    private static void EndGame(Piece winner) 
    {
        hasGameEnded = true;
        onGameEnded?.Invoke(winner);
    }
}
