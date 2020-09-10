using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataScriptableObject _gameData;
    [SerializeField] private GameObject _backgroundGenerator;
    [SerializeField] private GameObject _snakeManager;
    [SerializeField] private GameObject _itemManager;
    private SnakeManager _snakeManagerScript;
    private ItemManager _itemManagerScript;

    private Utils utils;
    public void Awake() {
        if(_gameData == null || _backgroundGenerator == null || _snakeManager == null || _itemManager == null){
            Debug.LogWarning("GameManager: Missing references!");
            return;
        }
        utils = new Utils();
        _backgroundGenerator.GetComponent<BackgroundGenerator>().createBackgroundTiles(_gameData.backgroundSizeX, _gameData.backgroundSizeY);
    
        InitializeItemManager();
        InitializeSnakeManager();
    }

    private void InitializeItemManager() {
        _itemManagerScript = _itemManager.GetComponent<ItemManager>();
        _itemManagerScript.Initialize(_gameData.backgroundSizeX, _gameData.backgroundSizeY);
    }

    private void InitializeSnakeManager() {
        _snakeManagerScript = _snakeManager.GetComponent<SnakeManager>();
        _snakeManagerScript.Initialize(_gameData.backgroundSizeX, _gameData.backgroundSizeY, _gameData.snakeSpeed);
        
        _snakeManagerScript.SpawnSnake(utils.getSnakeSpawnPosition(_gameData.backgroundSizeX, _gameData.backgroundSizeY),
                                                                   _gameData.snakeStartSize);

        _snakeManagerScript.StartSnakeMoving();
    }
}
