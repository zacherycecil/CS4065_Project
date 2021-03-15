using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool roundStarted;
    public Text menuText;

    // Start is called before the first frame update
    void Start()
    {
        roundStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            roundStarted = true;
        }

        if (!roundStarted)
        {
            menuText.text = "Iain Campbell and Zachery Lewis Present: Duck Hunt\n\nPress space to start";
        }
        else
        {
            menuText.text = "";
        }
    }
}
