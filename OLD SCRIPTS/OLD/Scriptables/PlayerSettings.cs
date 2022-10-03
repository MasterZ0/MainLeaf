using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/Player Settings", order = 0)]
public class PlayerSettings : ScriptableObject {

    [Header("Player Inputs")]
    public bool ignoreTime;
    public bool showMouse;
    public float walkMouseSensitivity = 50;
    public float aimMouseSensitivity = 2;
}
