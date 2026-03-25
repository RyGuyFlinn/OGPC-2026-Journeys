using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenSimple : MonoBehaviour
{
    public float worldRadius;
    public float edgeBuffer;
    public float centerBuffer;
    public int spawnGridLength;
    public float randomScatter;

    public GameObject OpenOcean;

    [Space]
    public GameObject[] islands;
    public GameObject spawnIsland;

    // Start is called before the first frame update
    void Start()
    {
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
                if (spawnPos.magnitude < worldRadius - edgeBuffer && spawnPos.magnitude > centerBuffer)
                {
                    Instantiate(islands[Random.Range(0, islands.Length)], spawnPos, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
