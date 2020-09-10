using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositionStatus { FREE, TAKEN };

public class ItemManager : MonoBehaviour
{
    private PositionStatus[,] _gameBoardStatus;

    public void Awake() {
        RegisterEvents();
    }

    public void OnDestroy() {
        UnRegisterEvents();
    }

    public void Initialize(int gameBoardWidth, int gameBoardHeight) {
        InitializeGameBoardStatusArray(gameBoardWidth, gameBoardHeight);
    }

    private void InitializeGameBoardStatusArray(int width, int height) {
        _gameBoardStatus = new PositionStatus[width, height];

        for(int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                _gameBoardStatus[i,j] = PositionStatus.FREE;
            }
        }
    }

    private void UpdateTakenPositionsOnGameBoard(Vector2 takenPosition, Vector2 oldPosition) {
        _gameBoardStatus[(int)takenPosition.x, (int)takenPosition.y] = PositionStatus.TAKEN;
        _gameBoardStatus[(int)oldPosition.x, (int)oldPosition.y] = PositionStatus.FREE;
    }

        private void RegisterEvents()
    {
        SnakeManager.OnSnakePositionUpdated += UpdateTakenPositionsOnGameBoard;
    }

    private void UnRegisterEvents()
    {
        SnakeManager.OnSnakePositionUpdated -= UpdateTakenPositionsOnGameBoard;
    }
}
