using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager Instance;

    [SerializeField] private GameObject player1Obj;
    private Player player1;
    [SerializeField] private GameObject player2Obj;
    private Player player2;

    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private GameObject ballObject;
    private Ball ball;

    [SerializeField] private GameObject UIObject;
    private UI UI;

    [SerializeField] private GameObject musicObject;
    private AudioSource music;

    public enum GameState
    {
        Title,
        Controls,
        RoundStart,
        Paused,
        InGame,
        EndGame
    }

    private int score1=0;
    private int score2=0;
    private const int VICTORY_SCORE = 3;

    void Awake()
    {
        Instance = this;

        ball = ballObject.GetComponent<Ball>();
        ball.OnP1Scored += Ball_OnP1Scored;
        ball.OnP2Scored += Ball_OnP2Scored; 
        ball.OnLeaveScreen += Ball_OnLeaveScreen; 
        ball.OnEnterScreen += Ball_OnEnterScreen;

        UI = UIObject.GetComponent<UI>();

        music = musicObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        player1 = player1Obj.GetComponent<Player>();
        player2 = player2Obj.GetComponent<Player>();
        SetGameState(GameState.Title);
    }

    void OnDestroy()
    {
        ball.OnP1Scored -= Ball_OnP1Scored;
        ball.OnP2Scored -= Ball_OnP2Scored;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Title)
        {
            if (Input.anyKeyDown)
            {
                SetGameState(GameState.Controls);
            }
        }
        else if (state == GameState.Controls)
        {
            if (Input.anyKey)
            {
                SetGameState(GameState.RoundStart);
            }
        }
        else if (state == GameState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SetGameState(GameState.InGame);
            }
        }
        else if (state == GameState.InGame)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SetGameState(GameState.Paused);
            }
        }
    }

    public void SetGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Title:
            {
                Title();
                break;
            }
            case GameState.Controls:
            {
                Controls();
                break;
            }
            case GameState.RoundStart:
            {
                RoundStart();
                SetGameState(GameState.InGame);
                break;
            }
            case GameState.Paused:
            {
                Paused();
                break;
            }
            case GameState.InGame:
            {
                InGame();
                break;
            }
            case GameState.EndGame:
            {
                EndGame();
                break;
            }
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void Title()
    {
        Time.timeScale = 0;
    }

    private void Controls()
    {
        Time.timeScale = 0;
    }

    private void RoundStart()
    {
        music.volume = 0.3f;
    }

    private void Paused()
    {
        Time.timeScale = 0;
    }

    private void InGame()
    {
        Time.timeScale = 1;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
    }

    private void Ball_OnP1Scored(object sender, EventArgs e)
    {
        score1++;
        UI.SetScore1(score1);

        if (score1 >= VICTORY_SCORE)
        {
            SetGameState(GameState.EndGame);
            score1 = 0;
            score2 = 0;
            UI.SetScore1(score1);
            UI.SetScore2(score2);
            UI.SetVictoryText(1);
        }
        else
        {
            SetGameState(GameState.RoundStart);
        }
    }

    private void Ball_OnP2Scored(object sender, EventArgs e)
    {
        score2++;
        UI.SetScore2(score2);

        if (score2 >= VICTORY_SCORE)
        {
            SetGameState(GameState.EndGame);
            score1 = 0;
            score2 = 0;
            UI.SetScore1(score1);
            UI.SetScore2(score2);
            UI.SetVictoryText(2);
        }
        else
        {
            SetGameState(GameState.RoundStart);
        }
    }

    private void Ball_OnLeaveScreen(object sender, EventArgs e)
    {
        UI.ShowHighBallMarker();
    }

    private void Ball_OnEnterScreen(object sender, EventArgs e)
    {
        UI.HideHighBallMarker();
    }

    public Vector2 GetBallPosition()
    {
        return ballObject.transform.position;
    }

    public void Rematch()
    {
        SetGameState(GameState.RoundStart);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UseItem(Item.ItemType itemType, String player)
    {
        Player user;
        Player opponent;

        if (player == "Player 1")
        {
            user = player1;
            opponent = player2;
        }
        else 
        {
            user = player2;
            opponent = player1;
        }
        switch (itemType)
        {
            case (Item.ItemType.Snowflake):
            {
                StartCoroutine(FreezePlayer(opponent, 1.2f));
                break;
            }
            case (Item.ItemType.Stopwatch):
            {
                StartCoroutine(SlowTime(3.0f));
                break;
            }
            case (Item.ItemType.Mushroom):
            {
                StartCoroutine(GrowPlayer(user, 5.0f));
                break;
            }
        }
    }

    IEnumerator SlowTime(float delayTime)
    {
        float slowAmount = 0.3f;
        delayTime *= slowAmount;

        Time.timeScale = slowAmount;
        yield return new WaitForSeconds(delayTime);
        Time.timeScale = 1;
    }

    IEnumerator FreezePlayer(Player player, float delayTime)
    {
        Rigidbody2D body = player.gameObject.GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(delayTime);

        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator GrowPlayer(Player player, float delayTime)
    {
        float growAmount = 2.0f;
        player.transform.localScale *= growAmount;

        float originalSpeed = player.GetSpeed();
        player.SetSpeed(player.GetSpeed() * (1/growAmount));

        yield return new WaitForSeconds(delayTime);

        player.transform.localScale /= growAmount;
        player.SetSpeed(originalSpeed);

    }
}



