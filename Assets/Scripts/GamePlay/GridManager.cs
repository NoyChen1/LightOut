using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

public class GridManager : MonoBehaviour
{
    [Inject] private TileFactory _tileFactory;
    [Inject] private GameTimer _gameTimer;

    [SerializeField] private int _gridSize = 5;
    [SerializeField] private float _tileSpacing = 1f;
    [SerializeField] private BoardController _boardController;


    private Tile[,] _tileBoard;

    private void OnEnable()
    {
        GameEventDispatcher.OnTryAgainClicked += RestartBoard;
        GameEventDispatcher.OnTimeOver += HandleTimerOver;
    }

    private void OnDisable()
    {
        GameEventDispatcher.OnTryAgainClicked -= RestartBoard;
        GameEventDispatcher.OnTimeOver -= HandleTimerOver;
    }

    private async void Start()
    {
        await _tileFactory.InitAsync(this.GetCancellationTokenOnDestroy());

        GenerateBoard();
        _gameTimer.StartTimer();
    }

    private void GenerateBoard()
    {
        _tileBoard = new Tile[_gridSize, _gridSize];

        float offset = (_gridSize - 1) * _tileSpacing * 0.5f;

        for (int y = 0; y < _gridSize; y++)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                Vector2 pos = new Vector2(x * _tileSpacing - offset, y * -_tileSpacing + offset);
                var tile = _tileFactory.GetTile(pos);
                tile.Init(new Vector2Int(x, y));
                _tileBoard[x, y] = tile;
            }
        }

        _boardController.Init(_tileBoard);
        _boardController.GenerateSolvableBoard();
    }

    private void HandleTimerOver()
    {
        GameEventDispatcher.TimerUpdated(0f);
        GameEventDispatcher.GameOver(_boardController.IsGameOver);
    }

    private void RestartBoard()
    {
        for (int i = 0; i < _gridSize; i++)
        {

            for (int j = 0; j < _gridSize; j++)
            {
                if(_tileBoard[i, j] != null)
                { 
                    _tileFactory.ReleaseTile(_tileBoard[i, j]);
                    _tileBoard[i, j] = null;
                }
            }
        }

        GenerateBoard();    
        _gameTimer.StartTimer();
    }

}
