using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public delegate void SnakePositionUpdatedEventHandler(Vector2 newPosition, Vector2 oldPosition);
    public static event SnakePositionUpdatedEventHandler OnSnakePositionUpdated;

    [SerializeField] private GameObject _snakePrefab;
    [SerializeField] private Transform _snake;

    List<Transform> _snakeTail;
    List<Direction> _listOfMoves = new List<Direction>();
    private int _listOfMovesLength = 0;
    private int _snakeLength = 0;
    private float _snakeSpeed = 0;
    private int _gameBoardWidth = 0;
    private int _gameBoardHeight = 0;
    private Direction _currentMoveDirection = Direction.RIGHT;

    public void Awake() {
        RegisterEvents();
    }

    public void OnDestroy() {
        UnRegisterEvents();
    }

    public void Initialize(int width, int height, float snakeSpeed) {
        _gameBoardWidth = width;
        _gameBoardHeight = height;

        _snakeSpeed = snakeSpeed;
    }

    public void SpawnSnake(Vector2 spawnPosition, int startLength) {
        if(_snake != null) {
            _snake.position = spawnPosition;

            _snakeTail = new List<Transform>();
            _snakeTail.Add(_snake);

            for (int i = 1; i < startLength; i++) {
                GameObject tail = Instantiate(_snakePrefab, new Vector2(spawnPosition.x - i, spawnPosition.y), Quaternion.identity);
                _snakeTail.Add(tail.GetComponent<Transform>());
            }

            _listOfMovesLength = startLength;
            _snakeLength = startLength;

            InitializeMoveList();
        }
    }

    public void StartSnakeMoving() {
        InvokeRepeating("MoveSnake", _snakeSpeed, _snakeSpeed);
    }

    private void InitializeMoveList() {
        for (int i = 0; i < _listOfMovesLength; i++) {
            _listOfMoves.Add(Direction.RIGHT);
        }
    }

    private void MoveSnake() {
        _listOfMoves.Insert(0, _currentMoveDirection);

        int moveCount = _listOfMoves.Count;
        if (moveCount > _snakeLength) {
            for(int i = _snakeLength; i < moveCount; i++) {
                _listOfMoves.RemoveAt(i);
            }
        }

        for (int x = 0; x < _snakeTail.Count; x++) {
                switch (_listOfMoves[x]) {
                case Direction.UP:
                    CalculateNewPosition(_snakeTail[x], new Vector2(_snakeTail[x].position.x, _snakeTail[x].position.y + 1), new Vector2(_snakeTail[x].position.x, _snakeTail[x].position.y));
                break;
                case Direction.DOWN:
                    CalculateNewPosition(_snakeTail[x], new Vector2(_snakeTail[x].position.x, _snakeTail[x].position.y - 1), new Vector2(_snakeTail[x].position.x, _snakeTail[x].position.y));
                break;
                case Direction.LEFT:
                    CalculateNewPosition(_snakeTail[x], new Vector2(_snakeTail[x].position.x - 1, _snakeTail[x].position.y), new Vector2(_snakeTail[x].position.x, _snakeTail[x].position.y));
                break;
                case Direction.RIGHT:
                    CalculateNewPosition(_snakeTail[x], new Vector2(_snakeTail[x].position.x + 1, _snakeTail[x].position.y), new Vector2(_snakeTail[x].position.x, _snakeTail[x].position.y));
                break;
                default:
                break;
            }
        }
    }

    private void CalculateNewPosition(Transform snakePart, Vector2 newPosition, Vector2 currentPosition) {

        Vector2 newPos = newPosition;
        if(newPos.x > _gameBoardWidth - 1)
        {
            newPos.x = 0;
        }
        else if (newPos.x < 0) {
            newPos.x = _gameBoardWidth - 1;
        }
        else if (newPos.y > _gameBoardHeight - 1) {
            newPos.y = 0;
        }
        else if (newPos.y < 0) {
            newPos.y = _gameBoardHeight - 1;
        }

        snakePart.position = newPos;
        OnSnakePositionUpdated?.Invoke(newPos, currentPosition);
    }

    private void UpdateCurrentDirection(Direction direction) {
        _currentMoveDirection = direction;
    }

    private void RegisterEvents()
    {
        InputManager.OnGamePadButtonClicked += UpdateCurrentDirection;
    }

    private void UnRegisterEvents()
    {
        InputManager.OnGamePadButtonClicked -= UpdateCurrentDirection;
    }
}
