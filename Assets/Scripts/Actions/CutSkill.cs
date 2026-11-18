
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
        // Execute animation
        
        /*
         * while (animation signal not triggered)
         * {
         *      yield return null;
         * }
         */
        
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