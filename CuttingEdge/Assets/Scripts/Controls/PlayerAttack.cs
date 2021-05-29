using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum StateType
//{
//    PONYTAIL,
//    TWINTAIL,
//}
public class PlayerAttack : MonoBehaviour
{
    public StateType State { get { return state; } set { state = value; } }
    private StateType state = StateType.PONYTAIL;
    public enum StateType
    {
        PONYTAIL,
        TWINTAIL,
    }
}
