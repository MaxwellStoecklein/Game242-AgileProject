using TMPro;
using UnityEngine;

public class DebugInputDisplay : MonoBehaviour
{
    private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vect = ControlManager.instance.PlayerControls.Player.Move.ReadValue<Vector2>();
        text.text = $"Input: < {vect.x} , {vect.y} >";
    }
}
