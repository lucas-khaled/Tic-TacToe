using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider), typeof(Renderer), typeof(Tile))]
public class TileSelector : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField]
    private Color selectedColor;

    private Renderer _renderer;
    private Tile _tile;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _tile = GetComponent<Tile>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_tile.IsOccupied || GameController.hasGameEnded) return;

        GameController.PieceMove(_tile);
        _renderer.material.color = _tile.ActualColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_tile.IsOccupied || GameController.hasGameEnded) return;

        _renderer.material.color = selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_tile.IsOccupied) return;

        _renderer.material.color = _tile.ActualColor;
    }
}
