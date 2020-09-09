using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameDataScriptableObject : ScriptableObject
{
    
    public int backgroundSizeX;
    public int backgroundSizeY;
    public int snakeStartSize;
    public float snakeSpeed;
}