using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundTile;
    public void createBackgroundTiles(int givenWidth, int givenHeight) {
        if(_backgroundTile == null) {
            Debug.LogWarning("BackgroundGenerator: Missing references!");
            return;
        }

        GameObject parent = new GameObject(GameConstants.BACKGROUND_PARENT_GAMEOBJECT_NAME);
        
        for (int height = 0; height < givenHeight; height++) {
            for (int width = 0; width < givenWidth; width++) {
                GameObject tile = Instantiate(_backgroundTile, new Vector3(width, height, 0.01f), Quaternion.identity);
                tile.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>());
            }
        }
    }
}
