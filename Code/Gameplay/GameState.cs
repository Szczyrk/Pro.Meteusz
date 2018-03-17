using UnityEngine;
using UnityEngine.UI;

public class GameState : State
{
    public static GameObject player;
    public Button pauseButton;



    void Awake()
    {

        player = FindObjectOfType<PlayerController>().gameObject;
        SetActionToButton(pauseButton, OnPause);
        Time.timeScale = 1f;

    }

    void OnPause()
    {
        SimpleStateMachine.instance.SetState<Pause>();

    }
}
