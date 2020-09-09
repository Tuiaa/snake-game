using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataScriptableObject _gameData;
    [SerializeField] private GameObject _backgroundGenerator;
    [SerializeField] private GameObject _snakeManager;
    private SnakeManager _snakeManagerScript;

    private Utils utils;
    public void Awake() {
        if(_gameData == null || _backgroundGenerator == null || _snakeManager == null){
            Debug.LogWarning("GameManager: Missing references!");
            return;
        }
        utils = new Utils();
        _backgroundGenerator.GetComponent<BackgroundGenerator>().createBackgroundTiles(_gameData.backgroundSizeX, _gameData.backgroundSizeY);
        _snakeManagerScript = _snakeManager.GetComponent<SnakeManager>();
        _snakeManagerScript.SpawnSnake(
            utils.getSnakeSpawnPosition(_gameData.backgroundSizeX, _gameData.backgroundSizeY),
            _gameData.snakeStartSize);

        _snakeManagerScript.StartSnakeMoving();
    }
}
