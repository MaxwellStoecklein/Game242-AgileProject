using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSheet", menuName = "Actor Assets/StatSheet")]
public class StatSheet : ScriptableObject
{
    public string entityName;

    public int maxHealth;
    public int maxMana;
    public int attack;
    public int defense;
    public int magicAttack;
    public int magicDefense;
    
    /// <summary>
    /// This list starts as strings and should be parsed as EntityAction objects when loaded.
    /// </summary>
    public List<string> actions;
}
