using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void GamePadButtonClickEventHandler(Direction direction);
    public static event GamePadButtonClickEventHandler OnGamePadButtonClicked;

    public void UpButtonClicked() {
        OnGamePadButtonClicked?.Invoke(Direction.UP);
    }

    public void DownButtonClicked() {
        OnGamePadButtonClicked?.Invoke(Direction.DOWN);
    }

    public void LeftButtonClicked() {
        OnGamePadButtonClicked?.Invoke(Direction.LEFT);
    }

    public void RightButtonClicked() {
        OnGamePadButtonClicked?.Invoke(Direction.RIGHT);
    }
}
