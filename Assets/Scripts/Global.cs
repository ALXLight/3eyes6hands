using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;




public class Global : MonoBehaviour
{
    private static bool Pause = false;
    private static string CurrentLevel ;   
    public static DialogTexts Dialogs = new DialogTexts();

    void Awake()
    {
        defineCurrentLevelName();
        
        // загружаем диалоги уровня
        if(!Dialogs.tryLoadData())
        {
            // ошибка, если файл есть, но не читается по какой-то причине
            Debug.Log("Can't load dialogs");            
            System.Environment.Exit( 1 );
        }

        
    }

    public static string currentLevel()
    {
        if (CurrentLevel == null)
            defineCurrentLevelName();

        return CurrentLevel;       
    }
    // for menu pause on and off
    public static void pause(bool setPause = true)
    {
        Pause = setPause;
    }

    public static bool isPaused()
    {
        return Pause;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause = !Pause;
        }
    }


    private static void defineCurrentLevelName()
    {
        string fullLevelPath = EditorApplication.currentScene;
        int lastSlash = fullLevelPath.LastIndexOf('/') + 1;
        CurrentLevel = fullLevelPath.Substring(lastSlash, fullLevelPath.LastIndexOf('.') - lastSlash);
    }

}
