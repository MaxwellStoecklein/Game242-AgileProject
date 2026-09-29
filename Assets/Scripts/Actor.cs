using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public string name;
    
    public int health;
    public int maxHealth;
    
    public Actor(string newName, int newMaxHealth)
    {
        name = newName;
        maxHealth = newMaxHealth;
        health = newMaxHealth;
    }

    public void Move()
    {
        Debug.Log($"Actor {name} moved.");
    }

    public void Act()
    {
        Debug.Log($"Actor {name} acted.");
    }
}
