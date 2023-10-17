using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private Camera camera;
    
    float speed;

    // Start is called before the first frame update
    void Start() 
    {
        camera = FindObjectOfType<Camera>();
        speed = Random.Range(-0.1f, -0.8f);
    }

    // Update is called once per frame
    void Update()
    { 
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
        bool onScreen = screenPos.x > - Screen.width * 0.3f;

        if (!onScreen)
        {
            speed = Random.Range(-0.1f, -0.8f);
            Vector3 newPosition = new Vector3(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0,0)).x * 1.3f, transform.position.y, 0);
            transform.position = newPosition;
        } 
    }

    void Process()
    {
        
    }
}
