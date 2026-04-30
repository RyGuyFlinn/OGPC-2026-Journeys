using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;

public class WorldGenSimple : MonoBehaviour
{
    [Header("World Settings")]
    public float worldRadius;
    public float edgeBuffer;
    public float centerBuffer;
    public float shallowsRadius;
    public int extraBiomes;
    public int spawnGridLength;
    public float randomScatter;
    private GameObject worldIslands;

    [Space]

    [Header("Ocean")]
    public GameObject OpenOcean;

    [Space]

    [Header("Islands")]

    public GameObject spawnIsland;

    public GameObject[] islands;
    private List<GameObject> spawnedIslands = new List<GameObject>();
    public GameObject mainIsland;

    public GameObject[] rockyIslands;
    private List<GameObject> spawnedRockyIslands = new List<GameObject>();
    public GameObject mainRockyIsland;

    public GameObject[] glacierIslands;
    private List<GameObject> spawnedGlacierIslands = new List<GameObject>();
    public GameObject mainGlacierIsland;

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
        GameObject mMinimapIsland = Instantiate(mapIsland, new Vector2(10.39f, 3.87f), Quaternion.identity);
        GameObject mIsland = Instantiate(spawnIsland, new Vector2(10.39f, 3.87f), Quaternion.identity);
        mIsland.transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland = mMinimapIsland;
        mIsland.transform.SetParent(worldIslands.transform);

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

                        //add island to the list of islands
                        spawnedIslands.Add(island);
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

                        //add island to the corresponding list of islands
                        if (biomeNum == 0)
                        {
                            spawnedRockyIslands.Add(island);
                        }
                        else if (biomeNum == 1)
                        {
                            spawnedGlacierIslands.Add(island);
                        }
                    }
                }
            }
        }

        int randBiome = 0;

        if (randBiome == 0)
        {
            //replace 1 island in shallows with mainIsland
            int randIsland = Random.Range(0,spawnedIslands.Count);
            Vector3 oldPos = spawnedIslands[randIsland].transform.position;

            GameObject minimapIslandX = spawnedIslands[randIsland].transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland;
            minimapIslandX.GetComponent<MapIsland>().SetMarkedSprite();

            GameObject islandX = Instantiate(mainIsland, oldPos, Quaternion.identity);
            islandX.transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland = minimapIslandX;
            Destroy(spawnedIslands[randIsland]);
        }
        else if (randBiome == 1)
        {
            //replace 1 island in rocky with mainRockyIsland
            int randIsland = Random.Range(0, spawnedRockyIslands.Count);
            Vector3 oldPos = spawnedRockyIslands[randIsland].transform.position;

            GameObject minimapIslandX = spawnedRockyIslands[randIsland].transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland;
            minimapIslandX.GetComponent<MapIsland>().SetMarkedSprite();

            GameObject islandX = Instantiate(mainRockyIsland, oldPos, Quaternion.identity);
            islandX.transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland = minimapIslandX;
            Destroy(spawnedRockyIslands[randIsland]);
        }
        else
        {
            //replace 1 island in glacier with mainGlacierIsland
            int randIsland = Random.Range(0, spawnedGlacierIslands.Count);
            Vector3 oldPos = spawnedGlacierIslands[randIsland].transform.position;

            GameObject minimapIslandX = spawnedGlacierIslands[randIsland].transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland;
            minimapIslandX.GetComponent<MapIsland>().SetMarkedSprite();

            GameObject islandX = Instantiate(mainGlacierIsland, oldPos, Quaternion.identity);
            islandX.transform.GetChild(1).gameObject.GetComponent<EnterIsland>().mapIsland = minimapIslandX;
            Destroy(spawnedGlacierIslands[randIsland]);
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
