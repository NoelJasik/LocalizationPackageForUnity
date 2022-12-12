using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{
    public enum Language
    {
        English,
        Polish
    }
    public static Language language = Language.English;
    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedPL;

    public static bool isInit;

    public static CSVLoader csvLoader;
    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();
        isInit = true;
    }

    public static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedPL = csvLoader.GetDictionaryValues("pl");


    }

    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if(!isInit) {Init();}
        return localisedEN;
    }
    public static void SelectLanguage(int WhichNumber)
    {
        Debug.Log("Changed");
        if(!isInit) {Init();}
    switch(WhichNumber)
    {
        case 0:
        language = Language.English;
        Debug.Log("EN");
        break;
        case 1:
        language = Language.Polish;
         Debug.Log("PL");
        break;
    }
    }
    public static string getLocalisedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Polish:
                localisedPL.TryGetValue(key, out value);
                break;
        }
        if(value == "")
        {
            value = "Not Found";
        }
        return value;
    }

    public static void Add(string key, string value)
    {
        if(value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if(csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

      public static void Remove(string key)
    {

        if(csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

      public static void Replace(string key, string value)
    {
        if(value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if(csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }
}
