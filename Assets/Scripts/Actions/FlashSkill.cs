using System.Collections;
using UnityEngine;

public class FlashSkill : SkillAction
{
    public override IEnumerator ActionCoroutine(Actor myActor)
    {
        Color temp = myActor.GetComponentInChildren<SpriteRenderer>().color;
        myActor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        float timer = 0f;
        while (timer < 0.3f)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        myActor.GetComponentInChildren<SpriteRenderer>().color = temp;
    }
}