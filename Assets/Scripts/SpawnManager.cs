using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnpos = new Vector3(25, 0, 0);

    private float startDelay = 3f;
    private float repeatTime = 1.5f;

    //diff way to get Hold of scipt in other scipt
    private PlayerController playerController;

    //list of obstacals spawned
    public List<GameObject> obstacals = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating(nameof(createObstacle), startDelay, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createObstacle()
    {
        if (!playerController.gameOver)
        {
            repeatTime = Random.Range(1f, 2.5f);
            int randIndex = Random.Range(0, obstaclePrefab.Length);
            GameObject x = Instantiate(obstaclePrefab[randIndex], spawnpos, Quaternion.identity);

            //adding items to list
            obstacals.Add(x);
        }
    }

    public void removeAllObstacals()
    {
        int length = obstacals.Count;
        // destroying all the obstales in the scene....
        for(int i=0; i < length; i++)
        {
            Destroy(obstacals[i]);
        }
        obstacals.Clear();
    }
}
