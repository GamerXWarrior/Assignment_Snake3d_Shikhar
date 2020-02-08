using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour {
    // Start is called before the first frame update

    public Text highScoreText;

    void Start () {
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt ("highscore");

    }

    public void ChangeLevel () {
        SceneManager.LoadScene ("GamePlay");
    }

 
    // Update is called once per frame
    void Update () {

    }
}