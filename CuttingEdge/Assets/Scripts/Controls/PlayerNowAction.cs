using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNowAction : MonoBehaviour
{
    public PlayerActionStateType State { get { return state; } set { state = value; } }
    private PlayerActionStateType state = PlayerActionStateType.IDEL;
    public enum PlayerActionStateType
    {
        IDEL,
        RUN,
        JUMP,
    }
}

