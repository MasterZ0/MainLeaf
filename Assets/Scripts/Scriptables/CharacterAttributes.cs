using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Scriptable Objects/Character Data", order = 1)]
public class CharacterAttributes : ScriptableObject {

    public int maxLife;         // Resistence
    public float moveSpeed;       // Agility

    public int attackDamage;    // Physical
    public int abilityPower;    // Magic (Damage/Support)
    //public int armor;             100 / (100 + 100)
    //public int magicResistence;    // 10 armadura = 10 de vida = 11 vida?

    // Enum hability style {Mana, Furia, None} -> Mana = Value
    //Lifesteel, vampirismo magico , velocidade de attack, penetra��o de armadura,  penetra��o magica, dano verdadeiro, tempo de recarga, %regenera��o de vida/mana, % acerto critico
    // debuff: slow, stun, %dano da vida m�xima, Acelera��o de Habilidade, escudo, %cura, redu��o de cura (n�o soma, apenas aumenta at� o limite)
    //Buff: Extra XP, Sorte
}
