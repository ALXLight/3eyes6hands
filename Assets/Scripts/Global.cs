using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;




public class Global : MonoBehaviour
{
    public static DialogTexts Dialogs = new DialogTexts();
    public delegate void WaitForAnyKeyCallback();
    private static WaitForAnyKeyCallback waitForAnyKeyCallback = null;
    private static bool Pause = false;
    private static bool isWaitingForAnyKey = false;
    private static string CurrentLevel;
    private static float lastTimeScale ;


    private void Awake()
    {
     //   lastTimeScale = Time.timeScale;
        

        defineCurrentLevelName();

        // загружаем диалоги уровня
        if (!Dialogs.tryLoadData())
        {
            // ошибка, если файл есть, но не читается по какой-то причине
            Debug.Log("Can't load dialogs");
            System.Environment.Exit(1);
        }


    }

    public static string currentLevel() // возвращает название текущей сцены
    {
        if (CurrentLevel == null)
            defineCurrentLevelName();

        return CurrentLevel;
    }

    // for menu pause on and off
    public static void pause(bool setPause = true)
    {
        Pause = setPause;
        setGameTimeScale();
    }

    public static void waitForAnyKey()
    {
        isWaitingForAnyKey = true;
        setGameTimeScale();
    }
    public static void waitForAnyKey(WaitForAnyKeyCallback callOnAnyKey)
    {
        isWaitingForAnyKey = true;
        waitForAnyKeyCallback = callOnAnyKey;
        setGameTimeScale();
    }

  
    public static bool isPaused()  // остановлена ли игра по любой причине
    {       
        return Pause || isWaitingForAnyKey;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pause(!Pause);
        }
        else if (isWaitingForAnyKey)
        {
            if (Input.anyKeyDown) // если это была не кнопка паузы
            {
                isWaitingForAnyKey = false;                
                setGameTimeScale();
                if (waitForAnyKeyCallback != null)
                {
                    WaitForAnyKeyCallback tmpCallback = waitForAnyKeyCallback;
                    waitForAnyKeyCallback = null;
                    tmpCallback();                    
                }
            }
        }
    }


    private static void defineCurrentLevelName()
    {
        string fullLevelPath = EditorApplication.currentScene;
        int lastSlash = fullLevelPath.LastIndexOf('/') + 1;
        CurrentLevel = fullLevelPath.Substring(lastSlash, fullLevelPath.LastIndexOf('.') - lastSlash);
    }

    // ! вызывать после соответствующих изменений флагов
    private static void setGameTimeScale()
    {
        
        if (isPaused()) // если игра должна быть остановлена
        {
            if (Time.timeScale != 0f) // если она ещё не остановлена
            {
                lastTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
        }
        else // продолжить игру
        {
            Time.timeScale = lastTimeScale;
        }
    }

}


