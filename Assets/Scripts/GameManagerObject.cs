using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManagerObject", order = 1)]
public class GameManagerObject : ScriptableObject
{
    [NonSerialized]
    public UnityEvent<bool> collisionEvent;

    private void OnEnable()
    {
        collisionEvent ??= new UnityEvent<bool>();
    }
}
