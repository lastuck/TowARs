using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public static class SceneParams
{
    public static int level = 1;
    public static bool lastWave = false;
    public static bool noARScene = false;

    public static bool newDeck;
    public static string deckName = "deck_1.txt";

    public static bool buildingDeck = false;
    public static int selectedDeck = 0;
}
