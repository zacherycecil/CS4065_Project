using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCursor: MonoBehaviour
{
    public float speed;
    float slowSpeed;
    public float innerRadius;
    public float outerRadius;
    public GameController gameController;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float distanceArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.duckList.Count != 0)
        {
            slowSpeed = 1;
            foreach (Duck duck in gameController.duckList)
            {
                if (Vector3.Distance(duck.transform.position, transform.position) < innerRadius)
                {
                    slowSpeed = 0.4f;
                }
                else if (Vector3.Distance(duck.transform.position, transform.position) < outerRadius && slowSpeed > 0.4f)
                {
                    slowSpeed = 0.6f;
                }
                else if (slowSpeed > 0.6f)
                {
                    slowSpeed = 1;
                }
            }
        }

        bool duckClicked = false;
        if (Input.GetMouseButtonDown(0) && GameController.roundStarted)
        {
            foreach (Duck duck in gameController.duckList)
            {
                if (Vector3.Distance(duck.transform.position, transform.position) < innerRadius)
                { 
                    gameController.statTracker.Score();
                    duck.DestroyDuck();
                    duckClicked = true;
                }
            }
            if (!duckClicked)
            {
                gameController.statTracker.ClickMissed();
            }
        }
        

        Vector3 v = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.position += v * speed * slowSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

    }
}
