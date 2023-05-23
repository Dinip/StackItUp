using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManagerObject", order = 1)]
public class GameManagerObject : ScriptableObject
{
    [NonSerialized]
    public UnityEvent<bool> collisionEvent;

    public Difficulty difficulty = Difficulty.Easy;

    private void OnEnable()
    {
        collisionEvent ??= new UnityEvent<bool>();
    }
}