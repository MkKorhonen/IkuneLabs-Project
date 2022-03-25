using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInfo : MonoBehaviour
{
    string info;

    public string GiveGhostInfo(int _num)
    {
        //Returns a string of text when this function is called with correct int value
        if (_num == 0)
            info = "White and basic ghost.\nMoves sideways";

        if (_num == 1)
            info = "Green ghost.\nFast circle type movement.";

        if (_num == 2)
            info = "Yellow ghost.\nVery agressive.\nAttacks the player.";

        if (_num == 3)
            info = "Blue ghost.\nVery calm ghost.\nMoves sideways.";

        return info;
    }
}
