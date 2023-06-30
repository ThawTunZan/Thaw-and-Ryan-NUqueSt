using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    public Tilemap highlightTilemap;
    public Tilemap tilemap;
    public TileBase highlightTile;
    public TileBase dirtTile;

    private Vector3Int previousTilePosition;

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = tilemap.WorldToCell(mousePosition);

        if (tilePosition != previousTilePosition)
        {
            highlightTilemap.SetTile(previousTilePosition, null);
            highlightTilemap.SetTile(tilePosition, highlightTile);
            previousTilePosition = tilePosition;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            tilemap.SetTile(tilePosition, dirtTile);
        }
    }
}
