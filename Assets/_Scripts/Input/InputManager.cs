using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void GamePadButtonClickEventHandler(Direction direction);
    public static event GamePadButtonClickEventHandler GamePadButtonClicked;

    public void UpButtonClicked() {
        GamePadButtonClicked?.Invoke(Direction.UP);
    }

    public void DownButtonClicked() {
        GamePadButtonClicked?.Invoke(Direction.DOWN);
    }

    public void LeftButtonClicked() {
        GamePadButtonClicked?.Invoke(Direction.LEFT);
    }

    public void RightButtonClicked() {
        GamePadButtonClicked?.Invoke(Direction.RIGHT);
    }
}
