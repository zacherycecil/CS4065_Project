using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public int ducksMissed;
    public int roundMissedDucks;
    public int missedClicks;
    public int roundMissedClicks;
    public int score;
    public int totalScore;
    public string output;
    public string difficulty;
    public string aimingScheme;
    public System.Random r;

    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
        ducksMissed = 0;
        roundMissedDucks = 0;
        missedClicks = 0;
        roundMissedClicks = 0;
        score = 0;
        totalScore = 0;
        output = "Difficulty,Cursor,Total Ducks Missed,Ducks Missed in Round,Missed Clicks,Missed Clicks in Round,Current Score,Total Score\n";
    }

    public void NewRound()
    {
        score = 0;
        roundMissedClicks = 0;
        roundMissedDucks = 0;
    }

    public void ClickMissed()
    {
        missedClicks++;
        roundMissedClicks++;
    }

    public void Score()
    {
        score++;
        totalScore++;
    }

    public void DuckMissed()
    {
        ducksMissed++;
        roundMissedDucks++;
    }

    public void SetRoundData(int difficulty, int aimingScheme)
    {
        if (difficulty == 1)
            this.difficulty = "Easy";
        else if (difficulty == 2)
            this.difficulty = "Medium";
        else if (difficulty == 3)
            this.difficulty = "Hard";

        if (aimingScheme == 0)
            this.aimingScheme = "Area";
        else if (aimingScheme == 1)
            this.aimingScheme = "Normal";
        else if (aimingScheme == 2)
            this.aimingScheme = "Sticky";
    }

    public void ToOutput()
    {
        output += difficulty + "," + aimingScheme + "," + ducksMissed + "," + roundMissedDucks + "," + missedClicks + "," + roundMissedClicks + "," + score + "," + totalScore + "\n";
    }

    public void ToFile()
    {
        File.WriteAllText("trial" + r.Next(0, 1000) + ".csv", output);
    }
}
