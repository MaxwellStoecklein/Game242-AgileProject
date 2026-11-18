using System;
using System.Collections;
using UnityEngine;

public interface IEntityAction
{
    public string GetName();
    public IEnumerator ActionCoroutine(Actor myActor);
}
