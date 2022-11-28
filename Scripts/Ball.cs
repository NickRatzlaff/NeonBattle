using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event EventHandler OnP1Scored;
    public event EventHandler OnP2Scored;

    public event EventHandler OnLeaveScreen;
    public event EventHandler OnEnterScreen;

    private Vector2 startPosition;
    private Rigidbody2D body;

    private AudioSource bounceSound;


    void Awake()
    {
        GameManager.OnGameStateChanged += BallOnGameStateChanged;
        body = gameObject.GetComponent<Rigidbody2D>(); 
        startPosition = new Vector2(0, 2);

        bounceSound = gameObject.GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= BallOnGameStateChanged;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Right Floor")
        {
            OnP1Scored(this, EventArgs.Empty);
        }
        else if (collision.collider.name == "Left Floor")
        {
            OnP2Scored(this, EventArgs.Empty);
        }

        if (collision.relativeVelocity.magnitude > 10)
        {
            bounceSound.Play();
        }
    }

    void OnBecameInvisible()
    {
        OnLeaveScreen(this, EventArgs.Empty);
    }

    void OnBecameVisible()
    {
        OnEnterScreen(this, EventArgs.Empty);
    }

    private void BallOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Title:
            {
                break;
            }
            case GameManager.GameState.RoundStart:
            {
                body.transform.position = startPosition;
                body.velocity = Vector2.zero;
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
}
