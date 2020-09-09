using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { UP, DOWN, LEFT, RIGHT };

public class Utils
{
    public Vector2 getSnakeSpawnPosition(int gameWidth, int gameHeight) {
        float posX = Mathf.Round(gameWidth/2);
        float posY = Mathf.Round(gameHeight/2);

        return new Vector2(posX, posY);
    }
}
