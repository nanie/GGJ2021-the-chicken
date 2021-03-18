using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LoadTilemap : MonoBehaviour
{
    public RuleTile rule;
    public Tilemap highlightMap;
    public int roomQuantity = 5;
    public GameObject[] prefabEnemy;
    int roomSize = 3;

    [Header("Map Setup")]
    [SerializeField] private int _width = 30;
    [SerializeField] private int _height = 30;
    [SerializeField] private int _maxRooms = 20;
    [SerializeField] private int _roomMaxSize = 6;
    [SerializeField] private int _roomMinSize = 3;
    [SerializeField] private int _corridorSize = 1;
    [SerializeField] private int _extraCorridors = 10;
    private readonly List<Rectangle> _rooms = new List<Rectangle>();

    void Start()
    {
        BuildMapV2();
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

    #region MapV2
    private void BuildMapV2()
    {
        CreateRooms();
        CreateMainCorridors();
        CreateExtraCorridors();
    }
    private void CreateRooms()
    {
        for (int i = 0; i < _maxRooms; i++)
        {
            int roomWidth = Random.Range(_roomMinSize, _roomMaxSize);
            int roomHeight = Random.Range(_roomMinSize, _roomMaxSize);
            int roomXPosition = Random.Range(0, _width - roomWidth - 1);
            int roomYPosition = Random.Range(0, _height - roomHeight - 1);

            var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);
            bool newRoomIntersects = _rooms.Any(room => newRoom.IntersectsWith(room));

            if (!newRoomIntersects)
            {
                _rooms.Add(newRoom);
            }
        }

        foreach (Rectangle room in _rooms)
        {
            CreateRoom(room);
        }
    }
    private void CreateMainCorridors()
    {
        for (int r = 1; r < _rooms.Count; r++)
        {
            int previousRoomCenterX = (int)Center(_rooms[r - 1]).x;
            int previousRoomCenterY = (int)Center(_rooms[r - 1]).y;
            int currentRoomCenterX = (int)Center(_rooms[r]).x;
            int currentRoomCenterY = (int)Center(_rooms[r]).y;

            if (Random.Range(0, 2) == 1)
            {
                CreateCorridorHorizontal(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                CreateCorridorVertical(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
            }
            else
            {
                CreateCorridorVertical(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                CreateCorridorHorizontal(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
            }
        }
    }
    private void CreateExtraCorridors()
    {
        for (int i = 0; i < _extraCorridors; i++)
        {
            var roomA = Center(_rooms[Random.Range(0, _rooms.Count)]);
            var roomB = Center(_rooms[Random.Range(0, _rooms.Count)]);
            int previousRoomCenterX = (int)roomA.x;
            int previousRoomCenterY = (int)roomA.y;
            int currentRoomCenterX = (int)roomB.x;
            int currentRoomCenterY = (int)roomB.y;

            if (Random.Range(0, 2) == 1)
            {
                CreateCorridorHorizontal(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                CreateCorridorVertical(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
            }
            else
            {
                CreateCorridorVertical(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                CreateCorridorHorizontal(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
            }
        }
    }
    private Vector2 Center(Rectangle room)
    {
        int x = room.X + room.Width / 2;
        int y = room.Y + room.Height / 2;
        return new Vector2(x, y);
    }
    private void CreateRoom(Rectangle room)
    {
        for (int x = room.Left + 1; x < room.Right; x++)
        {
            for (int y = room.Top + 1; y < room.Bottom; y++)
            {
                highlightMap.SetTile(new Vector3Int(x, y, 0), rule);
            }
        }
    }
    private void CreateCorridorHorizontal(int xStart, int xEnd, int yPosition)
    {
        for (int y = yPosition; y < yPosition + _corridorSize; y++)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                highlightMap.SetTile(new Vector3Int(x, y, 0), rule);
            }
        }
    }
    private void CreateCorridorVertical(int yStart, int yEnd, int xPosition)
    {
        for (int x = xPosition; x < xPosition + _corridorSize; x++)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                highlightMap.SetTile(new Vector3Int(x, y, 0), rule);
            }
        }
    }
    #endregion
}
