using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Color darkColor;
    [SerializeField]
    private Color lightColor;
    [SerializeField]
    private Renderer tileRenderer;
    [SerializeField]
    private GameObject X_Prefab;
    [SerializeField]
    private GameObject O_Prefab;

    public Piece OccupiedBy { get; private set; } = Piece.None;
    public bool IsOccupied => OccupiedBy != Piece.None;
    public Color ActualColor { get; private set; }

    public void Occupy(Piece piece, bool activatePrefab = true) 
    {
        OccupiedBy = piece;

        if(activatePrefab)
            ActivatePlayerPrefab();
    }

    public void DeOccupy(bool deActivatePrefab = true) 
    {
        OccupiedBy = Piece.None;

        if(deActivatePrefab)
            ActivatePlayerPrefab();
    }

    public void SetDarkColor() 
    {
        tileRenderer.material.color = darkColor;
        ActualColor = darkColor;
    }

    public void SetLightColor() 
    {
        tileRenderer.material.color = lightColor;
        ActualColor = lightColor;
    }

    private void ActivatePlayerPrefab() 
    {
        switch (OccupiedBy) 
        {
            case Piece.X:
                X_Prefab.SetActive(true);
                break;
            case Piece.O:
                O_Prefab.SetActive(true);
                break;
            case Piece.None:
                X_Prefab.SetActive(false);
                O_Prefab.SetActive(false);
                break;
        }
    }
}
