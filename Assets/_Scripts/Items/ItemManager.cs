using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      Item Manager
 *      - Takes care of item related things
 */
public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    private GameObject _spawnedItem;

    private GameBoardBlockStatus[,] _gameBoardStatus;
    private int _gameBoardWidth = 0;
    private int _gameboardHeight = 0;

    public void Awake() {
        RegisterEvents();
    }

    public void OnDestroy() {
        UnRegisterEvents();
    }

    public void Initialize(int gameBoardWidth, int gameBoardHeight) {
        _gameBoardWidth = gameBoardWidth;
        _gameboardHeight = gameBoardHeight;

        InitializeGameBoardStatusArray();
        RandomizeNewItem();
    }

    // Game board status array knows the status of each game board block piece, can be taken or free
    private void InitializeGameBoardStatusArray() {
        _gameBoardStatus = new GameBoardBlockStatus[_gameBoardWidth, _gameboardHeight];

        for(int i = 0; i < _gameBoardWidth; i++) {
            for (int j = 0; j < _gameboardHeight; j++) {
                _gameBoardStatus[i,j] = GameBoardBlockStatus.FREE;
            }
        }
    }

    // Randomizes a new location for an item and adds it to the game board status array
    private void RandomizeNewItem() {
        Vector2 itemPosition = new Vector3(Random.Range(0, _gameBoardWidth - 1), Random.Range(0, _gameboardHeight - 1));

        if(_gameBoardStatus[(int)itemPosition.x, (int)itemPosition.y] == GameBoardBlockStatus.FREE) {
            _spawnedItem = Instantiate(_itemPrefab, itemPosition, Quaternion.identity);
            _gameBoardStatus[(int)itemPosition.x, (int)itemPosition.y] = GameBoardBlockStatus.TAKEN;
        } else {
            RandomizeNewItem();
        }
    }

    /*  EVENTS  */
    private void OnSnakePositionUpdated(Vector2 takenPosition, Vector2 oldPosition) {
        _gameBoardStatus[(int)takenPosition.x, (int)takenPosition.y] = GameBoardBlockStatus.TAKEN;
        _gameBoardStatus[(int)oldPosition.x, (int)oldPosition.y] = GameBoardBlockStatus.FREE;
    }

    private void OnItemEaten() {
        Transform _currentItemTransform = _spawnedItem.GetComponent<Transform>();
        _gameBoardStatus[(int)_currentItemTransform.position.x, (int)_currentItemTransform.position.y] = GameBoardBlockStatus.FREE;

        Destroy(_spawnedItem);
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
        Item.OnItemEaten -= OnItemEaten;
    }
}
