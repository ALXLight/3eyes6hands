using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{

    public string interactionContainerTag = "ObjectInteractor";
    protected bool used = false;
    private bool longUsed = false;
    protected ObjectTexts objectTexts = new ObjectTexts();

    protected virtual void Awake()
    {
      //  used = false;
      //  longUsed = false;
        objectTexts.tryLoadData(name);

       // Debug.Log("piu" + name);
        //if()
    }

	void OnTriggerEnter2D(Collider2D otherObject) 
	{
	    if (!Global.isPaused())
	    {
	        if (otherObject.tag == interactionContainerTag)
	        {
	            onEnterCollide();
	        }
	    }
	}

	void OnTriggerExit2D(Collider2D otherObject ) 
	{
	    if (!Global.isPaused())
	    {
	        if (otherObject.tag == interactionContainerTag)
	        {
	            onExitCollide();
	        }
	    }
	}

	void  OnTriggerStay2D(Collider2D otherObject)
	{
	    if (!Global.isPaused())
	    {           
            if (otherObject.tag == interactionContainerTag)
            {              
	            onCollide();

                if (Input.GetButtonUp("Use") && longUsed == false)
                    onUse();
                else if (Input.GetAxis("Use") == 1)
                {
                    longUsed = true;
                    onLongUse();
                }
                else
                {
                    longUsed = false;
                }
            }
	    }
	}



    protected virtual  void onEnterCollide()
    {      
    }

    protected virtual void onCollide()
    {}

    protected virtual void onUse()
    {}

    protected virtual void onLongUse()
    { }

    protected virtual void onExitCollide()
    {}

}
