using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walking_Control : MonoBehaviour
{
	// �񑫕��s�̗̑�
	[SerializeField]
	int HP = 40;
	// �񑫕��s�̍U����
	[SerializeField]
	int ATK = 10;
	[SerializeField]
	// �񑫕��s�̈ړ����x
	float SPEED = 0.0f;
	
	Vector3 targetPosition;

	// �Q�[���I�u�W�F�N�g������ϐ�
	GameObject EW;
	// Enemy_Create�̃X�N���v�g������ϐ�
	Enemy_Create EW_Script;

	// Start is called before the first frame update
	void Start()
	{
		// �Q�[���I�u�W�F�N�g�𖼑O����擾���ĕϐ��ɓ����
		EW = GameObject.Find("Test");
		// EW�̒��ɂ���Enemy_Create���擾���ĕϐ��ɓ����
		EW_Script = EW.GetComponent<Enemy_Create>();
	}

	// Update is called once per frame
	void Update()
	{
		// HP���Ȃ��Ȃ����玩�g��j��
		if (HP == 0)
		{
			Destroy(this.gameObject);
		}

		// �v���C���[�̃I�u�W�F�N�g�̈ʒu���擾
		targetPosition = GameObject.Find("Test_Player").transform.position;

		//�����ƒǂ�������
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
		
	}
}
