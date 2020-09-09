using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private GameObject _snakePrefab;
    [SerializeField] private Transform _snake;

    List<Transform> _snakeTail;

    public void SpawnSnake(Vector2 spawnPosition, int startLength) {
        if(_snake != null) {
            _snake.position = spawnPosition;

            _snakeTail = new List<Transform>();
            _snakeTail.Add(_snake);

            for (int i = 1; i < startLength; i++) {
                GameObject tail = Instantiate(_snakePrefab, new Vector2(spawnPosition.x - i, spawnPosition.y), Quaternion.identity);
                _snakeTail.Add(tail.GetComponent<Transform>());
            }
        }
    }
}
