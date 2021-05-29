using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    private string floorTag = "Floor";  //Floorタグ用
    private bool isFloor = false;       //床判定
    private bool isFloorEnter, isFloorStay, isFloorExit;    //(床に入った　床にいる　床から出た)フラグ

    //接地判定を返すメソッド
    //物理判定の更新毎に呼ぶ必要がある
    public bool IsFloor()
    {
        if (isFloorEnter || isFloorStay)
        {
            isFloor = true;
        }
        else if (isFloorExit)
        {
            isFloor = false;
        }

        isFloorEnter = false;
        isFloorStay = false;
        isFloorExit = false;
        return isFloor;
    }
    //PlayerがFloorに入った際
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == floorTag)
        {
            isFloorEnter = true;
            // Debug.Log("1");
        }
    }
    //PlayerがFloorに継続的にいる際
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == floorTag)
        {
            isFloorStay = true;
            //Debug.Log("2");
        }
    }
    //PlayerがFloorから出た際
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == floorTag)
        {
            isFloorExit = true;
            // Debug.Log("3");
        }
    }
}
