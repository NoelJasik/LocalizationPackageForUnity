using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextLocaliserEditWindow : EditorWindow
{

    public static void Open(string key)
    {
        var Window = (TextLocaliserEditWindow)ScriptableObject.CreateInstance(typeof(TextLocaliserEditWindow));
        Window.titleContent = new GUIContent("Localiser Window");
        Window.ShowUtility();
        Window.key = key;
    }

    public string key;
    public string value;

    public void OnGUI()
    {
        key = EditorGUILayout.TextField("Key :", key);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Value:", GUILayout.MaxWidth(50));

        EditorStyles.textArea.wordWrap = true;
        value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add"))
        {
            if (LocalisationSystem.getLocalisedValue(key) != string.Empty)
            {
                LocalisationSystem.Replace(key, value);
            }
            else
            {
                LocalisationSystem.Add(key, value);
            }
        }

        minSize = new Vector2(460, 250);
        maxSize = minSize;
    }
}


public class TextLocaliserSearchWindow : EditorWindow
{
    public static void Open()
    {
           var Window = (TextLocaliserSearchWindow)ScriptableObject.CreateInstance(typeof(TextLocaliserSearchWindow));
        Window.titleContent = new GUIContent("Localisation Search");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
        Window.ShowAsDropDown(r, new Vector2(500, 300));
    }

    public string value;
    public Vector2 scroll;
    public Dictionary<string, string> dictionary;

    private void onEnable()
    {
        dictionary = LocalisationSystem.GetDictionaryForEditor();
    }
    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
        value = EditorGUILayout.TextField(value);
        EditorGUILayout.EndHorizontal();

        GetSearchResults();
    }

    private void GetSearchResults()
    {
        if (value == null) { value = "id"; }
        EditorGUILayout.BeginVertical();
        scroll = EditorGUILayout.BeginScrollView(scroll);
         dictionary = LocalisationSystem.GetDictionaryForEditor();
        foreach (KeyValuePair<string, string> element in dictionary)
        {
            if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
            {
                EditorGUILayout.BeginHorizontal("box");
                Texture closeIcon = (Texture)Resources.Load("close");

                GUIContent content = new GUIContent(closeIcon);

                if (GUILayout.Button(content, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    if (EditorUtility.DisplayDialog("Remove Key " + element.Key + "?", "This will make the key go fuck you", "Less Go!"))
                    {
                        LocalisationSystem.Remove(element.Key);
                        AssetDatabase.Refresh();
                        LocalisationSystem.Init();
                        dictionary = LocalisationSystem.GetDictionaryForEditor();
                    }
                }

                EditorGUILayout.TextField(element.Key);
                EditorGUILayout.LabelField(element.Value);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

}