using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MenuControl
{
    private List<GameObject> elements = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitialSetup()
    {
        
    }

    public override Vector2 MoveCursor(Vector2 dir)
    {
        throw new System.NotImplementedException();
    }

    public override void SelectElement()
    {
        throw new System.NotImplementedException();
    }

    public override void Back()
    {
        throw new System.NotImplementedException();
    }
}
