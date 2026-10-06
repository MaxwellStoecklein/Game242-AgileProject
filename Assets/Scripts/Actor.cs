using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    
    // TODO: Privatize most of the properties.
    [SerializeField] protected string unitName;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Vector3 position;

    [SerializeField] protected GameObject selectionPointer;


    [SerializeField] protected bool isLeader;

    public class Action
    {
        public enum ActionType
        {
            Act,
            Move,
            Idle
        }
        
        public ActionType actionType;
        public Vector3 destination;
    }
    
    #region Setters

    public void SetName(string name)
    {
        unitName = name;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public void SetLeader(bool leader = true)
    {
        isLeader = leader;
    }

    #endregion
    
    #region Getters
    
    public string GetName() => unitName;
    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;
    public Vector3 GetPosition() => position;
    public bool IsLeader() => isLeader;
    
    #endregion
    
    public Actor(string newName, int newMaxHealth)
    {
        name = newName;
        maxHealth = newMaxHealth;
        health = newMaxHealth;
        isLeader = false;
    }

    public Actor()
    {
        name = "";
        maxHealth = 10;
        health = maxHealth;
        isLeader = false;
    }

    /*public IEnumerator Move(Vector3Int direction)
    {
        float timer = 0f;
        Vector3 oldPos = position;
        Vector3 newPos = GridManager.instance.StageMap.CellToWorld((Vector3Int)(position + direction));
        while (timer < 0.5f)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timer / 0.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = newPos;
        position += direction;
    }*/
    
    

    public void MoveTick(float timer, float duration, Vector3 destination)
    {
        transform.position = Vector3.Lerp(position, destination, timer / duration);
    }

    public IEnumerator Act()
    {
        Color temp = GetComponentInChildren<SpriteRenderer>().color;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        float timer = 0f;
        while (timer < 0.3f)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        GetComponentInChildren<SpriteRenderer>().color = temp;
        
        //Debug.Log($"Actor {unitName} acted.");
    }
}
