using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Piece
{
    X,
    O,
    None
}

public static partial class Extensions 
{
    public static Piece GetOpponentPiece(this Piece actualPlayer) 
    {
        switch (actualPlayer)
        {
            case Piece.O:
                return Piece.X;
            case Piece.X:
                return Piece.O;
            default:
                return Piece.None;
        }
    }
}
