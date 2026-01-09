using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGeneration : MonoBehaviour
{
    public GameObject OpenOcean;

    [Header("Biomes")]
    public GameObject[] Biomes;
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
            GameObject Biome = Instantiate(Biomes[Random.Range(0, Biomes.Length)], SpawnPos, transform.rotation);

            //Change the Biome size between the min and max values
            float RandomBiomeSize = Random.Range(BiomeSizeMin, BiomeSizeMax);
            Vector3 BiomeSize = new Vector3(RandomBiomeSize, RandomBiomeSize, 1);
            Biome.transform.localScale = BiomeSize;

            BaseAngle += Mathf.PI * 2 / 3;

            //Spawn in the main islands into the designated biomes
            if (Biome.CompareTag("RedBiome"))
            {
                float biomeRadius = RandomBiomeSize * 0.5f;

                Vector2 localOffset = Random.insideUnitCircle * biomeRadius;
                Vector2 worldPos = (Vector2)Biome.transform.position + localOffset;

                GameObject island = Instantiate(MainRedIsland, worldPos, Quaternion.identity);
                island.transform.parent = Biome.transform;
            }
        }
    }

}
