using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelData : MonoBehaviour
{
    private static Vector3 playerPosition;
    public static Vector3 PlayerPosition
    {
        get
        {
            return playerPosition;
        }
        set
        {
            playerPosition = value;
        }
    }
}
