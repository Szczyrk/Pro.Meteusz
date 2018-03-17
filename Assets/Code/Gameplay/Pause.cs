using UnityEngine;
using UnityEngine.UI;

public class Pause : State
{

    public Button returnButton;

    void Awake()
    {
        SetActionToButton(returnButton, OnReturn);

        Time.timeScale = 0f;
    }


    void OnReturn()
    {
        SimpleStateMachine.instance.SetState<GameState>();
    }
}