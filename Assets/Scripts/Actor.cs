using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    
    // TODO: Privatize most of the properties.
    [SerializeField] private string unitName;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Vector3Int position;

    [SerializeField] private GameObject selectionPointer;

    #region Setters

    public void SetName(string name)
    {
        unitName = name;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    public void SetPosition(Vector3Int position)
    {
        this.position = position;
    }

    #endregion
    
    #region Getters
    
    public string GetName() => unitName;
    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;
    
    #endregion
    
    public Actor(string newName, int newMaxHealth)
    {
        name = newName;
        maxHealth = newMaxHealth;
        health = newMaxHealth;
    }

    public Actor()
    {
        name = "";
        maxHealth = 10;
        health = maxHealth;
    }

    public IEnumerator Move(Vector3Int direction)
    {
        float timer = 0f;
        Vector3 oldPos = position;
        Vector3 newPos = GridManager.instance.StageMap.CellToWorld(position + direction);
        while (timer < 0.5f)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timer / 0.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = newPos;
        position += direction;
    }

    public IEnumerator Act()
    {
        while (false)
            yield return null;
        Debug.Log($"Actor {unitName} acted.");
    }
}
