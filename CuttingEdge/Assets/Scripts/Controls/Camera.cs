using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;   //Gameobject "Player"取得

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //"PlayerPosにplayerの座標を代入"
        Vector3 PlayerPos = player.transform.position;
        //自身の座標(camera)をPlayerPosのx軸に合わせるs
        transform.position = new Vector3(PlayerPos.x, 0, -1);
    }
}
