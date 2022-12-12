using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using System.Linq;

public class CSVLoader
{
    //File to refrence
    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char surrond = '"';
    private string[] fieldSeperator = { "\",\"" };

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localisation");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeID)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeperator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeperator, System.StringSplitOptions.None);

        for (int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(attributeID))
            {
                attributeIndex = i;
                break;
            }
        }

        Regex CSVparser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CSVparser.Split(line);

            for (int f = 0; f < fields.Length; f++)
            {
                fields[f] = fields[f].TrimStart(' ', surrond);
                fields[f] = fields[f].TrimEnd(surrond);
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key)) { continue; }
                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }
        return dictionary;
     }
#if UNITY_EDITOR
     public void Add(string key, string value)
     {
         string appended = string.Format("\n\"{0}\",\"{1}\",\"\"", key, value);
         File.AppendAllText("Assets/Resources/localisation.csv", appended);

         UnityEditor.AssetDatabase.Refresh();
     }
     public void Remove(string key)
     {
         string[] lines = csvFile.text.Split(lineSeperator);
         
         string[] keys = new string[lines.Length];

         for (int i = 0; i < lines.Length; i++)
         {
             string line = lines[i];

             keys[i] = line.Split(fieldSeperator, System.StringSplitOptions.None)[0];
         }

         int index = -1;
         for (int i = 0; i < keys.Length; i++)
         {
             if(keys[i].Contains(key))
             {
                 index = i;
                 break;
             }
         }

         if(index > -1)
         {
             string[] newLines;
             newLines = lines.Where(w => w != lines[index]).ToArray();

             string replaced = string.Join(lineSeperator.ToString(), newLines);
             File.WriteAllText("Assets/Resources/localisation.csv", replaced);
         }

      
     }
        public void Edit(string key, string value)
         {
             Remove(key);
             Add(key, value);
         }
         #endif
}
