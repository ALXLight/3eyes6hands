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


public struct DialogPhrase
{
    public string speaker;
    public string phrase;
}
public class DialogTexts
{
    private Dictionary<string, List<DialogPhrase>> Dialogues = new Dictionary<string, List<DialogPhrase>>();    

    private static Regex idRegExp = new Regex("(?s)([\\w-_]+):.+?\\{(.+?)\\}");
    private static Regex phraseRegExp = new Regex("(?s)([\\w-_]+):.+?\\[(.+?)\\]");

    public string message(string dialogId, int phrase)
    {   
        return Dialogues[dialogId][phrase].phrase;
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
                Dialogues.Add(id, parsePhrase(id, innerBlock) );
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
                string speaker = titleMatch.Groups[1].Value;
                string phraseText = titleMatch.Groups[2].Value.Trim().Replace("\t", String.Empty);

                currentIndex = titleMatch.Index + titleMatch.Length;

                DialogPhrase phrase;
                phrase.speaker = speaker;
                phrase.phrase = phraseText;

                phraseDialog.Add(phrase);
            }
            else
            {
                break;
            }
        }

        return phraseDialog;
    }
}



