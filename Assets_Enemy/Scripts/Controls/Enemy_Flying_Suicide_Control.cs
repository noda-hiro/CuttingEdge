using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying_Suicide_Control : MonoBehaviour
{
    // �G�l�~�[�E����_�����̗̑�
    [SerializeField]
    int HP = 20;
    // �G�l�~�[�E����_�����̍U����
    [SerializeField]
    int ATK = 30;

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
