using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatXpos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        
        // box collider tells us the length of the background and we divide it by 2 to repeate it perfectly.
        repeatXpos = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= startPos.x - repeatXpos)
            transform.position = startPos;
    }
}
