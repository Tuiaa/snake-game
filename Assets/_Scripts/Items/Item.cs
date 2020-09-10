using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
