
using System.Collections;
using UnityEngine;

public class CutSkill : SkillAction
{
    public override IEnumerator ActionCoroutine(Actor myActor)
    {
        Debug.Log("Executed cut skill");
        yield return new WaitForSeconds(1f);
    }
}