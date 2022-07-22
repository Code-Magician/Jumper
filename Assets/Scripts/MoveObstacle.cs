using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private float speed = 20f;
    private float leftBound = -6f;

    //diff way to get Hold of scipt in other scipt
    private PlayerController playerController;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // finding PlayerController scipt from Player GameObject in Hierarchy...
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            if (playerController.runFast)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
                
        }


        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            if (playerController.runFast)
                playerController.score += 20;
            else
                playerController.score += 10;
            Destroy(gameObject);
            spawnManager.obstacals.Remove(gameObject);
        }
    }
}
