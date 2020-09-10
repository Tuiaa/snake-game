using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

         RegisterEvents();

        _backgroundGenerator.GetComponent<BackgroundGenerator>().createBackgroundTiles(_gameData.gameBoardWidth, _gameData.gameBoardHeight);
    
        InitializeItemManager();
        InitializeSnakeManager();

    }

    public void OnDestroy() {
        UnRegisterEvents();
    }

    private void InitializeItemManager() {
        _itemManagerScript = _itemManager.GetComponent<ItemManager>();
        _itemManagerScript.Initialize(_gameData.gameBoardWidth, _gameData.gameBoardHeight);
    }

    private void InitializeSnakeManager() {
        _snakeManagerScript = _snakeManager.GetComponent<SnakeManager>();
        _snakeManagerScript.Initialize(_gameData.gameBoardWidth, _gameData.gameBoardHeight, _gameData.snakeSpeed);
        
        _snakeManagerScript.SpawnSnake(utils.getMiddlePositionFromGameBoard(_gameData.gameBoardWidth, _gameData.gameBoardHeight),
                                                                   _gameData.snakeStartSize);

        _snakeManagerScript.StartSnakeMoving();
    }

    private void OnSnakeDied() {
        SceneManager.LoadScene(GameConstants.GAMESCENE_NAME);
    }

    private void RegisterEvents()
    {
        Snake.OnSnakeDied += OnSnakeDied;
    }

    private void UnRegisterEvents()
    {
        Snake.OnSnakeDied -= OnSnakeDied;
    }
}
