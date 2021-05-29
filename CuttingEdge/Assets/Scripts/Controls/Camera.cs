using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;   //Gameobject "Player"æ“¾

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //"PlayerPos‚Éplayer‚ÌÀ•W‚ğ‘ã“ü"
        Vector3 PlayerPos = player.transform.position;
        //©g‚ÌÀ•W(camera)‚ğPlayerPos‚Ìx²‚É‡‚í‚¹‚és
        transform.position = new Vector3(PlayerPos.x, 0, -1);
    }
}
