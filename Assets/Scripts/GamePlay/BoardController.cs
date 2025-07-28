using Unity.VisualScripting;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private Tile[,] _tiles;
    private int _size;
    private bool isGameOver;

    public bool IsGameOver => isGameOver;

    public void Init(Tile[,] board)
    {
        _tiles = board;
        _size = board.GetLength(0);
        isGameOver = false;

        GameEventDispatcher.OnTileClicked -= HandleTileClicked;
        GameEventDispatcher.OnTileClicked += HandleTileClicked;
    }

    private void OnDisable()
    {
        GameEventDispatcher.OnTileClicked -= HandleTileClicked;
    }

    public void GenerateSolvableBoard()
    {
        int randomPresses = Random.Range(3, 8);
        Debug.Log($"Played {randomPresses} Steps");

        for (int i = 0; i < randomPresses; i++)
        {
            int x = Random.Range(0, _size);
            int y = Random.Range(0, _size);
            Press(new Vector2Int(x, y));
        }
    }

    private void HandleTileClicked(Vector2Int gridPosition)
    {
        Press(gridPosition);

        GameOver();
        if (isGameOver)
        {
            GameEventDispatcher.GameOver(isGameOver);
        }
    }

    private void Press(Vector2Int pos)
    {
        ToggleAt(pos);
        ToggleAt(pos + Vector2Int.up);
        ToggleAt(pos + Vector2Int.down);
        ToggleAt(pos + Vector2Int.left);
        ToggleAt(pos + Vector2Int.right);
    }
    private void ToggleAt(Vector2Int pos)
    {
        if (pos.x >= 0 && pos.x < _size && pos.y >= 0 && pos.y < _size)
        {
            _tiles[pos.x, pos.y].Toggle();
        }
    }

    private void GameOver()
    {
        foreach (Tile tile in _tiles)
        {
            if (tile.IsOn)
            {
                isGameOver = false; 
                return;
            }
        }

        isGameOver = true;
    }
}
