using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private Minimax _minimax = new Minimax();
    private Piece _actualPiece = Piece.O;

    private void Start()
    {
        GameController.onPieceMadeMove += OnPieceMadeMove;
        GameController.onGameStarted += OnPieceMadeMove;
    }

    public void SetPiece(Piece piece) 
    {
        _actualPiece = piece;
    }

    private void OnPieceMadeMove(Piece nextPiece) 
    {
        if (_actualPiece != nextPiece) return;

        Board actualBoard = GameController.GetActualBoard();
        Tile bestTile = _minimax.Evaluate(actualBoard, _actualPiece);
        GameController.PieceMove(bestTile);
    }
}

public class Minimax 
{
    private Piece myPiece;
    public Tile Evaluate(Board board, Piece actualPiece) 
    {
        if (actualPiece == Piece.None) 
            throw new Exception("Cannot pass 'Piece.None' to minimax evaluation");

        myPiece = actualPiece;

        int bestScore = int.MinValue;
        Tile bestTile = null;
        foreach(var row in board.GetTiles()) 
        {
            foreach(var tile in row) 
            {
                if (tile.IsOccupied) continue;

                tile.Occupy(myPiece, false);

                int score = EvaluateMove(board, myPiece.GetOpponentPiece());

                tile.DeOccupy(false);

                if(score > bestScore) 
                {
                    bestScore = score;
                    bestTile = tile;
                }
            }
        }

        return bestTile;
    }

    public int EvaluateMove(Board board, Piece actualPiece, int depth = 1)
    {
        bool isMyTurn = myPiece == actualPiece;

        if (GameController.HasWon(myPiece))
        {
            return 10 - depth;
        }

        if (GameController.HasWon(myPiece.GetOpponentPiece()))
            return -10 + depth;

        if (GameController.HasTie())
            return 0;

        int bestScore = (isMyTurn) ? int.MinValue : int.MaxValue;
        foreach (var row in board.GetTiles())
        {
            foreach (var tile in row)
            {
                if (tile.IsOccupied) continue;

                tile.Occupy(actualPiece, false);

                int score = EvaluateMove(board, actualPiece.GetOpponentPiece(), depth+1);

                tile.DeOccupy(false);

                bestScore = (isMyTurn) ?  Mathf.Max(score, bestScore) : Mathf.Min(score, bestScore);
            }
        }

        return bestScore;
    }
}