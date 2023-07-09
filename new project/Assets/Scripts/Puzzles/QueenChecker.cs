using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueenChecker : MonoBehaviour
{
    public List<Vector2Int> queenPositions = new List<Vector2Int>();

    public Dictionary<Vector2Int, int> seenBefore = new Dictionary<Vector2Int, int>();

    public static QueenChecker instance;

    private void Start()
    {
        instance = this;
    }

    public bool CheckX()
    {
        Dictionary<int, int> dictX = new Dictionary<int, int>();
        for (int i = 0; i < queenPositions.Count; i++)
        {
            if (dictX.ContainsKey(queenPositions[i].x))
            {
                return true;
            }
            else
            {
                dictX.Add(queenPositions[i].x, 1);
            }
        }
        return false;
    }

    public bool CheckDiag(Vector2Int queenPosition)
    {
        // Top left
        int tempY = queenPosition.y + 1;
        for (int x = queenPosition.x - 1; x > -1; x--)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY++;
            }
        }
        // Top right
        tempY = queenPosition.y + 1;
        for (int x = queenPosition.x + 1; x < 6; x++)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY++;
            }
        }
        // Bottom left
        tempY = queenPosition.y - 1;
        for (int x = queenPosition.x - 1; x > -1; x--)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY--;
            }
        }
        // Bottom right
        tempY = queenPosition.y - 1;
        for (int x = queenPosition.x + 1; x < 6; x++)
        {
            if (x >= 0 && x <= 5 && queenPosition.y >= 0 && queenPosition.x <= 5)
            {
                if (queenPositions.Contains(new Vector2Int(x, tempY)))
                {
                    return true;
                }
                tempY--;
            }
        }
        return false;
    }

    public bool HasSeenBefore()
    {
        int seenIntX = 0;
        int seenIntY = 0;
        for (int i = 5; i > -1; i--)
        {
            seenIntX += queenPositions[5 - i].x * (int)Math.Pow(10, i);
            seenIntY += queenPositions[5 - i].y * (int)Math.Pow(10, i);
        }
        Vector2Int seenPos = new Vector2Int(seenIntX, seenIntY);
        if (seenBefore.ContainsKey(seenPos))
        {
            return true;
        }
        seenBefore.Add(new Vector2Int(seenIntX, seenIntY), 1);
        return false;
    }
}
