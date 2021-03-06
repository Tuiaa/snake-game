﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      Snake Manager
 *      - Takes care of snake related things
 */
public class SnakeManager : MonoBehaviour
{
    public delegate void SnakePositionUpdatedEventHandler(Vector2 newPosition, Vector2 oldPosition);
    public static event SnakePositionUpdatedEventHandler OnSnakePositionUpdated;

    [SerializeField] private GameObject _snakeTailPrefab;
    [SerializeField] private Transform _snake;

    List<Transform> _snakeTailList;
    List<Direction> _listOfMoves;
    private int _listOfMovesLength = 0;
    private Direction _currentMoveDirection = Direction.RIGHT;

    bool _snakePartAdded = false;
    private float _snakeSpeed = 0;
    private int _gameBoardWidth = 0;
    private int _gameBoardHeight = 0;

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

    // Spawns the snake and its tail and adds them into a list
    public void SpawnSnake(Vector2 spawnPosition, int startLength) {
        if(_snake == null) {
            Debug.LogWarning("SnakeManager: Missing Snake reference!");
            return;
        }

        _snake.position = spawnPosition;
        _snakeTailList = new List<Transform>();
        _snakeTailList.Add(_snake);

        for (int i = 1; i < startLength; i++) {
            GameObject tail = Instantiate(_snakeTailPrefab, new Vector2(spawnPosition.x - i, spawnPosition.y), Quaternion.identity);
            _snakeTailList.Add(tail.GetComponent<Transform>());
        }

        _listOfMovesLength = startLength;
        InitializeMoveList();
    }

    // Keeps track of the moves snake has done
    private void InitializeMoveList() {
        _listOfMoves = new List<Direction>();
        for (int i = 0; i < _listOfMovesLength; i++) {
            _listOfMoves.Add(Direction.RIGHT);
        }
    }

    public void StartSnakeMoving() {
        InvokeRepeating("MoveSnake", _snakeSpeed, _snakeSpeed);
    }

    // Moves each snake part one move behind each other
    private void MoveSnake() {
        // Adds a move to the list
        _listOfMoves.Insert(0, _currentMoveDirection);

        // Checks if there's more moves than there are snake parts
        if (_listOfMoves.Count > _snakeTailList.Count) {
            for(int i = _snakeTailList.Count; i < _listOfMoves.Count; i++) {
                _listOfMoves.RemoveAt(i);
            }
        }

        // Moves each of the snake parts
        for (int x = 0; x < _snakeTailList.Count; x++) {
            if(_snakePartAdded && x == _snakeTailList.Count - 1) {
                _snakePartAdded = false;
                return;
            }

            CalculateNewPosition(_listOfMoves[x], _snakeTailList[x]);
        }
    }

    private void CalculateNewPosition(Direction direction, Transform snakePart) {

        Vector2 newPosition = new Vector2(0, 0);
        switch (direction) {
            case Direction.UP:
                newPosition = new Vector2(snakePart.position.x, snakePart.position.y + 1);
            break;
            case Direction.DOWN:
                newPosition = new Vector2(snakePart.position.x, snakePart.position.y - 1);
            break;
            case Direction.LEFT:
                newPosition = new Vector2(snakePart.position.x - 1, snakePart.position.y);
            break;
            case Direction.RIGHT:
                newPosition = new Vector2(snakePart.position.x + 1, snakePart.position.y);
            break;
            default:
            break;
        }
        
        // If snake goes beyond border, it should teleport to the other side
        if(newPosition.x > _gameBoardWidth - 1) {
            newPosition.x = 0;
        }
        else if (newPosition.x < 0) {
            newPosition.x = _gameBoardWidth - 1;
        }
        else if (newPosition.y > _gameBoardHeight - 1) {
            newPosition.y = 0;
        }
        else if (newPosition.y < 0) {
            newPosition.y = _gameBoardHeight - 1;
        }

        snakePart.position = newPosition;
        OnSnakePositionUpdated?.Invoke(newPosition, snakePart.position);
    }

    /*  EVENTS  */
    private void OnGamePadButtonClicked(Direction direction) {
        _currentMoveDirection = direction;
    }

    // Grow snake when it has eaten an item
    private void OnItemEaten() {
        GameObject tail = Instantiate(_snakeTailPrefab, _snakeTailList[_snakeTailList.Count - 1].position, Quaternion.identity);
        _snakeTailList.Insert(_snakeTailList.Count, tail.GetComponent<Transform>());
        _snakePartAdded = true;
    }

    private void RegisterEvents()
    {
        InputManager.OnGamePadButtonClicked += OnGamePadButtonClicked;
        Item.OnItemEaten += OnItemEaten;
    }

    private void UnRegisterEvents()
    {
        InputManager.OnGamePadButtonClicked -= OnGamePadButtonClicked;
        Item.OnItemEaten -= OnItemEaten;
    }
}
