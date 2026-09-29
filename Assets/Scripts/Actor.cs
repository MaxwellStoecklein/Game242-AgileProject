using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    
    // TODO: Privatize most of the properties.
    [SerializeField] private string name;

    [SerializeField] private int health;
    public int maxHealth;

    [SerializeField] private GameObject selectionPointer;
    
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
