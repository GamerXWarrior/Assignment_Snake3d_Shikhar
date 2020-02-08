using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags {

    public static string WALL = "wall";
    public static string FRUIT = "fruit";
    public static string BLUEFRUIT = "bluefruit";
    public static string REDFRUIT = "redfruit";
    public static string TAIL = "tail";
}

public class FruitStats {

    public static string COLOR = "color";
    public static string POINTS = "points";
    public static int REDSCORE = 15;
    public static int BLUESCORE = 20;

}

public class Metrics {
    public static float NODE = 0.5f;
}

public enum SnakeDirection {
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
}