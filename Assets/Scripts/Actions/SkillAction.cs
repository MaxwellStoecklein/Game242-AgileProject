
using System.Collections;

public abstract class SkillAction : IEntityAction
{
    public abstract IEnumerator ActionCoroutine(Actor myActor);
}