using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject NPC;
    private float currentSpawn;
    private float timeSpawn;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timeSpawn = Random.Range(3, 7);

        if (currentSpawn > 0)
        {
            currentSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentSpawn = timeSpawn;
        }
    }

    public void SpawnObject()
    {
        int spawnPos = 3;
        spawnPos = Random.Range(0, 3);
        switch (spawnPos)
        {
            case 3:
                Instantiate(NPC, new Vector3(-0.11f, 4.35f, 0), transform.rotation);
                break;
            case 2:
                Instantiate(NPC, new Vector3(0.49f, 1.75f, 0), transform.rotation);
                break;
            case 1:
                Instantiate(NPC, new Vector3(-1.1f, 2.79f, 0), transform.rotation);
                break;
            default:
                Instantiate(NPC, new Vector3(1.72f, 3.98f, 0), transform.rotation);
                break;
        }
    }
}
