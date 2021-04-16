using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStatus : MonoBehaviour, IDamageable {
    [Header("Player Status")]

    [SerializeField] private CharacterAttributes playerData;

    private int currentLife;

    public bool IsDead { get; private set; }

    public event Action DeathEvent = delegate { };
    public event Action<int> OnUpdateAmmo = delegate { };
    public event Action<float> TakeDamageEvent = delegate { }; //  Percentage


    private void Start() {
        DeathEvent += () => GameController.SetGameState(GameState.PlayerDied);
        HUD.SetupPlayer(playerData.maxLife, ref TakeDamageEvent, OnUpdateAmmo);
        currentLife = playerData.maxLife;
    }

    public virtual bool TakeDamage(int damage) { 
        currentLife -= damage;
        if(currentLife <= 0) {
            DeathEvent();            
            return true;
        }
        TakeDamageEvent.Invoke((float)currentLife / playerData.maxLife); // ex: 100 / 200 = .5%
        return false;
    }
}
