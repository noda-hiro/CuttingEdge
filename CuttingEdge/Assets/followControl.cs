using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
public class followControl : MonoBehaviour
{
    [SerializeField]
  private  GameObject player;

    void Update()
    {
        this.transform.position = player.transform.position;
        CDebug.Log(this.transform.position);
    }
}
