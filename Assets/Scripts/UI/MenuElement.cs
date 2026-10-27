using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MenuElement", menuName = "Scriptable Objects/MenuElement")]
public class MenuElement : ScriptableObject
{
    public enum oldbeha
    {
        Standalone,
        AddWindow
    }
    
    public enum ContentType
    {
        ButtonList,
        ObjectDescription,
        ActionList,
        SpellList,
        Inventory,
    }
    
    
    public string menuName;
    public oldbeha oldbeeha;
}
