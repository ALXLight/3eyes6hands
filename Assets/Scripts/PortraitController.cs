using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PortraitController : MonoBehaviour
{
    private Text signText ;
    private RawImage image;

	// Use this for initialization
	void Awake ()
	{
	    signText = GameObject.Find(gameObject.name+"/Sign").GetComponent<Text>();
	    image = gameObject.GetComponent<RawImage>();
	}

    public void setSignedPicture(SignedPicture signedPicture)
    {
        if (signedPicture == null)
            return;

        image.texture = signedPicture.texture;
        signText.text = signedPicture.sign;
    }
}
