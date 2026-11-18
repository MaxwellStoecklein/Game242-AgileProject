
using System.Collections;

public abstract class SkillAction : IEntityAction
{
    public abstract string GetName();
    public abstract IEnumerator ActionCoroutine(Actor myActor);
}