using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

public class RiddleLoader : MonoBehaviour
{
    // private Dictionary<int, (string riddle, string answer)> riddles = new Dictionary<int, (string, string)>();
    private Dictionary<int, List<(string riddle, string answer)>> riddles = new Dictionary<int, List<(string, string)>>();


    void Start()
    {
        LoadRiddles();
    }

    
    private void LoadRiddles()
    {
        string path = Application.dataPath + "/Resources/riddles.csv";

        if (!File.Exists(path))
        {
            Debug.LogError("Riddle CSV file not found!");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        Debug.Log("Printing CSV contents:");

        foreach (string line in lines)
        {
            // Use regex to correctly split on commas **outside** of quotes
            string[] columns = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            if (columns.Length < 3)
            {
                Debug.LogError($"Malformed CSV row: {line}");
                continue;
            }

            // Remove surrounding quotes, if present
            string riddle = columns[0].Trim().Trim('"'); // First column = Riddle
            string answer = columns[1].Trim().Trim('"'); // Second column = Answer
            int level;

            // Ensure level is a valid number
            if (!int.TryParse(columns[2].Trim(), out level))
            {
                Debug.LogError($"Invalid level format in CSV: {columns[2]}");
                continue;
            }

            if (!riddles.ContainsKey(level))
                riddles[level] = new List<(string, string)>();
            
            riddles[level].Add((riddle, answer));


            // Debug.Log($"Added Riddle: {riddle}, Answer: {answer}, Level: {level}");
        }
    }




    public (string, string) GetRiddle(int level)
    {
        if (riddles.ContainsKey(level) && riddles[level].Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, riddles[level].Count);
            return riddles[level][randomIndex];  // Pick a random riddle from the list
        }
        return ("No riddle found", "");
    }

}
