using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    public Tilemap highlightTilemap;
    public Tilemap tilemap;
    public TileBase highlightTile;
    public TileBase dirtTile;

    private BoxCollider2D tilemapBoundary;

    private Vector3Int tilePosition;
    private Vector3Int previousTilePosition;

    private GameObject player;

    public static TileHighlighter instance;

    private void Start()
    {
        instance = this;
        player = GameObject.Find("Player");
        tilemapBoundary = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

    }

    public void HighlightTilemap(Vector3 mousePosition, int maxReach)
    {
        tilePosition = tilemap.WorldToCell(mousePosition);
        Vector3 playerPosition = player.transform.position;
        Vector3Int playerTilePosition = tilemap.WorldToCell(playerPosition);
        if (tilePosition != previousTilePosition
            && highlightTilemap.GetTile(tilePosition) == null
            && tilemapBoundary.OverlapPoint(mousePosition)
            && Mathf.Abs(playerTilePosition.x - tilePosition.x) <= maxReach
            && Mathf.Abs(playerTilePosition.y - tilePosition.y) <= maxReach)
        {
            highlightTilemap.SetTile(previousTilePosition, null);
            highlightTilemap.SetTile(tilePosition, highlightTile);
            previousTilePosition = tilePosition;
        }
    }

    public void UseHoeAddDirt(Vector3 mousePosition)
    {
        tilePosition = tilemap.WorldToCell(mousePosition);
        tilemap.SetTile(tilePosition, dirtTile);
    }

    public void UseHoeRemoveDirt(Vector3 mousePosition)
    {
        tilePosition = tilemap.WorldToCell(mousePosition);
        tilemap.SetTile(tilePosition, null);
    }

    public void RemoveHighlight(Vector3 mousePosition)
    {
        tilePosition = tilemap.WorldToCell(mousePosition);
        highlightTilemap.SetTile(tilePosition, null);
    }
}
