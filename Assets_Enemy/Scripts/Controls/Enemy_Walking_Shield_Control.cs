using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walking_Shield_Control : MonoBehaviour
{
    // �G�l�~�[�E�񑫕��s_���̗̑�
    [SerializeField]
    int HP = 50;
    // �G�l�~�[�E�񑫕��s_���̍U����
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
