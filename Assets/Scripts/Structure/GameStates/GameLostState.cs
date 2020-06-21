using UnityEngine;

public class GameLostState : State
{
    public override void OnEnter()
    {
        //Sad, we lost
    }

    public override void OnExit()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameController.Instance.GameStart();
        }
    }
}