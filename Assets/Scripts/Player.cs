using Unity.Mathematics;
using UnityEngine;

public class Player : Actor
{
    public Player(string newName, int newMaxHealth) : base(newName, newMaxHealth)
    {
    }
    
    public Action ChooseAction()
    {
        Action action = new Action();
        switch (RngManager.instance.AIRandom.NextInt(1, 3))
        {
            case 1:
                action.actionType = Action.ActionType.Move;
                // Rounding parts to avoid lossy float conversion if the float is marginally below the whole
                // number we're trying to convert to.
                // We get the position via CellToWorld to make sure target positions are always
                // accurate even if the tilemap's settings change.
                action.destination = GridManager.instance.StageMap.CellToWorld(
                    new Vector3Int((int)math.round(position.x), (int)math.round(position.y)) + 
                    new Vector3Int(
                        RngManager.instance.AIRandom.NextInt(-1, 2),
                        RngManager.instance.AIRandom.NextInt(-1, 2),
                        0));
                break;
            case 2:
                action.actionType = Action.ActionType.Act;
                break;
        }

        return action;
    }
}
