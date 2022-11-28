using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject camObject;
    private Camera cam;

    [SerializeField] private GameObject hud;
    private GameObject p1ScoreText;
    private GameObject p2ScoreText;
    private GameObject highBallMarker;
    private RectTransform hbmTransform;

    [SerializeField] private GameObject controlScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject endScreen;
    private GameObject p1VictoryText;
    private GameObject p2VictoryText;

    // Start is called before the first frame update
    void Start()
    {
        cam = camObject.GetComponent<Camera>();

        p1ScoreText = hud.transform.GetChild(0).gameObject;
        p2ScoreText = hud.transform.GetChild(1).gameObject;
        highBallMarker = hud.transform.GetChild(2).gameObject;
        hbmTransform = highBallMarker.GetComponent<RectTransform>();

        p1VictoryText = endScreen.transform.GetChild(0).GetChild(0).gameObject;
        p2VictoryText = endScreen.transform.GetChild(0).GetChild(1).gameObject;
    }

    void Awake()
    {
        GameManager.OnGameStateChanged += UIOnGameStateChanged;  
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UIOnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (highBallMarker.activeInHierarchy)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(GameManager.Instance.GetBallPosition());

            int x = (int)(viewPos.x);

            hbmTransform.position = new Vector2((int)(viewPos.x * Screen.width),0 + Screen.height - 30);
        }

    }

    private void UIOnGameStateChanged(GameManager.GameState state)
    {
        controlScreen.SetActive(state == GameManager.GameState.Controls);
        pauseScreen.SetActive(state == GameManager.GameState.Paused);
        titleScreen.SetActive(state == GameManager.GameState.Title);
        endScreen.SetActive(state == GameManager.GameState.EndGame);

        switch (state)
        {
            case GameManager.GameState.Title:
            {              
                break;
            }
            case GameManager.GameState.RoundStart:
            {   
                hud.SetActive(true);            
                break;
            }
            case GameManager.GameState.Paused:
            {
                break;
            }
            case GameManager.GameState.EndGame:
            {
                break;
            }
        }
    }

    private void SetScore(GameObject scoreText, int score)
    {
        TextMeshProUGUI tmp = scoreText.GetComponent<TextMeshProUGUI>();
        tmp.text = score.ToString();
    }

    public void SetScore1(int score)
    {
        SetScore(p1ScoreText, score);
    }

    public void SetScore2(int score)
    {
        SetScore(p2ScoreText, score);
    }

    public void ShowHighBallMarker()
    {
        if (highBallMarker != null)
        {
            highBallMarker.SetActive(true);
        }     
    }

    public void HideHighBallMarker()
    {
        if (highBallMarker != null)
        {
            highBallMarker.SetActive(false);
        }
    }

    public void SetVictoryText(int player)
    {
        if (player == 1)
        {
            p1VictoryText.SetActive(true);
            p2VictoryText.SetActive(false);
        }
        else if (player == 2)
        {
            p1VictoryText.SetActive(false);
            p2VictoryText.SetActive(true);
        }
    }
}
