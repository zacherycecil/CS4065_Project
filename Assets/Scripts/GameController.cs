using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool roundStarted;
    public static bool gameBegun;
    public bool gameDone;
    public Text menuText;
    public Text scoreText;

    public int aimingScheme;
    public List<Duck> duckList;
    public DuckGenerator duckGenerator;
    public int gameStart;
    public int totalTime;
    public static int score;
    public static int missedClicks;
    System.Random random;
    public int startingScheme;

    public int difficulty;
    public bool practiceDone;

    public int[] speeds;

    public int highScore;

    public List<GameObject> cursors;

    public StatTracker statTracker;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        roundStarted = false;
        gameBegun = false;
        duckList = new List<Duck>();
        random = new System.Random();
        score = 0;
        aimingScheme = 0;
        difficulty = 0;
        gameDone = false;
        practiceDone = false;

        foreach (GameObject cursor in cursors)
        {
            cursor.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (gameDone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Application.Quit();
            }
        }
        else if (!gameBegun)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                aimingScheme = 0;
                startingScheme = 0;
                gameBegun = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                aimingScheme = 1;
                startingScheme = 1;
                gameBegun = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                aimingScheme = 2;
                startingScheme = 2;
                gameBegun = true;
            }
        }
        else if (!roundStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                statTracker.NewRound();

                for (int i = 0; i < duckList.Count; i++)
                {
                    duckList[i].DestroyDuck();
                }
                
                if (difficulty == 0)
                {
                    cursors[aimingScheme].SetActive(true);
                    gameStart = (int)Time.timeSinceLevelLoad;
                    roundStarted = true;
                    score = 0;
                    difficulty = 1;
                    missedClicks = 0;
                }
                else
                {
                    cursors[aimingScheme].SetActive(true);
                    gameStart = (int)Time.timeSinceLevelLoad;
                    roundStarted = true;
                }
            }
        }

        for (int i = 0; i < duckList.Count; i++)
        {
            if (duckList[i] == null)
            {
                duckList.RemoveAt(i);
            }
        }

        // DUCK GENERATION
        if (roundStarted && duckList.Count == 0)
        {
            for (int i = 0; i < difficulty; i++)
            {
                float yPosRand = ((float)random.Next(-5, 7)) / 10;
                int randVal = (random.Next(0, 2) * 2) - 1;
                duckList.Add(duckGenerator.GenerateDuck(new Vector2((randVal) * (-1) * 1.8f, yPosRand), speeds[difficulty-1], new Vector2((randVal) * 0.5f, 0.1f), this));
            }
        }

        // TEXT MENU MANAGEMENT
        if (gameDone)
        {
            menuText.text = "Thank you for playing! Press E to finish.";
            scoreText.text = "";
        }
        else if (!gameBegun)
        {
            menuText.text = "Iain Campbell and Zachery Lewis Present: Duck Hunt\n\nHigh score: " + highScore + "\n\nPress '1' for group 1, '2' for group 2 and '3' for group 3.";
            scoreText.text = "";
        }
        else if (!roundStarted)
        {
            if (aimingScheme == 0)
                menuText.text = "AREA CURSOR - ";
            else if (aimingScheme == 1)
                menuText.text = "NORMAL CURSOR - ";
            else if (aimingScheme == 2)
                menuText.text = "STICKY CURSOR - ";

            if (!practiceDone)
                menuText.text += "PRACTICE\n";
            else
            {
                if (difficulty < 2)
                    menuText.text += "EASY\n";
                else if (difficulty == 2)
                    menuText.text += "MEDIUM\n";
                else if (difficulty == 3)
                    menuText.text += "HARD\n";
            }

            menuText.text += "Press spacebar to continue.";
            scoreText.text = "";
        }
        else
        {
            menuText.text = "";
            if (aimingScheme == 0)
                scoreText.text = "Area cursor\n";
            else if (aimingScheme == 1)
                scoreText.text = "Normal cursor\n";
            else if (aimingScheme == 2)
                scoreText.text = "Sticky cursor\n";
            scoreText.text += "Score: " + statTracker.score + "\nTime Left: " + (totalTime - (int)Time.timeSinceLevelLoad + gameStart) + "\nMissed clicks: " + statTracker.roundMissedClicks 
                + "\nMissed ducks: " + statTracker.roundMissedDucks;
        }

        if (roundStarted && totalTime - (int)Time.timeSinceLevelLoad + gameStart < 0)
        {
            statTracker.SetRoundData(difficulty, aimingScheme);
            statTracker.ToOutput();
            if(!practiceDone)
            {
                practiceDone = true;
                roundStarted = false;
            }
            else if (difficulty < 2)
            {
                difficulty = 2;
                roundStarted = false;
            }
            else if (difficulty < 3)
            {
                difficulty = 3;
                roundStarted = false;
            }
            else
            {
                practiceDone = false;
                difficulty = 0;
                cursors[0].SetActive(false);
                cursors[1].SetActive(false);
                cursors[2].SetActive(false);
                aimingScheme++;
                if (aimingScheme == 3)
                {
                    aimingScheme = 0;
                }

                if (startingScheme == aimingScheme)
                {
                    gameDone = true;
                    statTracker.ToFile();
                }

                roundStarted = false;
                if (score > highScore)
                    highScore = score;
            }
        }


        if (Input.GetMouseButtonDown(0)) missedClicks += 1;
    }
}
