using UnityEngine;
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

    public void setText(string text)
    {
        messageTextField.text = text;
    }

    public IEnumerator setTextForTime(string text, float secsShown)
    {
        messageTextField.text = text;
        yield return new WaitForSeconds(secsShown);
        //print(Time.time);
        clear();
    }

    public void setTextPaused(string text)
    {
        messageTextField.text = text;
        
        Debug.Log("pause here");

        clear();
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
