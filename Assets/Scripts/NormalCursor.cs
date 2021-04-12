using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCursor : MonoBehaviour
{
    public float speed;
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
            if (Input.GetMouseButtonDown(0) && GameController.roundStarted)
            {
                bool duckClicked = false;
                foreach (Duck duck in gameController.duckList)
                {
                    if (Mathf.Abs(duck.transform.position.x - transform.position.x) < 1.3 * distanceArea && Mathf.Abs(duck.transform.position.y - transform.position.y) < distanceArea)
                    {
                        duck.DestroyDuck();
                        gameController.statTracker.Score();
                        duckClicked = true;
                    }
                }
                if (!duckClicked)
                {
                    gameController.statTracker.ClickMissed();
                }
            }
        }

        Vector3 v = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.position += v * speed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

    }
}
