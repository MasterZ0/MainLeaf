using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStatus : MonoBehaviour/*, IDamageable*/, ICollector {
    [SerializeField] private bool imortal;

    [Header("Player Status")]
    [SerializeField] private int arrowCount;

    private int currentLife;

    public bool IsDead { get; private set; }

    public event Action DeathEvent = delegate { };

    public event Action<int> OnUpdateAmmo = delegate { };
    public event Action<float> TakeDamageEvent = delegate { }; //  Percentage


    private void Start() {
        DeathEvent += OnPlayerDeath;
        //HUD.SetupPlayer(0 playerData.maxLife, ref TakeDamageEvent, ref OnUpdateAmmo);
        //currentLife = playerData.maxLife;

        OnUpdateAmmo.Invoke(arrowCount);
    }

    public bool Fire() {
        if (arrowCount < 1)
            return false;

        arrowCount--;
        OnUpdateAmmo.Invoke(arrowCount);
        return true;
    }

    public virtual bool TakeDamage(int damage) { 
        if(!imortal)
            currentLife -= damage;

        if(currentLife <= 0) {
            IsDead = true;
            DeathEvent();            
            return true;
        }
        TakeDamageEvent.Invoke((float)currentLife /*/ playerData.maxLife*/); // ex: 100 / 200 = .5%
        return false;
    }
    public void GetItem(Item item) {
        if(item.itemType == ItemType.Arrow) {
            arrowCount += item.value;
            OnUpdateAmmo.Invoke(arrowCount);

        } else if(item.itemType == ItemType.Life) {
            currentLife += item.value;
            TakeDamageEvent.Invoke((float)currentLife /*/ playerData.maxLife*/);
        }
    }

    private void OnPlayerDeath() {
        GameController.SetGameState(GameState.PlayerDied);
    }

    private void OnDestroy() {
        DeathEvent -= OnPlayerDeath;
    }
}
