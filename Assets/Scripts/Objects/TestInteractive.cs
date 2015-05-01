using UnityEngine;
using System.Collections;


//using OTS = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, ObjectTextStrings>>;

public class TestInteractive : InteractiveObject
{

    private MessageDisplay msgDisplay;
   

    /*override*/ void Awake()
    {
        base.Awake();

       


        msgDisplay = MessageDisplay.getMessageDisplay("MessagePanel");
        //Debug.Log(msgDisplay);

    }

    protected override void onEnterCollide()
    {
        //msgDisplay.setTextPaused("piu");
       // msgDisplay.setText("piu");


       
    }
    protected override void onCollide()
    {
      //  Debug.Log("InsideObject");
    }

    protected override void onExitCollide()
    {
       // Debug.Log("not collide");
    }

    protected override void onUse()
    {
        //Debug.Log("use");

        msgDisplay.setDialog("0");
    }

    protected override void onLongUse()
    {
      //  Debug.Log("long use");

        msgDisplay.setTextPaused(objectTexts.message("longuse"));
    }
}
