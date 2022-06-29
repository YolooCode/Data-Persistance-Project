using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class Persistance : MonoBehaviour
{
    public static string PlayerName;
    public static string HaighscorePlayersName;
    public TMP_InputField enterName;
   
    public GameObject haighScoreText;
    public GameObject panelEmpty;

    public static int CurrentHighScore = 0;
    
    [System.Serializable]
    class GamePrefs
    {
        public int CurrentHighScores = 0;
        public string HaighscorePlayersNames;
    }

    public static void SaveGame()
    {
        
        GamePrefs gameprefs = new GamePrefs();
        
        gameprefs.HaighscorePlayersNames = HaighscorePlayersName;
        gameprefs.CurrentHighScores = CurrentHighScore;

        string json = JsonUtility.ToJson(gameprefs);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("irase");
    }
    public void loadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GamePrefs data = JsonUtility.FromJson<GamePrefs>(json);

           CurrentHighScore = data.CurrentHighScores;
           HaighscorePlayersName = data.HaighscorePlayersNames;
            Debug.Log("ikele");
        }
    }


    void Start()
    {

        loadGame();

        panelEmpty.SetActive(false);

        if(CurrentHighScore < MainManager.m_Points)
        {
            CurrentHighScore = MainManager.m_Points;

            if(HaighscorePlayersName != PlayerName)
            {
                HaighscorePlayersName = PlayerName;
            }
        }
        if(HaighscorePlayersName == null)
        {
            haighScoreText.SetActive(false);
        }
        else
        {
            haighScoreText.GetComponent<TMP_Text>().text = $"{HaighscorePlayersName} Has highscore of {CurrentHighScore} points!";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        if(enterName.text != "")
        {
            PlayerName = enterName.text;
            SceneManager.LoadScene("main");
        }
        else
        {
            panelEmpty.SetActive(true);
        }
        
    }

}
