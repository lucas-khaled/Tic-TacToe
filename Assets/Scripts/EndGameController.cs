using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text winnerText;
    [SerializeField]
    private GameObject endGamePanel;
    [SerializeField]
    private SideSelector sideSelector;

    private const string WIN_TEXT = "{0} wins!";
    private const string DRAW_TEXT = "That's a draw!";

    private void Start()
    {
        GameController.onGameEnded += OnGameEnded;
    }

    private void OnGameEnded(Piece winnerPiece) 
    {
        endGamePanel.SetActive(true);

        if (winnerPiece == Piece.None)
            winnerText.text = DRAW_TEXT;
        else
            winnerText.text = string.Format(WIN_TEXT, winnerPiece);
    }

    public void RestartGame() 
    {
        endGamePanel.SetActive(false);
        sideSelector.SwapSelection();

        GameController.StartGame();
    }
}
