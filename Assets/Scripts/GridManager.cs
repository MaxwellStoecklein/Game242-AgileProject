using System;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    private Grid stageMap;

    #region Getters

    public Grid StageMap => stageMap;

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        stageMap = GetComponent<Grid>();
    }
}
