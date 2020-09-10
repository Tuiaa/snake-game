using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositionStatus { FREE, TAKEN };

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    private PositionStatus[,] _gameBoardStatus;
    private int _gameBoardWidth = 0;
    private int _gameboardHeight = 0;
    private GameObject _currentlySpawnedItem;

    public void Awake() {
        RegisterEvents();
    }

    public void OnDestroy() {
        UnRegisterEvents();
    }

    public void Initialize(int gameBoardWidth, int gameBoardHeight) {
        _gameBoardWidth = gameBoardWidth;
        _gameboardHeight = gameBoardHeight;
        InitializeGameBoardStatusArray(gameBoardWidth, gameBoardHeight);

        RandomizeNewItem();
    }

    private void InitializeGameBoardStatusArray(int width, int height) {
        _gameBoardStatus = new PositionStatus[width, height];

        for(int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                _gameBoardStatus[i,j] = PositionStatus.FREE;
            }
        }
    }

    private void RandomizeNewItem() {
        Vector2 itemPosition = new Vector3(Random.Range(0, _gameBoardWidth - 1), Random.Range(0, _gameboardHeight - 1));

        if(_gameBoardStatus[(int)itemPosition.x, (int)itemPosition.y] == PositionStatus.FREE) {
            _currentlySpawnedItem = Instantiate(_itemPrefab, itemPosition, Quaternion.identity);
            _gameBoardStatus[(int)itemPosition.x, (int)itemPosition.y] = PositionStatus.TAKEN;
        } else {
            RandomizeNewItem();
        }
    }

    private void OnSnakePositionUpdated(Vector2 takenPosition, Vector2 oldPosition) {
        _gameBoardStatus[(int)takenPosition.x, (int)takenPosition.y] = PositionStatus.TAKEN;
        _gameBoardStatus[(int)oldPosition.x, (int)oldPosition.y] = PositionStatus.FREE;
    }

    private void OnItemEaten() {
        Transform _currentItemTransform = _currentlySpawnedItem.GetComponent<Transform>();
        _gameBoardStatus[(int)_currentItemTransform.position.x, (int)_currentItemTransform.position.y] = PositionStatus.FREE;

        Destroy(_currentlySpawnedItem);
        RandomizeNewItem();
    }

    private void RegisterEvents()
    {
        SnakeManager.OnSnakePositionUpdated += OnSnakePositionUpdated;
        Item.OnItemEaten += OnItemEaten;
    }

    private void UnRegisterEvents()
    {
        SnakeManager.OnSnakePositionUpdated -= OnSnakePositionUpdated;
    }
}
