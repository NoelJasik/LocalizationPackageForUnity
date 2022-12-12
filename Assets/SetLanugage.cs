using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanugage : MonoBehaviour
{
    //0 english 1 polish
    [SerializeField]
    int numberOfLanguage;
    public void Select()
    {
        LocalisationSystem.SelectLanguage(numberOfLanguage);
    }
}
