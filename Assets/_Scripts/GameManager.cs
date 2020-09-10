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

        _backgroundGenerator.GetComponent<BackgroundGenerator>().createBackgroundTiles(_gameData.backgroundSizeX, _gameData.backgroundSizeY);
    
        InitializeItemManager();
        InitializeSnakeManager();

    }

    public void OnDestroy() {
        UnRegisterEvents();
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

    private void OnSnakeDied() {
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(GameConstants.GAMESCENE_NAME);
    }

    private void RegisterEvents()
    {
        SnakeHead.OnSnakeDied += OnSnakeDied;
    }

    private void UnRegisterEvents()
    {
        SnakeHead.OnSnakeDied -= OnSnakeDied;
    }
}
