using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ExtraTileData", menuName = "Scriptable Objects/ExtraTileData")]
public class ExtendedTile : Tile
{
    /*public ExtendedTile(Tile oldTile)
    {
        color = oldTile.color;
        flags = oldTile.flags;
        sprite = oldTile.sprite;
        transform = oldTile.transform;
    }*/
    
    public enum TileType
    {
        Lava,
        Floor,
        Wall,
        Water,
        Air
    }
    
    [Header("Configuration")]
    public TileType tileType;
}
