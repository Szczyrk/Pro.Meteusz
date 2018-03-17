using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : State
{
    const float MIN_WAIT_TIME = 10f;

    AsyncOperation loading;
    //float timer;

    void Awake()
    {
        loading = SceneManager.LoadSceneAsync("Mapa1", LoadSceneMode.Additive);
       // timer = MIN_WAIT_TIME;
    }

    void Update()
    {
        if (loading != null && loading.isDone)
        {
            SimpleStateMachine.instance.SetState<GameState>();
            loading = null;
        }
       // timer -= Time.deltaTime;
    }
}
