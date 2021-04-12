using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public int speed;
    public Vector2 direction;
    public Vector2 startPos;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(Time.deltaTime * direction.x * speed / 1000, Time.deltaTime * direction.y * speed / 1000, 0);

        // If duck reaches edge of screen without being being clicked
        if ((startPos.x > 0 && this.gameObject.transform.position.x < (-1) * startPos.x
            || startPos.x < 0 && this.gameObject.transform.position.x > (-1) * startPos.x) && GameController.roundStarted)
        {
            gameController.statTracker.DuckMissed();
            Destroy(this);
        }

        if (direction.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    public void DestroyDuck()
    {
        Destroy(this.gameObject);
    }
    
}
