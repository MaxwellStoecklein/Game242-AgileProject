using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Actor
{
    private bool queueAction = false;

    public IEntityAction ChooseAction()
    {
        IEntityAction action;
        
        switch (RngManager.instance.AIRandom.NextInt(0, 2))
        {
            case 1:
                MoveAction moveAction = new MoveAction();
                bool validMove = false;
                // Rounding parts to avoid lossy float conversion if the float is marginally below the whole
                // number we're trying to convert to.
                // We get the position via CellToWorld to make sure target positions are always
                // accurate even if the tilemap's settings change.
                int failcount = -1;
                while (!validMove && failcount < 10)
                {
                    failcount++;
                    moveAction.destination = GridManager.instance.StageMap.CellToWorld(
                        new Vector3Int((int)math.round(position.x), (int)math.round(position.y)) + 
                        new Vector3Int(
                            RngManager.instance.AIRandom.NextInt(-1, 2),
                            RngManager.instance.AIRandom.NextInt(-1, 2),
                            0));
                    validMove = TurnSystem.instance.CheckWalkableTile(this, moveAction);
                    validMove = TurnSystem.instance.CheckVacantTile(moveAction);
                }

                if (failcount >= 10)
                {
                    Debug.Log($"{this.name} couldn't move...");
                    MoveAction idle = new MoveAction();
                    idle.destination = GridManager.instance.StageMap.CellToWorld(
                        new Vector3Int((int)math.round(position.x), (int)math.round(position.y)));
                    action = idle;
                }
                else
                {
                    action = moveAction;
                }
                
                break;
            case 2:
                FlashSkill flashSkill = new FlashSkill();
                action = flashSkill;
                break;
            default:
                FlashSkill errorSkill = new FlashSkill();
                action = errorSkill;
                break;
        }

        return action;
    }

    public void AcceptQueueRequest()
    {
        
    }
}
