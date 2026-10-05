using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Random = Unity.Mathematics.Random;


// X bounds = -9 to 8
// Y bounds = -5 to 4


public class TurnSystem : MonoBehaviour
{
    public InputSystem_Actions actions;
    [SerializeField] private Tilemap tilemap;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    
    private List<Actor> actors = new();
    private List<Actor> turnList = new();
    private int currentActorTurn = -1;
    private Actor currentActor;

    Random random;

    private bool listenInputs = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Decide Unit turns

        InitTurnOrderState();
        actions = new InputSystem_Actions();
        actions.Enable();
        
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
            
            // Start turns
        }
        NextTurn();
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
        temp.SetName((actors.Count).ToString());
        temp.SetMaxHealth(10);

        foreach (Actor entity in actors)
        {
            Vector3Int newPos = new Vector3Int(random.NextInt(-9, 9), random.NextInt(-5, 5), 0);
            while (FindMatchingPosition(newPos))
            {
                newPos.x = random.NextInt(-9, 9);
                newPos.y = random.NextInt(-5, 5);
            }

            entity.SetPosition(new Vector3Int(newPos.x, newPos.y, 0));
            entity.transform.position = new Vector3(newPos.x, newPos.y, 0); // TODO: Replace with " position = Tilemap.GetPosition(x,y) "
        }
            
            

        bool FindMatchingPosition(Vector3Int pos)
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

    private bool FindPlayer(Actor actor)
    {
        return actor is Player;
    }

    void NextTurn()
    {
        // Player or enemy turn?
        currentActorTurn = (currentActorTurn + 1) % actors.Count;
        currentActor = actors[currentActorTurn];
        Debug.Log($"Turn {currentActor.GetName()} started.\n" +
                  $"Actor is {currentActor.GetType().ToString()}");

        if (currentActor is Player)
        {
            Debug.Log("Starting player turn.");
            StartPlayerTurn();
        }
        else
        {
            Debug.Log("Starting enemy turn.");
            StartEnemyTurn();
        }
    }

    void StartPlayerTurn()
    {
        // On turn start checks (e.g DoTs, )
        
        // set actionable to true (UI and controls scripts check this to see if they should listen to player)
        
        // focus camera on player
        
        // await input response
        listenInputs = true;
        Debug.Log("Set listen inputs to true");
    }

    void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return ((Enemy)currentActor).ChooseAction();
        
        Debug.Log($"Enemy {currentActor.GetName()} moved.");
        NextTurn();
    }

    IEnumerator PlayerMoveCoroutine()
    {
        yield return currentActor.Move(new Vector3Int(0, 1, 0));
        
        Debug.Log($"Actor {currentActor.GetName()} moved.");
        NextTurn();
    }

    IEnumerator PlayerActCoroutine()
    {
        yield return currentActor.Act();
        
        Debug.Log($"Actor {currentActor.GetName()} acted.");
        NextTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (listenInputs)
        {
            if (actions.Player.mv.triggered)
            {
                listenInputs = false;
                //currentActor.Move(new Vector3Int(0, 1, 0));
                //StartCoroutine(currentActor.Move(new Vector3Int(0, 1, 0)));
                StartCoroutine(PlayerMoveCoroutine());
            }

            if (actions.Player.act.triggered)
            {
                listenInputs = false;
                StartCoroutine(PlayerActCoroutine());
            }
        }
    }
}
