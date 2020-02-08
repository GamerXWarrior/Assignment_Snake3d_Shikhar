using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [HideInInspector]
    public SnakeDirection direction;
    [HideInInspector]
    public float movement_Length = 0.5f;
    [HideInInspector]
    public float movement_Frequency = 0.1f;
    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> delta_Position;
    private List<Rigidbody> nodes;
    private Rigidbody main_Body;
    private Rigidbody head_Body;
    private Transform tr;

    private bool move;
    private float counter;
    private bool create_Node_At_Tail;

    void Awake () {
        Time.timeScale = 0;

        tr = transform;
        main_Body = GetComponent<Rigidbody> ();
        InitSnakeNodes ();
        InitPlayer ();

        delta_Position = new List<Vector3> () {
            new Vector3 (-movement_Length, 0f), // -x direction
            new Vector3 (0f, movement_Length), // y direction
            new Vector3 (movement_Length, 0f), // x direction
            new Vector3 (0f, -movement_Length) // -y direction
        };

    }

    void Update () {
        CheckMovementFrequency ();
    }

    void FixedUpdate () {
        if (move) {
            move = false;
            Move ();
        }
    }

    public void InitSnakeNodes () {
        nodes = new List<Rigidbody> ();
        nodes.Add (tr.GetChild (0).GetComponent<Rigidbody> ());
        nodes.Add (tr.GetChild (1).GetComponent<Rigidbody> ());
        nodes.Add (tr.GetChild (2).GetComponent<Rigidbody> ());
        head_Body = nodes[0];

    }

    void SetRandomDirection () {
        // int dirRandom = Random.Range (0, (int) SnakeDirection.COUNT);
        int dirStart = 2; // Start from right direction
        direction = (SnakeDirection) dirStart;

    }

    void InitPlayer () {
        SetRandomDirection ();

        switch (direction) {
            case SnakeDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3 (Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3 (Metrics.NODE * 2, 0f, 0f);
                break;
            case SnakeDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3 (Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3 (Metrics.NODE * 2, 0f, 0f);

                break;
            case SnakeDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3 (0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3 (0f, Metrics.NODE * 2, 0f);
                break;
            case SnakeDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3 (0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3 (0f, Metrics.NODE * 2, 0f);
                break;

        }
    }

    void Move () {
        Vector3 dPosition = delta_Position[(int) direction];
        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;

        main_Body.position += dPosition * 2;
        head_Body.position += dPosition * 2;
        for (int i = 1; i < nodes.Count; i++) {
            prevPosition = nodes[i].position;
            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }

        //check if we need to creat a node coz of fruit
        if (create_Node_At_Tail) {
            create_Node_At_Tail = false;
            GameObject newNode = Instantiate (tailPrefab, nodes[nodes.Count - 1].position, Quaternion.identity);
            newNode.transform.SetParent (transform, true);
            nodes.Add (newNode.GetComponent<Rigidbody> ());
        }
    }

    void CheckMovementFrequency () {
        counter += Time.deltaTime;

        if (counter >= movement_Frequency) {
            counter = 0;
            move = true;
        }
    }

    public void SetInputDirection (SnakeDirection dir) {
        if (dir == SnakeDirection.UP && direction == SnakeDirection.DOWN || dir == SnakeDirection.DOWN && direction == SnakeDirection.UP || dir == SnakeDirection.LEFT && direction == SnakeDirection.RIGHT || dir == SnakeDirection.RIGHT && direction == SnakeDirection.LEFT)
            return;
        else {
            direction = dir;
            ForceMove ();
        }
    }

    void ForceMove () {
        counter = 0;
        move = false;
        Move ();
    }

    void OnTriggerEnter (Collider target) {

        if (target.tag == Tags.BLUEFRUIT) {
            // print ("fruit touched");
            // GameController.instance.IcreaseScore ();
            GameController.instance.SpawnPickups ();
            target.gameObject.SetActive (false);
            create_Node_At_Tail = true;
            GameController.instance.IcreaseBlueScore ();
        }
        if (target.tag == Tags.REDFRUIT) {
            // print ("fruit touched");
            // GameController.instance.IcreaseScore ();
            GameController.instance.SpawnPickups ();
            target.gameObject.SetActive (false);
            create_Node_At_Tail = true;
            GameController.instance.IcreaseRedScore ();
        
        }

        if (target.tag == Tags.WALL || target.tag == Tags.TAIL) {
            print (target.tag + " touched");
            Time.timeScale = 0;
            GameController.instance.SetGameOverMenu ();
        }
    }

}