using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;   //Gameobject "Player"�擾

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //"PlayerPos��player�̍��W����"
        Vector3 PlayerPos = player.transform.position;
        //���g�̍��W(camera)��PlayerPos��x���ɍ��킹��s
        transform.position = new Vector3(PlayerPos.x, 0, -1);
    }
}
