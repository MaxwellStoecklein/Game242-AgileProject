using UnityEngine;

public abstract class MenuControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void InitialSetup();
    public abstract Vector2 MoveCursor(Vector2 dir);
    public abstract void SelectElement();
    public abstract void Back();
}
