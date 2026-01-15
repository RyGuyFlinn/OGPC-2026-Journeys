using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGeneration : MonoBehaviour
{
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
    public GameObject[] RedIslands;
    public GameObject[] OrangeIslands;
    public GameObject[] GreenIslands;
    public GameObject[] PurpleIslands;
    public GameObject[] BrownIslands;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void SpawnBiomes()
    {
        float BaseAngle = Mathf.Deg2Rad * Random.Range(0, 120);
        for (int i = 0; i < 3; i++)
        {
            //Get Random Biome Position On circumfrance of the World
            biomeAngle = (BaseAngle + Mathf.Deg2Rad * Random.Range(-25, 25)) % (2 * Mathf.PI);
            SpawnPos = new Vector2(Mathf.Cos(biomeAngle), Mathf.Sin(biomeAngle)) * 50;

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
                float worldRadius = 45;

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

                GameObject island = Instantiate(MainRedIsland, worldPos, Quaternion.identity);

                island.transform.parent = Biome.transform;                
            }

            if (Biome.CompareTag("PurpleBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 45;

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

                GameObject island = Instantiate(MainPurpleIsland, worldPos, Quaternion.identity);

                island.transform.parent = Biome.transform;
            }

            if (Biome.CompareTag("GreenBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 45;

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

                GameObject island = Instantiate(MainGreenIsland, worldPos, Quaternion.identity);

                island.transform.parent = Biome.transform;
            }

            if (Biome.CompareTag("OrangeBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 45;

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

                GameObject island = Instantiate(MainOrangeIsland, worldPos, Quaternion.identity);

                island.transform.parent = Biome.transform;
            }

            if (Biome.CompareTag("BrownBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 45;

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

                GameObject island = Instantiate(MainBrownIsland, worldPos, Quaternion.identity);

                island.transform.parent = Biome.transform;
            }

            //Spawn the filler islands all around the world
            SpawnIslands(Biome, RandomBiomeSize);
        }
    }

    private void SpawnIslands(GameObject Biome, float RandomBiomeSize)
    {
        if (Biome.CompareTag("RedBiome"))
        {
            for (int i = 0; i < Random.Range(MinIslandCount, MaxIslandCount); i++)
            {
                float biomeRadius = RandomBiomeSize * 0.5f;
                float worldRadius = 45;

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

                GameObject island = Instantiate(RedIslands[Random.Range(0, RedIslands.Length)], worldPos, Quaternion.identity);

                island.transform.parent = Biome.transform;
            }
        }
    }

}
