using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSelector : MonoBehaviour
{
    [SerializeField]
    private Bot bot;
    [SerializeField]
    private GameObject selectionScreen;

    private Piece _pieceSelected = Piece.None;

    public void PlayerSelectX() 
    {
        _pieceSelected = Piece.X;
        bot.SetPiece(Piece.O);
        StartGame();
    }

    public void PlayerSelectO() 
    {
        _pieceSelected = Piece.O;
        bot.SetPiece(Piece.X);
        StartGame();
    }

    public void SwapSelection() 
    {
        bot.SetPiece(_pieceSelected);
        _pieceSelected = _pieceSelected.GetOpponentPiece();
    }

    private void StartGame() 
    {
        selectionScreen.SetActive(false);
        GameController.StartGame();
    }
}
