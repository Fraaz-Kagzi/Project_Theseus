using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Highscore.Scoreboards
{
    public class HighScores : MonoBehaviour
    {


        // Update is called once per frame
        void Update()
        {
            if (GameVariables.gameFinish == true)
            {
                ScoreboardEntryData myScore = new ScoreboardEntryData {entryName=GetName.theName,entryScore=GameVariables.Level };
                Scoreboard scoreboard = new Scoreboard();
                scoreboard.AddEntry(myScore);
                GameVariables.gameFinish = false;
            }
        }
    }
}

