using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{

    public string interactionContainerTag;
    protected bool used;

    void Awake()
    {
        used = false;
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
	            onEndCollide();
	        }
	    }
	}

	void  OnTriggerStay2D(Collider2D otherObject)
	{
	    if (!Global.isPaused())
	    {
	        if (otherObject.name == interactionContainerTag)
	        {
	            onCollide();
	        }
	    }
	}

    protected void onEnterCollide()
    { }

    protected void onCollide()
    {}

    protected void onUse()
    {}

    protected void onEndCollide()
    {}

}
