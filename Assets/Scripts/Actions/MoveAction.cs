using System.Collections;
using UnityEngine;

public class MoveAction : IEntityAction
{
    public string GetName()
    {
        return GetType().Name;
    }
    
    public Vector3 destination;

    public IEnumerator ActionCoroutine(Actor myActor)
    {
        // Movement logic has to be handled elsewhere so this function goes unused for MoveActions.
        // Class mainly exists to store the destination tile and its name to determine what the action is.
        throw new System.NotImplementedException();
    }
}