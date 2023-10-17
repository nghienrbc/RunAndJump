using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float multiplier;
    public GameObject camera;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, startPosition.x + multiplier * camera.transform.position.y, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
