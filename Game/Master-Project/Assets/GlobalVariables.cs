using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static GameObject selectedObjectForScaling = null;
    public static List<string> newCubesParents = new List<string>();
    public static List<GameObject> newCubesAsGO = new List<GameObject>();
    public static bool isGameStarted = false;
    public static string url = "http://commonhybrid.com/";

    public static Dictionary<string, string> userInfo = new Dictionary<string, string>();
    public static int globalMovesLeft = 0;
}