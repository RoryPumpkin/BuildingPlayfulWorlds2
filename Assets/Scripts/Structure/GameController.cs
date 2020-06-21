using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Player Player { get; private set; }
    public Level Level { get; private set; }

    public TileColors Colors { get; private set; }

    public FSM fsm { get; private set; }

    public Canvas canvas;

    private void Awake()
    {
        Instance = this;
        Level = FindObjectOfType<Level>();
        fsm = GetComponent<FSM>();
        Colors = GetComponent<TileColors>();
        if(FindObjectOfType<Canvas>() == null)
        {
            Instantiate(canvas);
        }
        //canvas = FindObjectOfType<Canvas>();
        //DontDestroyOnLoad(canvas);
    }

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        Level.Init();
        Player = FindObjectOfType<Player>();
        Player.Init();
        fsm.Init();
        fsm.SwitchState(typeof(PlayerTurnState));
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    private void FixedUpdate()
    {
        fsm.OnFixedUpdate();
    }

    public void WinGame()
    {
        //Debug.Log("Game Won!");
        fsm.SwitchState(typeof(GameWonState));
    }

    public void LoseGame()
    {
        //Debug.Log("Game Over!");
        fsm.SwitchState(typeof(GameLostState));
    }

    public void EndPlayerTurn()
    {
        //Debug.Log("Player Turn Ended!");
        //fsm.SwitchState(typeof(BetweenTurnState));
        fsm.SwitchState(typeof(PlayerTurnState));
    }
}
