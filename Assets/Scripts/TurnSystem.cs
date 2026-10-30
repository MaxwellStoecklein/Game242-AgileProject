using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    
    private Dictionary<Actor, MoveAction> moveQueue = new();
    private Dictionary<Actor, IEntityAction> actionQueue = new();
    
    private int currentActorTurn = -1;
    private Actor currentActor;

    private bool listenInputs = false;
    public bool manualMode = true;
    
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
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                AddActor(playerPrefab);
                actors[i].SetLeader();
                continue;
            }
            
            if (actors.FindAll(FindPlayer).Count < 4)
            {
                if (RngManager.instance.SpawnRandom.NextInt(0, 2) == 0)
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
        }
        NextTurn();
    }
    
    void AddActor(GameObject actor)
    {
        Actor temp;
        actors.Add(temp = Instantiate(actor).GetComponent<Actor>());
        temp.SetName((actors.Count).ToString());
        temp.SetMaxHealth(10);

        foreach (Actor entity in actors)
        {
            Vector3Int newPos = new Vector3Int(RngManager.instance.SpawnRandom.NextInt(-9, 9), 
                RngManager.instance.SpawnRandom.NextInt(-5, 5), 0);
            while (FindMatchingPosition(newPos))
            {
                newPos.x = RngManager.instance.SpawnRandom.NextInt(-9, 9);
                newPos.y = RngManager.instance.SpawnRandom.NextInt(-5, 5);
            }

            entity.SetPosition(new Vector3Int(newPos.x, newPos.y, 0));
            entity.transform.position = new Vector3(newPos.x, newPos.y, 0);
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
        Debug.Log($"Turn {currentActor.GetName()} started.\n" + $"Actor is {currentActor.GetType().ToString()}");

        if (currentActor is Player)
        {
            //Debug.Log("Starting player turn.");
            if (manualMode)
            {
                StartPlayerTurn();
            }
            else if (currentActor.IsLeader())
            {
                StartPlayerTurn();
            }
            else
            {
                StartPlayerAITurn();
            }
        }
        else
        {
            //Debug.Log("Starting enemy turn.");
            StartEnemyTurn();
        }
    }

    void StartPlayerAITurn()
    {
        //Actor.Action playerAIAction = ((Player)currentActor).ChooseAction();
        IEntityAction playerAIAction = ((Player)currentActor).ChooseAction();
        StartCoroutine(ExecuteAction(playerAIAction));
    }
    
    void StartPlayerTurn()
    {
        // On turn start checks (e.g DoTs, )
        
        // set actionable to true (UI and controls scripts check this to see if they should listen to player)
        
        // focus camera on player
        
        // await input response
        listenInputs = true;
        //Debug.Log("Set listen inputs to true");
    }

    void StartEnemyTurn()
    {
        Debug.Log("Starting enemy turn");
        //Actor.Action enemyAction = ((Enemy)currentActor).ChooseAction();
        IEntityAction enemyAction = ((Enemy)currentActor).ChooseAction();
        StartCoroutine(ExecuteAction(enemyAction));
    }

    IEnumerator ExecuteAction(IEntityAction action)
    {
        // TODO: Add if action is moveaction
        Debug.Log($"Got {action.GetType().Name} from {currentActor.GetName()}");
        if (action is MoveAction)
        {
            Debug.Log($"Actor {currentActor.GetName()} moved.");
            moveQueue.Add(currentActor, (MoveAction)action);
        }
        else if (action is SkillAction)
        {
            if (currentActor is Player)
            {
                yield return StartCoroutine(MoveQueueCoroutine());
                yield return StartCoroutine(ActQueueCoroutine());
                Debug.Log($"Actor {currentActor.GetName()} executed act! They're a player so they skipped queue.");
                yield return StartCoroutine(PlayerActCoroutine());
            }
            else
            {
                Debug.Log($"Actor {currentActor.GetName()} acted. They're an enemy so their act was held.");
                actionQueue.Add(currentActor, action);
            }
        }

        if (actors[(currentActorTurn + 1) % actors.Count] is Player)
        {
            Debug.Log("Next turn is player.");
            if (manualMode || actors[(currentActorTurn + 1) % actors.Count].IsLeader())
            {
                Debug.Log("Manual Mode is on or next player is the leader");
                yield return StartCoroutine(MoveQueueCoroutine());

                yield return StartCoroutine(ActQueueCoroutine());
            }
            else if (!manualMode)
            {
                yield return StartCoroutine(ActQueueCoroutine());
                /*foreach (var queuedActor in actionQueue)
                {
                    Debug.Log($"Actor {queuedActor.Key.GetName()} executed act!");
                    yield return StartCoroutine(queuedActor.Value.ActionCoroutine(queuedActor.Key)); // TODO: Change to some kinda ai act coro
                }*/
            }
        }
        
        NextTurn();
    }
    
    /*IEnumerator ExecuteAction(Actor.Action action)
    {
        if (action.actionType == Actor.Action.ActionType.Move)
        {
            Debug.Log($"Actor {currentActor.GetName()} moved.");
            moveQueue.Add(currentActor, action);
        }
        else if (action.actionType == Actor.Action.ActionType.Act)
        {
            if (currentActor is Player)
            {
                yield return StartCoroutine(MoveQueueCoroutine());
                yield return StartCoroutine(ActQueueCoroutine());
                Debug.Log($"Actor {currentActor.GetName()} executed act! They're a player so they skipped queue.");
                yield return StartCoroutine(PlayerActCoroutine());
            }
            else
            {
                Debug.Log($"Actor {currentActor.GetName()} acted. They're an enemy so their act was held.");
                actionQueue.Add(currentActor, action);
            }
        }

        if (actors[(currentActorTurn + 1) % actors.Count] is Player)
        {
            if (manualMode || actors[(currentActorTurn + 1) % actors.Count].IsLeader())
            {
                yield return StartCoroutine(MoveQueueCoroutine());

                yield return StartCoroutine(ActQueueCoroutine());
            }
            /*else if (!manualMode)
            {
                foreach (var queuedActor in actionQueue)
                {
                    Debug.Log($"Actor {queuedActor.Key.GetName()} executed act!");
                    yield return StartCoroutine(queuedActor.Key.Act()); // TODO: Change to some kinda ai act coro
                }
            }#1#
        }
        
        NextTurn();
    }*/

    IEnumerator ActQueueCoroutine()
    {
        Debug.Log("Action queue started");
        foreach (var queuedActor in actionQueue)
        {
            Debug.Log($"Actor {queuedActor.Key.GetName()} executed act!");
            yield return StartCoroutine(queuedActor.Value.ActionCoroutine(queuedActor.Key)); // TODO: Change to some kinda ai act coro
        }
        Debug.Log("Action queue ended");
        actionQueue.Clear();
    }
    
    IEnumerator MoveQueueCoroutine()
    {
        float timer = 0f;
        float gameSpeed = 0.5f; // TODO: When creating config system, make this configurable
        
        while (timer < gameSpeed)
        {
            foreach (var queuedActor in moveQueue)
            {
                queuedActor.Key.MoveTick(timer, gameSpeed, queuedActor.Value.destination);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        foreach (var queuedActor in moveQueue)
        {
            Debug.Log($"Actor {queuedActor.Key.GetName()} executed move!");
            
            queuedActor.Key.SetPosition(queuedActor.Value.destination);
        }
        moveQueue.Clear();
    }

    public void PlayerMove(Vector3Int direction)
    {
        //Actor.Action playerAction = new Actor.Action();
        MoveAction playerAction = new MoveAction();
        playerAction.destination = GridManager.instance.StageMap.CellToWorld(
            new Vector3Int((int)math.round(currentActor.GetPosition().x),
                (int)math.round(currentActor.GetPosition().y)) +
            direction);
        
        StartCoroutine(ExecuteAction(playerAction));
    }

    IEnumerator PlayerActCoroutine()
    {
        yield return currentActor.Act();
    }

    // Update is called once per frame
    void Update()
    {
        if (listenInputs)
        {
            /*if (actions.Player.Move.ReadValue<Vector2>().magnitude > 0.1)
            {
                
            }*/
            
            if (actions.Player.Move.inProgress)
            {
                listenInputs = false;

                Vector2 dir = actions.Player.Move.ReadValue<Vector2>();
                
                PlayerMove(new Vector3Int(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y)));
            }

            if (actions.Player.Attack.triggered)
            {
                listenInputs = false;
                
                StartCoroutine(PlayerActCoroutine());
            }
        }
    }
}
