using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataScriptableObject _gameData;
    [SerializeField] private GameObject _backgroundGenerator;
    public void Awake() {
        if(_gameData == null || _backgroundGenerator == null){
            Debug.LogWarning("GameManager: Missing references!");
            return;
        }
        _backgroundGenerator.GetComponent<BackgroundGenerator>().createBackgroundTiles(_gameData.backgroundSizeX, _gameData.backgroundSizeY);
        
    }
}
