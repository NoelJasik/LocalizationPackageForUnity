using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliser : MonoBehaviour
{

    TextMeshProUGUI textField;
    public LocalisedString localisedString;
    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        textField.text = localisedString.value;
    }

    void Update()
    {
        if(textField.text != localisedString.value)
        {
  textField.text = localisedString.value;
        }
      
    }

}
