using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class ObjectTexts
{
    private Dictionary<string, string> Messages = new Dictionary<string, string>();   

    private static Regex idRegExp = new Regex("(?s)([\\w-_]+):.+?\\{(.+?)\\}");

    public string message(string key)
    {
       return Messages[key];
    }

  

    public bool tryLoadData(string objectName)
    {
        string path = "Assets/Texts/Objects/" + Global.currentLevel() + "/" + objectName + ".txt";

        try
        {
            string fileContent = System.IO.File.ReadAllText(path);
            parse(fileContent);
        }
        catch (Exception)
        {                 
            return false;
        }
        return true;
    }  

    private void parse(string parseString)
    {
        int currentIndex = 0;

        while (true)
        {
            Match titleMatch = idRegExp.Match(parseString, currentIndex);

            if (titleMatch.Success)
            {
                string id = titleMatch.Groups[1].Value;
                string innerBlock = titleMatch.Groups[2].Value.Trim().Replace("\t", String.Empty);

                currentIndex = titleMatch.Index + titleMatch.Length;
                Messages.Add(id, innerBlock);
            }
            else
            {
                break;
            }
        }
    }
}


public class DialogPhrase
{
    public bool left = true;
    public string speaker = "";
    public string phrase = "";
}
public class DialogTexts
{
    private Dictionary<string, List<DialogPhrase>> Dialogs = new Dictionary<string, List<DialogPhrase>>();    

    private static Regex idRegExp = new Regex("(?s)([\\w-_]+):.+?\\{(.+?)\\}");
    private static Regex phraseRegExp = new Regex("(?s)([<>])([\\w-_@]+):.+?\\[(.+?)\\]");
    private int currentPhrase = 0;
    private string currentDialogId = "";

    // использовать для непоследовательного вывода сообщеий. Позиция не сохраняется
    public DialogPhrase message(string dialogId, int phrase)
    {
        if (!contains(dialogId))
            return null;

        return Dialogs[dialogId][phrase];
    }

    // последовательный вывод сообщений диалога. При остутствии или окончании диалога возвращает null
    public DialogPhrase message(string dialogId)
    {
        if (!contains(dialogId))
            return null;

        if (currentDialogId != dialogId)
        {
            currentDialogId = dialogId;
            currentPhrase = 0;
        }

       
        if (currentPhrase < Dialogs[dialogId].Count)
        {
            
            return Dialogs[dialogId][currentPhrase++];
        }
        else // окончание диалога
        {
            currentPhrase = 0;
            currentDialogId = "";
            return null;
        }        
    }

    private IEnumerable<DialogPhrase> nextMessage(string dialogId)
    {
        for (int i = 0; i < Dialogs[dialogId].Count(); ++i)
            yield return Dialogs[dialogId][i];
    }

    public bool contains(string dialogId)
    {
        if (Dialogs.ContainsKey(dialogId))
            return true;        
        return false;
    }



    public bool tryLoadData()
    {
        string path = "Assets/Texts/Dialogs/" + Global.currentLevel() + ".txt";
        if (!System.IO.File.Exists(path))
            return true;
        try
        {
            string fileContent = System.IO.File.ReadAllText(path);
            parseDialog(fileContent);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    private void parseDialog(string parseString)
    {
        int currentIndex = 0;

        while (true)
        {
            Match titleMatch = idRegExp.Match(parseString, currentIndex);

            if (titleMatch.Success)
            {
                string id = titleMatch.Groups[1].Value;
                string innerBlock = titleMatch.Groups[2].Value.Trim().Replace("\t", String.Empty);

                currentIndex = titleMatch.Index + titleMatch.Length;
                Dialogs.Add(id, parsePhrase(id, innerBlock) );
            }
            else
            {
                break;
            }
        }
    }

    private List<DialogPhrase> parsePhrase(string dialogId, string parseString)
    {
        int currentIndex = 0;

        List<DialogPhrase> phraseDialog = new List<DialogPhrase>();      

        while (true)
        {
            Match titleMatch = phraseRegExp.Match(parseString, currentIndex);

            if (titleMatch.Success)
            {
                string speaker = titleMatch.Groups[2].Value;
                string phraseText = titleMatch.Groups[3].Value.Trim().Replace("\t", String.Empty);

                currentIndex = titleMatch.Index + titleMatch.Length;

                DialogPhrase parsedPhrase = new DialogPhrase();
                parsedPhrase.speaker = speaker;
                parsedPhrase.phrase = phraseText;
                if (titleMatch.Groups[1].Value == "<")
                    parsedPhrase.left = true;
                else if(titleMatch.Groups[1].Value == ">")
                    parsedPhrase.left = false;

                phraseDialog.Add(parsedPhrase);
            }
            else
            {
                break;
            }
        }

        return phraseDialog;
    }
}



