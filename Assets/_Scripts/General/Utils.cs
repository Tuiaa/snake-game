using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { UP, DOWN, LEFT, RIGHT };
public enum PositionStatus { FREE, TAKEN };
    
/*
 *      Utils
 *      - General helper methods, enums etc
 */
public class Utils
{
    public Vector2 getMiddlePositionFromGameBoard(int gameWidth, int gameHeight) {
        float posX = Mathf.Round(gameWidth/2);
        float posY = Mathf.Round(gameHeight/2);

        return new Vector2(posX, posY);
    }
}
