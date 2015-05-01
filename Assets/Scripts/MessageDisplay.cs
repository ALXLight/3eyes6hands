using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageDisplay : MonoBehaviour 
{
    public GameObject TextObject;
    public GameObject LeftPortrait;
    public GameObject RightPortrait;

    private Text messageTextField;
    private bool pausedByText;
    private PortraitController LeftPortraitController;
    private PortraitController RightPortraitController;
    private static string currentDialogId;
    


	void Awake ()
	{
        if (TextObject)	
            messageTextField = TextObject.GetComponent<Text>();

	    if (LeftPortrait)
	    {
	        LeftPortraitController = LeftPortrait.GetComponent<PortraitController>();
            LeftPortrait.SetActive(false);
	    }


	    if (RightPortrait)
	    {
	        RightPortraitController = RightPortrait.GetComponent<PortraitController>();
            RightPortrait.SetActive(false);
	    }


	    gameObject.SetActive(false);
  
	    pausedByText = false;
        Debug.Log(name);
        //clear();
	}

    public static MessageDisplay getMessageDisplay(string name)
    {
        try
        {
            GameObject go = GameObject.Find(name);
            return go.GetComponent<MessageDisplay>();
        }
        catch (Exception)
        {
            Debug.LogError("Incorrect getMessageDisplay name: " + name);
            return null;
        }
    }

    public void setText(string text)
    {
        messageTextField.alignment = TextAnchor.UpperCenter;
        messageTextField.text = text;
        gameObject.SetActive(true);
    }

    public void setTextForTime(string text, float secsShown)
    {
        messageTextField.alignment = TextAnchor.UpperCenter;
        gameObject.SetActive(true);
        messageTextField.text = text;               
        Invoke("clear", secsShown);
    }

    public void setTextPaused(string text)
    {
        messageTextField.alignment = TextAnchor.UpperCenter;
        messageTextField.text = text;
        gameObject.SetActive(true);
        Global.waitForAnyKey(clear);      
    }

    public void setDialog(string dialogId)
    {
        gameObject.SetActive(true);
        currentDialogId = dialogId;
        nextDialogMessage();             
    }

    // функция для колбэка, вручную не вызывать
    public void nextDialogMessage()
    {

        DialogPhrase dp = Global.Dialogs.message(currentDialogId);
        if (dp != null)
        {

            if (dp.left)
                messageTextField.alignment = TextAnchor.UpperLeft;
            else
                messageTextField.alignment = TextAnchor.UpperRight;

            messageTextField.text = dp.phrase;
            setPortrait(dp);
            Global.waitForAnyKey(nextDialogMessage);
        }
        else
        {
            clear();
        }
    }
    
   

    public void clear()
    {
        messageTextField.text = "";
        gameObject.SetActive(false);
    }

   

    private void setPortrait(DialogPhrase dp)
    {
        if (dp.left)
        {
            if (LeftPortraitController)
            {
                RightPortrait.SetActive(false);
                LeftPortrait.SetActive(true);
                setPortrait(LeftPortraitController, dp);
            }

        }
        if (!dp.left)
        {
            if (RightPortraitController)
            {
                RightPortrait.SetActive(true);
                LeftPortrait.SetActive(false);
                setPortrait(RightPortraitController, dp);
            }
        }
    }
    private void setPortrait(PortraitController portraitController, DialogPhrase dp)
    {
        string speakerName = dp.speaker;
        string speakerPortrait = "default";
        int portraitSelectorPos = dp.speaker.IndexOf('@');

        if (portraitSelectorPos > 0)
        {
            speakerName = dp.speaker.Substring(0, portraitSelectorPos);
            speakerPortrait = dp.speaker.Substring(portraitSelectorPos + 1);
        }

        GameObject speaker = GameObject.Find(speakerName);
        if (!speaker)
        {
            Debug.LogWarning("MessageDisplay.setPortrait: Can't find speaker \"" + dp.speaker + "\"");
            return;
        }
        DialogPortraits portraitSet = speaker.GetComponent<DialogPortraits>();
        if (!portraitSet)
            return;

        portraitController.setSignedPicture(portraitSet[speakerPortrait]);

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
