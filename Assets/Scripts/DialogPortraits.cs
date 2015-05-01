using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class SignedPicture
{
    public Texture2D texture;
    public string sign;
}

[Serializable]
public class DialogPortraitContainer : Dictionary<string, SignedPicture>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<string> keys = new List<string>();

    [SerializeField]
    private List<SignedPicture> values = new List<SignedPicture>();

     

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (KeyValuePair<string, SignedPicture> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);            
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
        {

            string key = keys[i];
            if (keys[i].ToString() != "" && this.ContainsKey(keys[i]))
            {        
                key = key + "Copy" ;
                Debug.LogError("DialogPortraits: Dublicate key \"" + keys[i] + "\" saved as \"" + key + "\"" );
            }

            this.Add(key, values[i]);
        }
    }
}



/*

[Serializable]
public class DialogPortraitContainer : SerializableDictionary<string, Sprite> { }

*/
public class DialogPortraits : MonoBehaviour
{

    public string tstString;
    public DialogPortraitContainer icons = new DialogPortraitContainer();
        //Dictionary<string, Sprite> PortraitContainers;




	// Use this for initialization
	void Awake () 
    {

        icons.Add("first", null);
        icons.Add("second", null);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void appendElement()
    {
        icons.Add("New Portrait", null);
    }

 

    public SignedPicture this[string id]
    {
        get
        {
            return icons[id];
        } 
        set
        {
            icons[id] = value;
        }
    }
}
