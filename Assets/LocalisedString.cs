using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LocalisedString
{
   
   public string key;

   public LocalisedString(string key)
   {
       this.key = key;
   }

   public string value
   {
       get
       {
           return LocalisationSystem.getLocalisedValue(key);
       }
   }

   public static implicit operator LocalisedString(string key)
   {
       return new LocalisedString(key);
   }
}
