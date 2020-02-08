using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public int fruit_PickUp;
    public GameObject[] fruitTypes;
    private float min_X = -7f, max_X = 7f, min_Y = -3f, max_Y = 3f;
    private float zPos = -0.25f;
    public Text score_Text;
    private int score_Count;
    // string jsonString;
    public Text food_color;

    [SerializeField]
    private GameObject touchButton;
    public GameObject gameOverMenu;
    public Text HighScore_Text;
    private int highScore;
    // private System.Object jsonBallsData;
    private static bool repeatedBlue = true;
    private int blueStreak = 1;
    private static bool repeatedRed = true;
    private int redStreak = 1;
    public Text streakText;

    void Awake () {
        gameOverMenu.SetActive (false);
        MakeInstance ();
        touchButton.SetActive (true);
        // Time.timeScale = 0;

    }

    // Start is called before the first frame update
    void Start () {
        // food_color.text = foodData[0].color;
        GameObject[] fruitTypes = new GameObject[2];
        // jsonBallsData = JsonLoader.instance.ReturnData ();
        score_Count = 0;
        highScore = PlayerPrefs.GetInt ("highscore");
        Debug.Log ("highscore in start " + highScore);
        score_Text.text = "Score: " + score_Count;
        HighScore_Text.text = "High Sore: " + highScore;
        SpawnPickups ();
        streakText.text = redStreak > 0 ? "Streak: " + redStreak : (blueStreak > 0 ? "Streak: " + blueStreak : "");
        // Invoke ("StartSpawning");

    }

    void MakeInstance () {
        if (instance == null)
            instance = this;
        Debug.Log ("instance created");
    }

    public void SetGameOverMenu () {
        gameOverMenu.SetActive (true);
    }

    void StartSpawning () {
        // StartCoroutine (SpawnPickups ());
    }

    public void CancelSpawning () {
        CancelInvoke ("StartSpawning");
    }

    public void SpawnPickups () {
        
        if (Random.Range (0f, 1f) > 0.5f)
            fruit_PickUp = 1;
        else
            fruit_PickUp = 0;

        Instantiate (fruitTypes[fruit_PickUp], new Vector3 (Random.Range (min_X, max_X), Random.Range (min_Y, max_Y)), Quaternion.identity);
    }

    public void IcreaseBlueScore () {
        // repeatedRed = false;
        redStreak = 1;
        // if (repeatedBlue) {
        score_Count += FruitStats.BLUESCORE * blueStreak;
        blueStreak++;
        // repeatedBlue = true;
        // }
        SetScore ();

    }

    public void IcreaseRedScore () {
        // repeatedBlue = false;

        blueStreak = 1;
        // if (repeatedRed) {
        score_Count += FruitStats.REDSCORE * redStreak;
        redStreak++;
        // repeatedRed = true;
        // }
        SetScore ();

    }

    public void SetScore () {
        streakText.text = redStreak > 0 ? "Streak: " + redStreak : (blueStreak > 0 ? "Streak: " + blueStreak : "");
        if (score_Count > highScore) {

            PlayerPrefs.SetInt ("highscore", score_Count);
            // HighScore_Text.text = "High Sore: " + score_Count;
        } else {
            highScore = PlayerPrefs.GetInt ("highscore");
        }

        score_Text.text = "Score: " + score_Count;

    }

    public void StartGame () {
        Time.timeScale = 1;
        touchButton.SetActive (false);
    }

    public void LoadMAinMenu () {
        SceneManager.LoadScene ("MainMenu");

    }


    public void RsestartGame () {
        SceneManager.LoadScene ("GamePlay");

    }

    // Update is called once per frame
    void Update () {

    }

  


}