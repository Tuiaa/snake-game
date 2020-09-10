using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      Snake
 *      - Takes care of collision with its own tail
 *      - Triggers a game ending event
 */
public class Snake : MonoBehaviour
{
    public delegate void SnakeDiedEventHandler();
    public static event SnakeDiedEventHandler OnSnakeDied;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.SNAKE_TAIL_GAMEOBJECT_TAG) {
            OnSnakeDied?.Invoke();
        }
    }
}
