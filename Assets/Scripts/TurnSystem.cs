using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;


// X bounds = -9 to 8
// Y bounds = -5 to 4


public class TurnSystem : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    
    private List<Actor> actors = new();
    private List<Actor> turnList = new();

    Random random;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        
        
        
    }

    void InitTurnOrderState()
    {
        random.InitState();
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                AddActor(playerPrefab);
                continue;
            }
            
            if (actors.FindAll(FindPlayer).Count < 4)
            {
                if (random.NextInt(0, 2) == 0)
                {
                    AddActor(playerPrefab);
                }
                else
                { 
                    AddActor(enemyPrefab);
                }
            }
            else
            {
                AddActor(enemyPrefab);
            }
            
            //StartCoroutine(PlayerState());
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="index"></param>
    void AddActor(GameObject actor)
    {
        Actor temp;
        actors.Add(temp = Instantiate(actor).GetComponent<Actor>());
        temp.name = (actors.Count).ToString();
        temp.maxHealth = 10;

        foreach (Actor entity in actors)
        {
            Vector2Int newPos = new Vector2Int(random.NextInt(-9, 9), random.NextInt(-5, 5));
            while (FindMatchingPosition(newPos))
            {
                newPos.x = random.NextInt(-9, 9);
                newPos.y = random.NextInt(-5, 5);
            }

            entity.transform.position = new Vector3(newPos.x, newPos.y, 0);
        }
            
            

        bool FindMatchingPosition(Vector2Int pos)
        {
            foreach (Actor obj in actors)
            {
                if (Mathf.Approximately(obj.transform.position.x, pos.x) && Mathf.Approximately(obj.transform.position.y, pos.y))
                {
                    return true;
                }
            }
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool FindPlayer(Actor actor)
    {
        return actor is Player;
    }

    
}
