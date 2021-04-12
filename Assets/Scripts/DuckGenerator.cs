using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGenerator : MonoBehaviour
{
    public GameObject duckPrototype;
    
    // Start is called before the first frame update
    void Start()
    {
        //GenerateDuck(new Vector2(0, 0), 1, new Vector2(0.5f, 0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Duck GenerateDuck(Vector2 startPos, int speed, Vector2 direction, GameController gameController)
    {
        GameObject newDuck = Instantiate(duckPrototype, new Vector3(startPos.x, startPos.y, 0), Quaternion.identity);
        Duck duck = newDuck.GetComponent<Duck>();
        duck.speed = speed;
        duck.direction = direction;
        duck.startPos = startPos;
        duck.gameController = gameController;
        return duck;
    }
}
