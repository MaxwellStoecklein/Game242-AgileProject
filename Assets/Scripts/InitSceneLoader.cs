using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneLoader : MonoBehaviour
{
    public List<GameObject> toLoad = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject go in toLoad)
            Instantiate(go);

        StartCoroutine(Waiter());
        
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
