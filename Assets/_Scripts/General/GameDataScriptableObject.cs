using UnityEngine;

/*
 *      Game Data Scriptable Object
 *      - General data used in game
 */
[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameDataScriptableObject : ScriptableObject
{
    public int gameBoardWidth;
    public int gameBoardHeight;
    public int snakeStartSize;
    public float snakeSpeed;
}