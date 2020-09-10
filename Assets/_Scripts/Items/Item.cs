using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      Item
 *      - Takes care of collisions with snake
 *      - Triggers an event when item has been eaten
 */
public class Item : MonoBehaviour
{
    public delegate void ItemEatenEventHandler();
    public static event ItemEatenEventHandler OnItemEaten;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameConstants.SNAKE_GAMEOBJECT_TAG) {
            OnItemEaten?.Invoke();
        }
    }
}
