using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoadTilemap : MonoBehaviour
{
    public RuleTile rule;
    public Tilemap highlightMap;
    public int roomQuantity = 5;
    public GameObject[] prefabEnemy;
    int roomSize = 15;


    void Start()
    {
        BuildMap();
        StartCoroutine(RecalculatePaths());

    }

    IEnumerator RecalculatePaths()
    {
        yield return new WaitForEndOfFrame();
        AstarPath.active.Scan();
    }

    private void BuildMap()
    {
        var room = BuildRoom();
        int yIndex = 0;
        int xIndex = 0;
        for (int i = 0; i < roomQuantity; i++)
        {
            var randDirection = UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f;
            LoadRoom((roomSize * xIndex) + xIndex, (roomSize * yIndex) + yIndex, room);
            if (i < roomQuantity - 1)
                LoadConnection((roomSize * xIndex) + xIndex, (roomSize * yIndex) + yIndex, randDirection);
            if (randDirection)
            {
                xIndex++;
            }
            else
            {
                yIndex++;
            }
        }
    }

    private void LoadConnection(int positionxStart, int positionyStart, bool direction)
    {
        if (direction)
        {
            var indexX = positionxStart + roomSize;
            var indexY = positionyStart + roomSize / 3;
            for (int i = 0; i < roomSize / 3; i++)
            {
                highlightMap.SetTile(new Vector3Int(indexX, indexY + i, 0), rule);

            }
        }
        else
        {
            var indexX = positionxStart + roomSize / 3;
            var indexY = positionyStart + roomSize;
            for (int i = 0; i < roomSize / 3; i++)
            {
                highlightMap.SetTile(new Vector3Int(indexX + i, indexY, 0), rule);
            }
        }
    }

    private void LoadRoom(int positionxStart, int positionyStart, string[,] room)
    {
        for (int i = 0; i < room.GetLength(0); i++)
        {
            for (int j = 0; j < room.GetLength(1); j++)
            {
                if (room[i, j] == "1")
                {
                    highlightMap.SetTile(new Vector3Int(i + positionxStart, j + positionyStart, 0), rule);
                }
                else if (room[i, j] == "e")
                {
                    var pos = new Vector3Int(i + positionxStart, j + positionyStart, 0);
                    highlightMap.SetTile(pos, rule);
                    Instantiate(prefabEnemy[UnityEngine.Random.Range(0, prefabEnemy.Length)], highlightMap.GetCellCenterWorld(pos), Quaternion.identity);
                }
                else
                {
                    highlightMap.SetTile(new Vector3Int(i + positionxStart, j + positionyStart, 0), null);
                }
            }
        }
    }

    private string[,] BuildRoom()
    {
        string[,] map = new string[roomSize, roomSize];
        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                if (j == roomSize / 2 && i == roomSize / 2)
                {
                    map[i, j] = "e";
                }
                else
                {
                    map[i, j] = "1";
                }

            }
        }

        return map;
    }

}
