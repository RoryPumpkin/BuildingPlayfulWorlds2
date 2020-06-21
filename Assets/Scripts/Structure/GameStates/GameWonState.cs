using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWonState : State
{
    private UI_Tweens won;

    public override void OnEnter()
    {
        //Yay we won!
        
        if (FindObjectOfType<UI_Tweens>())
        {
            won = FindObjectOfType<UI_Tweens>();
        }
        won.currentState = UI_Tweens.State.full;
        won.MoveToPos(won.gameObject, new Vector3(won.gameObject.transform.position.x, Screen.height * 0.6f, won.gameObject.transform.position.z), 0.5f);
    }

    public override void OnExit()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        if (Input.GetButtonDown("reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void NextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(scene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

