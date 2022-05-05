using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 

    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    //public float timeBetweenSpawns;
    public float delayAndSpawnRate;
    public float timeUntilSpawnRateIncrease;
    
    private List<GameObject> sheepList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine(delayAndSpawnRate));
        //StartCoroutine(SpawnRoutine());

    }

    private IEnumerator SpawnRoutine(float firstDelay)
    //private IEnumerator SpawnRoutine()
    {
        float spawnRateCountdown=timeUntilSpawnRateIncrease;
        float spawnCountdown=firstDelay;
        /*while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }*/
        while( canSpawn )
         {
             yield return null ;
             spawnRateCountdown -= Time.deltaTime;
             spawnCountdown     -= Time.deltaTime;
 
             // Should a new object be spawned?
             if( spawnCountdown < 0 )
             {
                 spawnCountdown += delayAndSpawnRate;
                 SpawnSheep();
             }
 
             // Should the spawn rate increase?
             if( spawnRateCountdown < 0 && delayAndSpawnRate > 1 )
             {
                 spawnRateCountdown += timeUntilSpawnRateIncrease;
                 delayAndSpawnRate -= 0.1f;
             }
         }
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}