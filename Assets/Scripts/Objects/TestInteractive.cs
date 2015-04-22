using UnityEngine;
using System.Collections;


//using OTS = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, ObjectTextStrings>>;

public class TestInteractive : InteractiveObject
{

    private MessageDisplay msgDisplay;
   

    /*override*/ void Awake()
    {
        base.Awake();

       


        msgDisplay = MessageDisplay.getMessageDisplay("MessageText");
        //Debug.Log(msgDisplay);


        /*
        otc.addMessage("msg1");
        otc.addMessage("msg2");
        otc.addMessage("msg3");
        otc.addMessage("msg4");
        otc.addMessage("msg5");*/


        // texts["level1"]["testdoc"].addMessage("msg2");
        //  texts["level1"]["testdoc"].addMessage("msg3");


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

        //Debug.Log(texts["level1"]["testdoc"].message());
       // Debug.Log(texts["level1"].message());

        msgDisplay.setTextPaused(objectTexts.message("use"));
    }

    protected override void onLongUse()
    {
      //  Debug.Log("long use");

        msgDisplay.setTextPaused(objectTexts.message("longuse"));
    }
}
