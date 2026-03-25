using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGeneration : MonoBehaviour
{
    public static WorldGeneration Instance;

    public GameObject OpenOcean;

    [Header("Biomes")]
    public List<GameObject> Biomes;
    public int BiomePositionOffset = 50;
    public int BiomeSizeMin = 50;
    public int BiomeSizeMax = 75;

    private float biomeAngle;
    private Vector2 SpawnPos;

    [Header("Islands")]
    public GameObject StarterIsland;

    [Space]
    public GameObject MainRedIsland;
    public GameObject MainOrangeIsland;
    public GameObject MainGreenIsland;
    public GameObject MainPurpleIsland;
    public GameObject MainBrownIsland;

    [Header("Normal/Filler Islands")]
    public int MaxIslandCount = 5;
    public int MinIslandCount = 3;

    [Space]

    public float IslandCheckRadius = 5f;
    public LayerMask IslandLayer;

    [Space]
    public GameObject[] BasicIslands;
    public GameObject[] RedIslands;
    public GameObject[] OrangeIslands;
    public GameObject[] GreenIslands;
    public GameObject[] PurpleIslands;
    public GameObject[] BrownIslands;

    [Space]
    public List<GameObject> islands;

    [Header("Player")]
    public GameObject playerBoat;
    public GameObject player;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Spawn in an empty circle for the open ocean
        Instantiate(OpenOcean, transform.position, transform.rotation);

        //Spawn in the start island in the center of the world
        Instantiate(StarterIsland, transform.position, transform.rotation);

        //Spawn 3 Biomes randomly on the map
        SpawnBiomes();
    }

    void Update()
    {
        /*
         * Don't want the demo people accidently pressing this :)
         *
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        */
    }    

    private void SpawnBiomes()
    {
        float BaseAngle = Mathf.Deg2Rad * Random.Range(0, 120);
        for (int i = 0; i < 3; i++)
        {
            //Get Random Biome Position On circumfrance of the World
            biomeAngle = (BaseAngle + Mathf.Deg2Rad * Random.Range(-25, 25)) % (2 * Mathf.PI);
            SpawnPos = new Vector2(Mathf.Cos(biomeAngle), Mathf.Sin(biomeAngle)) * 100;

            //Spawn in a random biome at the random position
            int RandomBiomeIndex = Random.Range(0, Biomes.Count);
            GameObject Biome = Instantiate(Biomes[RandomBiomeIndex], SpawnPos, transform.rotation);

            //Remove the biome just spawned from the biomes list so that the same biome cant spawn multiple times in the same run.
            Biomes.RemoveAt(RandomBiomeIndex);

            //Change the Biome size between the min and max values
            float RandomBiomeSize = Random.Range(BiomeSizeMin, BiomeSizeMax);
            Vector3 BiomeSize = new Vector3(RandomBiomeSize, RandomBiomeSize, 1);
            Biome.transform.localScale = BiomeSize;

            BaseAngle += Mathf.PI * 2 / 3;
            
            //Spawn in the main islands into the designated biomes
            if (Biome.CompareTag("RedBiome")) {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 90;

                Vector2 centerPosition = transform.localPosition;

                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                Vector2 worldPos = (Vector2)Biome.transform.position + localOffset;

                float distance = Vector3.Distance(worldPos, centerPosition); //distance from potential island to the center of the map

                if (distance > worldRadius) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector2 fromOriginToObject = worldPos - centerPosition;
                    fromOriginToObject *= worldRadius / distance;
                    worldPos = centerPosition + fromOriginToObject; 
                }

                GameObject island = Instantiate(MainRedIsland, worldPos, GetIslandRotation(worldPos));

                island.transform.parent = Biome.transform;

                islands.Add(island);                
            }

            if (Biome.CompareTag("PurpleBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 90;

                Vector2 centerPosition = transform.localPosition;

                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                Vector2 worldPos = (Vector2)Biome.transform.position + localOffset;

                float distance = Vector3.Distance(worldPos, centerPosition); //distance from ~green object~ to *black circle*

                if (distance > worldRadius) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector2 fromOriginToObject = worldPos - centerPosition;
                    fromOriginToObject *= worldRadius / distance;
                    worldPos = centerPosition + fromOriginToObject;
                }

                GameObject island = Instantiate(MainPurpleIsland, worldPos, GetIslandRotation(worldPos));

                island.transform.parent = Biome.transform;

                islands.Add(island);
            }

            if (Biome.CompareTag("GreenBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 90;

                Vector2 centerPosition = transform.localPosition;

                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                Vector2 worldPos = (Vector2)Biome.transform.position + localOffset;

                float distance = Vector3.Distance(worldPos, centerPosition); //distance from ~green object~ to *black circle*

                if (distance > worldRadius) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector2 fromOriginToObject = worldPos - centerPosition;
                    fromOriginToObject *= worldRadius / distance;
                    worldPos = centerPosition + fromOriginToObject;
                }

                GameObject island = Instantiate(MainGreenIsland, worldPos, GetIslandRotation(worldPos));

                island.transform.parent = Biome.transform;

                islands.Add(island);
            }

            if (Biome.CompareTag("OrangeBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 90;

                Vector2 centerPosition = transform.localPosition;

                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                Vector2 worldPos = (Vector2)Biome.transform.position + localOffset;

                float distance = Vector3.Distance(worldPos, centerPosition); //distance from ~green object~ to *black circle*

                if (distance > worldRadius) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector2 fromOriginToObject = worldPos - centerPosition;
                    fromOriginToObject *= worldRadius / distance;
                    worldPos = centerPosition + fromOriginToObject;
                }

                GameObject island = Instantiate(MainOrangeIsland, worldPos, GetIslandRotation(worldPos));

                island.transform.parent = Biome.transform;

                islands.Add(island);
            }

            if (Biome.CompareTag("BrownBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 90;

                Vector2 centerPosition = transform.localPosition;

                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                Vector2 worldPos = (Vector2)Biome.transform.position + localOffset;

                float distance = Vector3.Distance(worldPos, centerPosition); //distance from ~green object~ to *black circle*

                if (distance > worldRadius) //If the distance is less than the radius, it is already within the circle.
                {
                    Vector2 fromOriginToObject = worldPos - centerPosition;
                    fromOriginToObject *= worldRadius / distance;
                    worldPos = centerPosition + fromOriginToObject;
                }

                GameObject island = Instantiate(MainBrownIsland, worldPos, GetIslandRotation(worldPos));

                island.transform.parent = Biome.transform;

                islands.Add(island);
            }

            //Spawn the filler islands all around the world
            SpawnIslands(Biome, RandomBiomeSize);
        }
    }

    private void SpawnIslands(GameObject Biome, float RandomBiomeSize)
    {
        
        for (int i = 0; i < Random.Range(MinIslandCount, MaxIslandCount); i++)
        {
            float biomeRadius = RandomBiomeSize * 0.5f;
            float worldRadius = 90f;

            Vector2 centerPosition = transform.localPosition;
            Vector2 worldPos;
            
            int attempts = 0;
            do
            {
                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                worldPos = (Vector2)Biome.transform.position + localOffset;

                float distance = Vector2.Distance(worldPos, centerPosition);
                if (distance > worldRadius)
                {
                    Vector2 dir = (worldPos - centerPosition).normalized;
                    worldPos = centerPosition + dir * worldRadius;
                }

                attempts++;
                if (attempts > 10) break;

            } while (Physics2D.OverlapCircle(worldPos, IslandCheckRadius, IslandLayer));

            if (Biome.CompareTag("RedBiome"))
            {
                GameObject island = Instantiate(
                    RedIslands[Random.Range(0, RedIslands.Length)],
                    worldPos,
                    Quaternion.identity
                );

                islands.Add(island);
            }
            if (Biome.CompareTag("OrangeBiome"))
            {
                GameObject island = Instantiate(
                    OrangeIslands[Random.Range(0, OrangeIslands.Length)],
                    worldPos,
                    Quaternion.identity
                );

                islands.Add(island);
            }
            if (Biome.CompareTag("GreenBiome"))
            {
                GameObject island = Instantiate(
                    GreenIslands[Random.Range(0, GreenIslands.Length)],
                    worldPos,
                    Quaternion.identity
                );

                islands.Add(island);
            }
            if (Biome.CompareTag("PurpleBiome"))
            {
                GameObject island = Instantiate(
                    PurpleIslands[Random.Range(0, PurpleIslands.Length)],
                    worldPos,
                    Quaternion.identity
                );

                islands.Add(island);
            }
            if (Biome.CompareTag("BrownBiome"))
            {
                GameObject island = Instantiate(
                    BrownIslands[Random.Range(0, BrownIslands.Length)],
                    worldPos,
                    Quaternion.identity
                );

                islands.Add(island);
            }
        }
    }

    Quaternion GetIslandRotation(Vector2 SpawnPos)
    {
        //Get the direction vector from the spawn point to the center (0,0)
        Vector2 direction = (Vector2)Vector3.zero - SpawnPos;

        //Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Subtract 90 degrees if your sprite's "front" is the top (Y-axis) 
        float offset = -90f; 

        //Add your random variation (e.g., +/- 10 degrees)
        float variation = Random.Range(-10f, 10f);

        //Create the final rotation on the Z axis
        Quaternion finalRotation = Quaternion.Euler(0, 0, angle + variation);

        return finalRotation;
    }
}
