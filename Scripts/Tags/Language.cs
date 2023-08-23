using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Language 
{
        
    public enum LANGUAGE
    {
        TR = 0,
        ENG = 1
    }

    static LANGUAGE currentPreference = LANGUAGE.TR;

    
    public static void ChangeLanguage(LANGUAGE _language)
    {
        currentPreference = _language;
        PlayerPrefs.GetInt("languageValue", (int)currentPreference);
        
    }

    public static int GetPreference()
    {
        return (int)currentPreference;
    }

    public static int GetLanguageCount()
    {
        return LANGUAGE.GetNames(typeof(LANGUAGE)).Length;
    }

}
