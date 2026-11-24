
using System.Collections;
using UnityEngine;

public class CutSkill : SkillAction
{
    public override string GetName()
    {
        return "Cut";
    }

    public override IEnumerator ActionCoroutine(Actor myActor)
    {
        
        Vector3 dir = myActor.GetFacingRotation();
        Vector3Int dirInt = new Vector3Int(0, 0, 0);
        if (dir.x > 0)
            dirInt.x = 1;
        else if (dir.x < 0)
            dirInt.x = -1;
        if (dir.y > 0)
            dirInt.y = 1;
        else if (dir.y < 0)
            dirInt.y = -1;

        #region Animation

        // Execute animation
        Transform[] oldPos = myActor.GetComponentsInChildren<Transform>();
        Vector3 savedOldPos = Vector3.zero;
        int posIndex = 0;
        for (int i = 0; i < oldPos.Length; i++)
        {
            Debug.Log($"SpriteObjName: {oldPos[i].name}");
            if (oldPos[i].tag == "ActorSprite")
            {
                posIndex = i;
            }
        }

        if (oldPos[posIndex].gameObject.tag == "ActorSprite")
        {
            savedOldPos = oldPos[posIndex].position;
            bool phaseOne = false;
            bool phaseTwo = false;
            float timer = 0f;
            float endTime = 0.15f;
            while (timer < endTime)
            {
                oldPos[posIndex].position = Vector3.Lerp(savedOldPos, savedOldPos + dirInt, timer * (1f / endTime));
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            endTime = 0.1f;
            while (timer < endTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            endTime = 0.05f;
            Vector3 oldDestPos = oldPos[posIndex].position;
            while (timer < endTime)
            {
                oldPos[posIndex].position = Vector3.Lerp(oldDestPos, savedOldPos, timer * (1f / endTime));
                timer += Time.deltaTime;
                yield return null;
            }
            oldPos[posIndex].position = savedOldPos;
        }
        else
        {
            Debug.LogError($"MISSING ACTOR SPRITE: {myActor.gameObject.name}");
        }

        #endregion
        
        // Continue action calcs
        Actor victim = TurnSystem.instance.GetActorAtTile(TurnSystem.instance.GetTilemap().WorldToCell(
            myActor.GetPosition()
            + dir));
        
        if (victim !=null)
        {
            Debug.Log($"Attack hit {victim.name}!");
            victim.Hit(myActor.GetAttack());
        }
        else
        {
            Debug.Log($"No one found at " +
                      $"{TurnSystem.instance.GetTilemap().WorldToCell(myActor.GetPosition() + dir)}" +
                      $"... Attack missed.");
        }
        
        Debug.Log("Executed cut skill");
        yield return new WaitForSeconds(1f);
    }
}