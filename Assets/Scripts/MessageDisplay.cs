using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour 
{
    public string TextObjectName;    
    private Text messageTextField;
    private bool pausedByText;

	void Awake ()
	{

	    if (TextObjectName != "")
	    {
            messageTextField = GameObject.Find(TextObjectName).GetComponent<Text>();
	    }
	    pausedByText = false;
	}

    public static MessageDisplay getMessageDisplay(string name)
    {
        try
        {
            return GameObject.Find(name).GetComponent<Text>().GetComponent<MessageDisplay>();
        }
        catch (Exception)
        {
            Debug.LogError("Incorrect getMessageDisplay name: " + name);
            return null;
        }
    }

    public void setText(string text)
    {
        messageTextField.text = text;
    }

    public void setTextForTime(string text, float secsShown)
    {
        messageTextField.text = text;               
        Invoke("clear", secsShown);
    }

    public void setTextPaused(string text)
    {
        messageTextField.text = text;
        Global.waitForAnyKey(clear);        
    }

    public void clear()
    {
        messageTextField.text = "";
    }
	

	void Update () 
    {
        if (pausedByText)
            if (Input.anyKey)
            {
                Debug.Log("unpause");
                pausedByText = false;
            }
	}
}
