using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    
    // TODO: Privatize most of the properties.
    [SerializeField] protected string unitName;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int attack;
    [SerializeField] protected int defense;
    [SerializeField] protected int magicAttack;
    [SerializeField] protected int magicDefense;

    [SerializeField] protected List<string> actions;
    
    [SerializeField] protected Vector3 position;
    [SerializeField] protected Vector2 facingRotation;
    [SerializeField] protected GameObject selectionPointer;
    [SerializeField] protected bool isLeader;

    [SerializeField] protected StatSheet statSheet;

    /*public class Action
    {
        public enum ActionType
        {
            Act,
            Move,
            Idle
        }
        
        public ActionType actionType;
        public Vector3 destination;
    }*/
    
    #region Setters

    public void SetName(string newName)
    {
        unitName = newName;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    public void SetPosition(Vector3 newPosition)
    {
        this.position = newPosition;
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
    /// <summary>
    /// Returns the player's rotation based on the last rotating input.
    /// </summary>
    /// <remarks>WARNING: This does NOT account for tilemap positions. Diagonals WILL return stuff like 0.71... and so on.
    /// MAKE SURE TO CORRECT THE RETURN VALUE TO PROPER INTEGERS WHEN APPLICABLE.</remarks>
    public Vector2 GetFacingRotation() => facingRotation;
    public bool IsLeader() => isLeader;
    public StatSheet GetStatSheet() => statSheet;
    public int GetAttack() => attack;
    public int GetDefense() => defense;
    public int GetMagicAttack => magicAttack;
    public int GetMagicDefense => magicDefense;
    public List<string> GetActions() => actions;
    
    #endregion
    
    /*public Actor(string newName, int newMaxHealth)
    {
        name = newName;
        maxHealth = newMaxHealth;
        health = newMaxHealth;
        isLeader = false;
        facingRotation = Vector2.down;
    }

    public Actor()
    {
        name = "";
        maxHealth = 10;
        health = maxHealth;
        isLeader = false;
        facingRotation = Vector2.down;
    }*/

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

    public void InitStats()
    {
        unitName = statSheet.entityName;
        health = statSheet.maxHealth;
        maxHealth = statSheet.maxHealth;
        attack = statSheet.attack;
        defense = statSheet.defense;
        magicAttack = statSheet.magicAttack;
        magicDefense = statSheet.magicDefense;
        actions = new List<string>(statSheet.actions);
    }

    public void Hit(int damage)
    {
        health -= damage - defense / 2;
    }

    public void Rotate(Vector2 rotDir)
    {
        facingRotation = rotDir;
        
        // TODO: Change sprite to new rotation.
    }

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
