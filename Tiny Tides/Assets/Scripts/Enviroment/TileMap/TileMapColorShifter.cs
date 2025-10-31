using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapColorShifter : MonoBehaviour
{
    public Tilemap tilemap;
    [Range(0f, 1f)]
    public float minBrightness = 0.3f; // how dark tiles get at Z=0

    void Start()
    {
        ColorizeTiles();
    }

    [ContextMenu("Recolor Tiles")]
    public void ColorizeTiles()
    {
        if (tilemap == null)
            tilemap = GetComponent<Tilemap>();

        // Find the highest Z among all tiles
        float highestZ = float.MinValue;
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                float z = tilemap.CellToWorld(pos).z;
                if (z > highestZ)
                    highestZ = z;
            }
        }

        // Colorize tiles based on their Z relative to the highest Z
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos))
                continue;

            Vector3 worldPos = tilemap.CellToWorld(pos);
            Color baseColor = tilemap.GetColor(pos);

            // Normalize Z between 0 and 1
            float normalizedZ = Mathf.InverseLerp(0f, highestZ, worldPos.z);
            float brightness;
            // Interpolate brightness between minBrightness and full (1)
            if (worldPos.z == 0)
            {
                brightness = Mathf.Lerp(minBrightness, 1f, 1);
            }
            else
            {
                brightness = Mathf.Lerp(minBrightness, 1f, normalizedZ);
            }

            Color adjusted = new Color(
                baseColor.r * brightness,
                baseColor.g * brightness,
                baseColor.b * brightness,
                baseColor.a
            );

            tilemap.SetTileFlags(pos, TileFlags.None);
            tilemap.SetColor(pos, adjusted);
        }
    }
}