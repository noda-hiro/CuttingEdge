using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying_Control : MonoBehaviour
{
	// �G�l�~�[�E���Ă̗̑�
	[SerializeField]
	int HP = 35;
	// �G�l�~�[�E���Ă̍U����
	[SerializeField]
	int ATK = 8;
	[SerializeField]
	// �񑫕��s�̈ړ����x
	float SPEED = 0.0f;

	Vector3 targetPosition;

	private SpriteRenderer SR = null;

	// Start is called before the first frame update
	void Start()
	{
		SR = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(SR.isVisible)
		{
			Debug.Log("���ł�����ʂɓ�������");
		}

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
