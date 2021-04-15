using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStatus : MonoBehaviour, IDamageable {
    [Header("Player Status")]

    [SerializeField] private CharacterAttributes playerData;

    private int currentLife;

    public bool IsDead { get; private set; }

    public event Action DeathEvent;
    public event Action<float> TakeDamageEvent;

    private void Start() {
        DeathEvent += GameController.Instance.OnPlayerDeath;
        DeathEvent += HUD.Instance.OnPlayerDeath;
        TakeDamageEvent += HUD.Instance.UpdateLife;
        HUD.Instance.PlayerSetup(playerData.maxLife);
        currentLife = playerData.maxLife;
    }

    public virtual bool TakeDamage(int damage) { 
        currentLife -= damage;
        if(currentLife <= 0) {
            DeathEvent();            
            return true;
        }
        TakeDamageEvent(currentLife / playerData.maxLife); // ex: 100 / 200 = .5%
        return false;
    }

}
