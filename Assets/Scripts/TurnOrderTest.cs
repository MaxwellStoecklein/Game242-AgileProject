using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;


// X bounds = -9 to 8
// Y bounds = -5 to 4


public class TurnOrderTest : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    
    List<GameObject> actors = new List<GameObject>();
    List<GameObject> turnList = new List<GameObject>();

    Random random;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        random.InitState();
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                AddActor(playerPrefab, i);
                continue;
            }
            
            if (actors.FindAll(FindPlayer).Count < 4)
            {
                if (random.NextInt(0, 2) == 0)
                {
                    AddActor(playerPrefab, i);
                }
                else
                { 
                    AddActor(enemyPrefab, i);
                }
            }
            else
            {
                AddActor(enemyPrefab, i);
            }
            
            //StartCoroutine(PlayerState());
        }

        void AddActor(GameObject actor, int index)
        {
            actors.Add(Instantiate(actor));
            actors[index].GetComponent<Actor>().name = (index + 1).ToString();
            actors[index].GetComponent<Actor>().maxHealth = 10;

            foreach (GameObject entity in actors)
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
                foreach (GameObject obj in actors)
                {
                    if (Mathf.Approximately(obj.transform.position.x, pos.x) && Mathf.Approximately(obj.transform.position.y, pos.y))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool FindPlayer(GameObject actor)
    {
        return actor.GetComponent<Actor>() is Player;
    }

    
}
