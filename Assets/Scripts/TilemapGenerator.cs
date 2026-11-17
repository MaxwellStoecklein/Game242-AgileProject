using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    [ContextMenu("Generate Tilemap")]
    public void GenerateTilemap()
    {
        Tilemap tilemap = GetComponentInChildren<Tilemap>();
        if (tilemap != null)
        {
            foreach (TileBase tile in tilemap.GetUsedTiles())
            {
                Tile temp = tile as Tile;
                ExtendedTile extendedTile = ScriptableObject.CreateInstance<ExtendedTile>();
                extendedTile.color = temp.color;
                extendedTile.sprite = temp.sprite;
                extendedTile.flags = temp.flags;
                extendedTile.transform = temp.transform;
                
                string[] split = AssetDatabase.GetAssetPath(tile).Split("/");
                string path = "";
                for (int i = 0; i < split.Length - 2; i++)
                {
                    path += split[i] + "/";
                }

                path += "ExtendedTiles/" + tile.name + ".asset";
                //Debug.Log(AssetDatabase.GetAssetPath(tile));
                //Debug.Log(extendedTile.name);
                AssetDatabase.CreateAsset(extendedTile, path);
                ExtendedTile tmp = AssetDatabase.LoadAssetAtPath(path, typeof(ExtendedTile)) as ExtendedTile;
                tilemap.SwapTile(tile, tmp);
                Debug.Log(tmp.name);
            }
        }
    }
}
