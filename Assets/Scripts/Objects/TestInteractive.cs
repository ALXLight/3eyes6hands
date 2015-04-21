using UnityEngine;
using System.Collections;


//using OTS = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, ObjectTextStrings>>;

public class TestInteractive : InteractiveObject
{

    /*override*/ void Awake()
    {
        base.Awake();
  
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
    //    Debug.Log("use");

        //Debug.Log(texts["level1"]["testdoc"].message());
       // Debug.Log(texts["level1"].message());


    }

    protected override void onLongUse()
    {
      //  Debug.Log("long use");
    }
}
