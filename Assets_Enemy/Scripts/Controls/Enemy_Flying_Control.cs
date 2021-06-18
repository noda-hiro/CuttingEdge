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
	// �G�l�~�[�E���Ă̈ړ����x
	float SPEED = 0.0f;

	// ������肽�Ă��ǂ���
	bool spawnFlag = true;

	// IEnumerator�Œ�~����b��
	float StopTime = 0.5f;

	// EM_forPlayer�Ńv���C���[�Ɍ������Ĉړ�����Ƃ��ɕK�v�Ȃ��
	private GameObject PlayerObject;

	// ����Ԃ��ǂ���
	private bool isVisible = false;
	// ���̃v���p�e�B�[
	public bool IsVisible
	{
		get { return isVisible; }
	}

	// Babu���I��������ǂ����̃t���O
	bool babu_endFlag = false;
	// Flying_Act��if�����s�����ǂ����̃t���O
	bool duringFlag = false;
	// Flying_Act -> �ړ��p�̃t���O
	bool moveFlag = false;
	// Flying_Act -> �ҋ@�p�̃t���O
	bool idleFlag = false;

	// �ړ��p�̃^�C�}�[
	float moveTimer = 4.0f;
	// �ҋ@�p�̃^�C�}�[
	float idleTimer = 3.0f;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		// �v���C���[�̃I�u�W�F�N�g�̍��W���擾
		PlayerObject = GameObject.Find("Test_Player");
	}

	// Update is called once per frame
	void Update()
	{
		// ���Ԃŗ����̃V�[�h�l��ς��Ă���
		Random.InitState(System.DateTime.Now.Millisecond);

		// HP�Ȃ��Ȃ����玀�ʁB��l�������ɂȁI
		Death();

		// �����Ⴈ���Ⴕ���̂Ŏ��s�I
		Babu();

		if (babu_endFlag == true)
		{
			Flying_Act();
		}

		// 4�b�ԁA�v���C���[�Ɍ������Ĉړ�������
		if (moveFlag == true)
		{
			Debug.Log("MOVE");

			// �v���C���[�Ɍ������Ĉړ�
			EM_forPlayer();

			// moveTimer��1�b�����炷
			moveTimer -= 1.0f * Time.deltaTime;

			// moveTimer��0�b�ȉ��Ȃ�A���s
			if (moveTimer <= 0)
			{
				// moveFlag��false�ɂ���
				moveFlag = false;
				// duringFlag��false�ɂ���
				duringFlag = false;
			}

		}

		// 3�b�ԁA���̏�őҋ@
		if (idleFlag == true)
		{
			Debug.Log("IDLE");

			// idleTimer��1�b�����炷
			idleTimer -= 1.0f * Time.deltaTime;

			// idleTimer��0�b�ȉ��Ȃ�A���s
			if (idleTimer <= 0)
			{
				// idleFlag��false�ɂ���
				idleFlag = false;
				// duringFlag��false�ɂ���
				duringFlag = false;
			}
		}
	}

	/*----------------------------------------------------------------------------------------------------*/

	// HP���Ȃ��Ȃ����玩����j������֐�
	private void Death()
	{
		if (HP == 0)
		{
			Destroy(this.gameObject);
		}
	}

	// �G�l�~�[���A�v���C���[�Ɍ������Ĉړ�����֐�
	private void EM_forPlayer()
	{
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, PlayerObject.transform.position, move);
	}

	// �X�|�[�������Ă̂Ƃ��Ɏ��s����֐�
	private void Babu()
	{
		// ������肽�Ă̊Ԃ������s����
		if (spawnFlag == true)
		{
			// ��ʓ��ɓ����ĂȂ�������A�v���C���[�Ɍ������Ă����Ɠ���������
			if (isVisible == false)
			{
				// ��������
				EM_forPlayer();
			}
			// ��ʓ��ɓ�������A���΂炭��������Ɏ~�܂�܂�
			else if (isVisible == true)
			{
				// ��莞�Ԍ�Ɏ~�܂�
				StartCoroutine("Stop");
			}
		}
	}

	// ����ԂɂȂ�����Ă΂��֐�
	private void OnBecameVisible()
	{
		isVisible = true;
	}

	// �G�l�~�[�̍s���֐�
	private void Flying_Act()
	{
		// Random�N���X�̃C���X�^���X�𐶐�
		var act_rand_instance = new System.Random();
		// �K���ȗ����𐶐�����
		int enemy_act = act_rand_instance.Next();
		// �������ꂽ������4�Ŋ������]������߂�
		int flying_act = enemy_act % 4;

		// �������]��̒l�ɂ���čs����ς���
		if (flying_act == 0)                // �ړ�
		{
			// duringFlag ��false�Ȃ���s
			if (duringFlag == false)
			{
				// moveFlag��duringFlag��true�ɂ���
				moveFlag = true;
				duringFlag = true;

				moveTimer = 4.0f;
			}

			// Debug.Log("move");
		}
		else if (flying_act == 1)       // �ҋ@
		{
			// duringFlag ��false�Ȃ���s
			if (duringFlag == false)
			{
				// idleFlag��duringFlag��true�ɂ���
				idleFlag = true;
				duringFlag = true;

				idleTimer = 3.0f;
			}

			// Debug.Log("stay");
		}
		else if (flying_act == 2)       // �ːi�U��
		{
			// Debug.Log("attack");
		}
		else if (flying_act == 3)       // �^��g�U��
		{
			// Debug.Log("wave_attack");
		}
	}

	// Babu��p�̃I�u�W�F�N�g��~�֐�
	IEnumerator Stop()
	{
		 EM_forPlayer();

		// 0.5�b�҂�
		yield return new WaitForSeconds(StopTime);

		// ������肽�ĂłȂ��Ȃ�
		spawnFlag = false;

		Debug.Log("��������s�\���");

		// 2�b�҂�
		yield return new WaitForSeconds(2);

		babu_endFlag = true;

		Debug.Log("babu���I���܂���");
		// Debug.Log(spawnFlag);
	}
}