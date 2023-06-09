using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManagerObject", order = 1)]
public class GameManagerObject : ScriptableObject
{
    [NonSerialized]
    public UnityEvent<bool> collisionEvent;

    [NonSerialized]
    public UnityEvent<bool> gamePausedEvent;

    [NonSerialized]
    public UnityEvent<bool> gameOverEvent;

    public Difficulty difficulty { get; private set; }

    public bool isPaused = false;

    public int maxTotalHealth = 3;

    public int maxHealth = 3;

    public int health = 3;

    public int pieces = 0;

    public MouseMode mouseMode = MouseMode.Normal;

    private void OnEnable()
    {
        collisionEvent ??= new UnityEvent<bool>();
        gamePausedEvent ??= new UnityEvent<bool>();
        gameOverEvent ??= new UnityEvent<bool>();

        health = maxHealth;
        pieces = 0;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            gameOverEvent?.Invoke(false);
        }
    }

    public void SetPause(bool paused)
    {
        isPaused = paused;
        gamePausedEvent?.Invoke(paused);
    }

    public void ResetGame()
    {
        health = maxHealth;
        pieces = 0;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
        maxHealth = maxTotalHealth - (int)difficulty;
        health = maxHealth;
    }
}