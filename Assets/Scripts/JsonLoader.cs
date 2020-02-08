using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonLoader : MonoBehaviour {
    string path;
    string jsonString;
    private System.Object DATA;
    public static JsonLoader instance;

    // Start is called before the first frame update
    void Start () {
        CreateInstance ();
        var path = Resources.Load<TextAsset> ("Urls");
        // Debug.Log ("path= " + path);
        jsonString = path.ToString ();
        DATA = dataContent.CreateFromJson (jsonString).data;
        // Debug.Log (ReturnData (foodData));
        Debug.Log (DATA);
        // CreateInstance (foodData);

        // for (int i = 0; i < 2; i++) {

        //     Debug.Log ("data= " + foodData[i].color);
        // }

        // webSiteUrl = new string[] { "", "", "", "", "", "", "", "" };
        // for (int i = 0; i < 8; i++)
        // {
        //     WWW urlResponse = new WWW(urlData[i].imageUrl);
        //     yield return urlResponse;
        //     buttonList[i].texture = urlResponse.texture;
        //     webSiteUrl[i] = urlData[i].webUrl;
        // }
    }

    void CreateInstance () {
        if (instance == null)
            instance = this;
    }

    public System.Object ReturnData () {
        return DATA;
    }

    [Serializable]
    public class dataContent {

        public List<Data> data;
        [Serializable]
        public class Data {
            public string color;
            public string points;
        }
        public static dataContent CreateFromJson (string jsonString) {
            return JsonUtility.FromJson<dataContent> (jsonString);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}