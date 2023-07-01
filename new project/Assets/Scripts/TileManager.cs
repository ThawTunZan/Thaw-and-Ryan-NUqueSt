using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;
using static UnityEditor.Progress;

public class TileManager : MonoBehaviour
{
    public Tilemap highlightTilemap;
    public Tilemap dirtTilemap;
    public Tilemap seedTilemap;
    public TileBase highlightTile;
    public TileBase seedHighlightTile;
    public TileBase dirtTile;

    public static TileManager instance;

    public List<TileBase> seedTiles = new List<TileBase>();
    private Dictionary<string, TileBase> seedTileToTileBaseDict = new Dictionary<string, TileBase>();

    public List<Vector3Int> seedPositions = new List<Vector3Int>();
    public List<string> seedNames = new List<string>();
    public List<float> seedNextGrowths = new List<float>();

    private BoxCollider2D tilemapBoundary;

    private Vector3Int tilePosition;
    private Vector3Int previousTilePosition;

    private GameObject player;

    private void Start()
    {
        instance = this;
        tilemapBoundary = highlightTilemap.GetComponent<BoxCollider2D>();
        dirtTilemap = GameObject.Find("DirtMap").GetComponent<Tilemap>();
        seedTilemap = GameObject.Find("SeedMap").GetComponent<Tilemap>();
        player = GameObject.Find("Player");

        seedPositions = GameManager.instance.seedPositions;
        seedNames = GameManager.instance.seedNames;
        seedNextGrowths = GameManager.instance.seedNextGrowths;

        foreach (TileBase tileBase in seedTiles)
        {
            AddTileBase(tileBase);
        }
    }

    private void Update()
    {
        LoadSeedTile();
    }

    private void LoadSeedTile()
    {
        for (int i = 0; i < seedPositions.Count; i++)
        {
            dirtTilemap.SetTile(seedPositions[i], dirtTile);
            seedTilemap.SetTile(seedPositions[i], seedTileToTileBaseDict[seedNames[i]]);
        }
    }

    private void AddTileBase(TileBase tileBase)
    {
        if (!seedTileToTileBaseDict.ContainsKey(tileBase.name))
        {
            seedTileToTileBaseDict.Add(tileBase.name, tileBase);
        }
    }

    public TileBase GetTileBaseByName(string key)
    {
        if (seedTileToTileBaseDict.ContainsKey(key))
        {
            return seedTileToTileBaseDict[key];
        }
        return null;
    }

    public void HighlightTilemap(Vector3 mousePosition, int maxReach, string highlightType)
    {
        tilePosition = dirtTilemap.WorldToCell(mousePosition);
        Vector3 playerPosition = player.transform.position;
        Vector3Int playerTilePosition = dirtTilemap.WorldToCell(playerPosition);
        if (tilePosition != previousTilePosition
            && highlightTilemap.GetTile(tilePosition) == null
            && tilemapBoundary.OverlapPoint(mousePosition)
            && Mathf.Abs(playerTilePosition.x - tilePosition.x) <= maxReach
            && Mathf.Abs(playerTilePosition.y - tilePosition.y) <= maxReach)
        {
            highlightTilemap.SetTile(previousTilePosition, null);
            if (highlightType == "Hoe")
            {
                if (seedTilemap.GetTile(tilePosition))
                {
                    string seedTileName = seedTilemap.GetTile(tilePosition).name;
                    if (seedTileName[seedTileName.Length - 1] == '5')
                    {
                        highlightTilemap.SetTile(tilePosition, seedHighlightTile);
                    }
                }
                else
                {
                    highlightTilemap.SetTile(tilePosition, highlightTile);
                }
            }
            else if (highlightType == "Seed" && dirtTilemap.GetTile(tilePosition))
            {
                highlightTilemap.SetTile(tilePosition, highlightTile);
            }
            previousTilePosition = tilePosition;
        }
        //else if (highlightTilemap.GetTile(tilePosition).name == "highlightedTile"
        //    && (Mathf.Abs(playerTilePosition.x - tilePosition.x) > maxReach
        //    || Mathf.Abs(playerTilePosition.y - tilePosition.y) > maxReach))
        //{
        //    highlightTilemap.SetTile(tilePosition, null);
        //}
    }

