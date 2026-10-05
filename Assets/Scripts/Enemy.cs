using System.Collections;
using UnityEngine;

public class Enemy : Actor
{
    public Enemy(string newName, int newMaxHealth) : base(newName, newMaxHealth)
    {
    }

    public IEnumerator ChooseAction()
    {
        Random.InitState(Random.Range(0, 100));
        switch (Random.Range(1, 2))
        {
            case 1:
                yield return Move(new Vector3Int(
                    Random.Range(-1, 1),
                    Random.Range(-1, 1),
                    0));
                break;
            case 2:
                yield return Act();
                break;
        }
    }
}
