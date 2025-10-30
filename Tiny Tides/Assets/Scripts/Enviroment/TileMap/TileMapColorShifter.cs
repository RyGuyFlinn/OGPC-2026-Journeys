using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapColorShifter : MonoBehaviour
{
    public Tilemap tilemap;
    [Range(0f, 1f)]
    public float darkenPerUnit = 0.1f; // how much darker per world-space Z unit

    void Start()
    {
        ColorizeTiles();
    }

    [ContextMenu("Recolor Tiles")]
    public void ColorizeTiles()
    {
        if (tilemap == null)
            tilemap = GetComponent<Tilemap>();

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos))
                continue;

            Vector3 worldPos = tilemap.CellToWorld(pos);
            Color baseColor = tilemap.GetColor(pos);

            // brightness factor: closer to 0 -> darker
            float factor = Mathf.Clamp01(darkenPerUnit * worldPos.z);

            Color adjusted = new Color(
                baseColor.r * factor,
                baseColor.g * factor,
                baseColor.b * factor,
                1f
            );

            tilemap.SetTileFlags(pos, TileFlags.None);
            tilemap.SetColor(pos, adjusted);
        }
    }
}