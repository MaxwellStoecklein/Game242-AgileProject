using System.Collections;
using UnityEngine;

public class MoveAction : IEntityAction
{
    public Vector3 destination;
    
    public IEnumerator ActionCoroutine(Actor myActor)
    {
        throw new System.NotImplementedException();
    }
}