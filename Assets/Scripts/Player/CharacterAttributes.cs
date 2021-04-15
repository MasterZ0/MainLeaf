using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : ScriptableObject {

    public int maxLife;         // Resistence
    public int moveSpeed;       // Agility

    public int attackDamage;    // Physical
    public int abilityPower;    // Magic (Damage/Support)
    //public int armor;             100 / (100 + 100)
    //public int magicResistence;    // 10 armadura = 10 de vida = 11 vida?

    // Enum hability style {Mana, Furia, None} -> Mana = Value
    //Lifesteel, vampirismo magico , velocidade de attack, penetração de armadura,  penetração magica, dano verdadeiro, tempo de recarga, %regeneração de vida/mana, % acerto critico
    // debuff: slow, stun, %dano da vida máxima, Aceleração de Habilidade, escudo, %cura, redução de cura (não soma, apenas aumenta até o limite)
    //Buff: Extra XP, Sorte
}
