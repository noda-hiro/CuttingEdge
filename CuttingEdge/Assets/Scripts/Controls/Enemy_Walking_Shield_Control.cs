using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walking_Shield_Control : MonoBehaviour
{
    // エネミー・二足歩行_盾の体力
    [SerializeField]
    int HP = 50;
    // エネミー・二足歩行_盾の攻撃力
    [SerializeField]
    int ATK = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
