using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    [SerializeField] private string playerName;
    [SerializeField] private string highscoreName;
    [SerializeField] private int highscore;
    public string PlayerName { get => playerName; set => playerName = value; }
    public string HighscoreName { get => highscoreName; set => highscoreName = value; }
    public int Highscore { get => highscore; set => highscore = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int highscore;
        public string name;
    }

    public void SaveHighscore()
    {
        highscoreName = playerName;

        SaveData data = new SaveData();
        data.name = playerName;
        data.highscore = highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            highscore = data.highscore;
            highscoreName = data.name;
        } 
        else
        {
            highscore = 0;
            highscoreName = playerName;
        }
    }
}
