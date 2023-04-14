using System.IO;
using UnityEngine;

namespace Highscore.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform highscoresHolderTransform;
        [SerializeField] private GameObject scoreboardEntryObject;

        [Header("Test")]
        [SerializeField] ScoreboardEntryData testEntrydata = new ScoreboardEntryData();

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";

        private void Start()
        {
            ScoreboardSaveData savedScores =  GetSavedScores();

            UpdateUI(savedScores);
            SaveScores(savedScores);
        }
        /*[ContextMenu("Add Test Entry")]
        public void AddTestEntry()
        {
            AddEntry(testEntrydata);
        }*/
        void Update()
        {
            if (GameVariables.gameFinish == true)
            {
                ScoreboardEntryData myScore = new ScoreboardEntryData { entryName = GetName.theName, entryScore = GameVariables.Level };// creates a new scoreboard with name being the user input and score being level lasted
                Debug.Log(myScore.entryName);
                Debug.Log(myScore.entryScore);               
                AddEntry(myScore);
                Debug.Log("scores updated");
                GameVariables.gameFinish = false;
                
            }
        }
        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScores();
            bool scoreAdded = false;
            for(int i =0; i< savedScores.highscores.Count;i++)
            {
                if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if(!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
            {
                savedScores.highscores.Add(scoreboardEntryData);
            }
            if(savedScores.highscores.Count > maxScoreboardEntries)
            {
                savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
            }
            SaveScores(savedScores);
            Debug.Log("score saved to file");
            UpdateUI(savedScores);
            Debug.Log("UI updated");

        }

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach(Transform child in highscoresHolderTransform)
            {
                Destroy(child.gameObject);// deletes scoreboard ui 
            }
            foreach(ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<ScoreboardEntryUI>().Initialise(highscore); // rewrites ui scoreboard
            }
        }
        private ScoreboardSaveData GetSavedScores()
        {
            if(!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();
                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
                
            }
        }
    }
}

