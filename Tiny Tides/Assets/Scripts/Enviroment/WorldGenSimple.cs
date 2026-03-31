using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldGenSimple : MonoBehaviour
{
    public float worldRadius;
    public float edgeBuffer;
    public float centerBuffer;
    public float shallowsRadius;
    public int extraBiomes;
    public int spawnGridLength;
    public float randomScatter;
    private GameObject worldIslands;

    public GameObject OpenOcean;

    [Space]
    public GameObject spawnIsland;
    public GameObject[] islands;
    public GameObject[] rockyIslands;
    public GameObject[] glacierIslands;

    [Space]
    public GameObject mapIsland;
    public GameObject minimap;

    // Start is called before the first frame update
    void Start()
    {
        worldIslands = GameObject.Find("WorldIslands");

        //spawn open ocean
        Instantiate(OpenOcean, transform.position, Quaternion.identity);

        //spawn main island
        Instantiate(spawnIsland, new Vector2(10.39f, 3.87f), Quaternion.identity);

        //instantiate islands accross a grid
        for (int col = 0; col < spawnGridLength; col++)
        {
            for (int row = 0; row < spawnGridLength; row++)
            {
                //the current position on the grid
                Vector2 spawnPos = new Vector2(
                    (col * 2 * worldRadius) / spawnGridLength - worldRadius, 
                    (row * 2 * worldRadius) / spawnGridLength - worldRadius);

                //a random offset of a set size
                Vector2 spawnOffset = new Vector2(
                    Random.Range(-randomScatter, randomScatter),
                    Random.Range(-randomScatter, randomScatter));

                //move the current position on the grid by the random offset
                spawnPos += spawnOffset;

                //check if the final position is within the world border, if so, instantiate a random island at it
                if (spawnPos.magnitude < worldRadius - edgeBuffer &&
                    (spawnPos.magnitude > shallowsRadius + edgeBuffer ||
                    spawnPos.magnitude < shallowsRadius - edgeBuffer) && 
                    spawnPos.magnitude > centerBuffer)
                {
                    //spawn island on map and set a random sprite for it
                    Vector2 offset = (spawnPos / worldRadius) * minimap.GetComponent<MinimapManager>().mapRadius;
                    Vector2 mapPos = new Vector2(minimap.transform.GetChild(0).transform.position.x, minimap.transform.GetChild(0).transform.position.y) + offset;
                    GameObject minimapIsland = Instantiate(mapIsland, mapPos, Quaternion.identity);
                    minimapIsland.transform.SetParent(minimap.transform.GetChild(0));

                    //spawn island in world and link it to its corresponding map icon
                    if (Vector2.Distance(spawnPos, new Vector2(0f, 0f)) < shallowsRadius)
                    {
                        minimapIsland.GetComponent<MapIsland>().SetRandomSprite(-1);

                        GameObject island = Instantiate(islands[Random.Range(0, islands.Length)], spawnPos, Quaternion.identity);
                        island.transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland = minimapIsland;
                        island.transform.SetParent(worldIslands.transform);
                    }
                    else
                    {
                        //if the island is not in the shallows, then calculate which biome it belongs in
                        float angle = Vector2.Angle(new Vector2(0f, 1f), spawnPos);
                        if (spawnPos.x < 0)
                        {
                            angle = 360 - angle;
                        }
                        int biomeNum = Mathf.FloorToInt(angle * extraBiomes / 360);

                        minimapIsland.GetComponent<MapIsland>().SetRandomSprite(biomeNum);

                        GameObject island = Instantiate(GetIsland(biomeNum), spawnPos, Quaternion.identity);
                        island.transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland = minimapIsland;
                        island.transform.SetParent(worldIslands.transform);
                    }
                }
            }
        }
    }

    public GameObject GetIsland(int biome)
    {
        if (biome == 0 )
        {
            return rockyIslands[Random.Range(0, rockyIslands.Length)];
        }
        else if (biome == 1)
        {
            return glacierIslands[Random.Range(0, glacierIslands.Length)];
        }
        else
        {
            return islands[Random.Range(0, islands.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