    public void UseHoeAddDirt(Vector3 mousePosition)
    {
        tilePosition = dirtTilemap.WorldToCell(mousePosition);
        if (highlightTilemap.GetTile(tilePosition))
        {
            dirtTilemap.SetTile(tilePosition, dirtTile);
        }
    }

    public void UseHoeRemoveDirt(Vector3 mousePosition, int maxReach)
    {
        tilePosition = dirtTilemap.WorldToCell(mousePosition);
        Vector3 playerPosition = player.transform.position;
        Vector3Int playerTilePosition = dirtTilemap.WorldToCell(playerPosition);
        if (highlightTilemap.GetTile(tilePosition) 
            && Mathf.Abs(playerTilePosition.x - tilePosition.x) <= maxReach
            && Mathf.Abs(playerTilePosition.y - tilePosition.y) <= maxReach)
        {
            if (seedTilemap.GetTile(tilePosition))
            {
                int removeIndex = seedPositions.IndexOf(tilePosition);
                seedPositions.RemoveAt(removeIndex);
                seedNames.RemoveAt(removeIndex);
                for (int i = 0; i < 3; i++)
                {
                    seedNextGrowths.RemoveAt(3 * removeIndex);
                }
                GameManager.instance.seedPositions = seedPositions;
                GameManager.instance.seedNames = seedNames;
                GameManager.instance.seedNextGrowths = seedNextGrowths;

                string seedTileName = seedTilemap.GetTile(tilePosition).name;
                string seedNameWithStage = Regex.Replace(seedTileName, @"(\p{Lu})", " $1").Trim();
                string seedName = seedNameWithStage.Substring(0, seedNameWithStage.Length - 1);
                Vector3 spawnPosition = mousePosition;
                spawnPosition.z = 0;
                char seedStage = seedNameWithStage[seedNameWithStage.Length - 1];
                Item seedItem;
                if (seedStage == '5')
                {
                    seedItem = ItemManager.instance.GetItemByName(seedName.Split(' ')[0]);
                }
                else
                {
                    seedItem = ItemManager.instance.GetItemByName(seedName);
                }
                Instantiate(seedItem, spawnPosition, Quaternion.identity);

                seedTilemap.SetTile(tilePosition, null);
            }
            dirtTilemap.SetTile(tilePosition, null);
        }
    }

    public void RemoveHighlightTilemap(Vector3 mousePosition)
    {
        tilePosition = dirtTilemap.WorldToCell(mousePosition);
        highlightTilemap.SetTile(tilePosition, null);
    }

    public bool PlantSeed(Vector3 mousePosition, int hoursToGrow, string seedName)
    {
        tilePosition = seedTilemap.WorldToCell(mousePosition);
        if (highlightTilemap.GetTile(tilePosition) && dirtTilemap.GetTile(tilePosition) && !seedTilemap.GetTile(tilePosition))
        {
            string seedTileName = seedName.Replace(" ", "") + "0";
            TileBase seedTile = seedTileToTileBaseDict[seedTileName];
            //float nextGrowthDay = GameManager.instance.day + daysToGrow;
            float nextGrowthHour = hoursToGrow + GameManager.instance.hours + GameManager.instance.minutes/60;
            seedPositions.Add(tilePosition);
            seedNames.Add(seedTileName);
            //seedNextGrowths.Add(nextGrowthDay);
            seedNextGrowths.Add(nextGrowthHour);
            seedNextGrowths.Add(hoursToGrow);
            //seedNextGrowths.Add(daysToGrow);
            GameManager.instance.seedPositions = seedPositions;
            GameManager.instance.seedNames = seedNames;
            GameManager.instance.seedNextGrowths = seedNextGrowths;
            seedTilemap.SetTile(tilePosition, seedTile);
            return true;
        }
        return false;
    }
}
