using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class RngManager : MonoBehaviour
{
    public static RngManager instance;

    //private Random mapRandom;
    //private Random spawnRandom;
    //private Random aiRandom;
    
    public Random MapRandom;
    public Random SpawnRandom;
    public Random AIRandom;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapRandom = new Random((uint)System.DateTime.Now.Millisecond);
        SpawnRandom = new Random((uint)System.DateTime.Now.Millisecond);
        AIRandom = new Random((uint)System.DateTime.Now.Millisecond);
        
    }
}
