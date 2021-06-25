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
	// �G�l�~�[�E���Ă̈ړ����x
	[SerializeField]
	float SPEED = 0.0f;
	// �G�l�~�[�E���Ă̓ːi���x
	[SerializeField]
	float RushSpeed = 0.0f;

	// ATK�̃v���p�e�B�[
	public int atk
	{
		get { return ATK; }
	}

	// GameObject�����Ă������
	private GameObject PlayerObject;

	// ���W�����Ă������
	private Vector3 BackPosition;
	private Vector3 PlayerPosition;

	// ����Ԃ��ǂ���
	private bool isVisible = false;
	// ���̃v���p�e�B�[
	public bool IsVisible
	{
		get { return isVisible; }
	}

	// ������肽�Ă��ǂ���
	bool spawnFlag = true;
	// Babu���I��������ǂ����̃t���O
	bool babu_endFlag = false;
	// Flying_Act_Flag��if�����s�����ǂ����̃t���O
	bool duringFlag = false;
	// �ːi�U���̂Ƃ���Y���W�̎擾���������ǂ����̃t���O
	bool coordinate_getFlag = false;
	// �^��g�U�����������������ǂ����̃t���O
	bool fireFlag = false;

	// Flying_Act -> �ړ��p�̃t���O
	bool moveFlag = false;
	// Flying_Act -> �ҋ@�p�̃t���O
	bool idleFlag = false;
	// Flying_Act -> �ːi�U���p�̃t���O
	bool rushFlag = false;
	// Flying_Act -> �^��g�U���p�̃t���O
	bool aerial_slashFlag = false;

	// �ړ��p�̃^�C�}�[
	float moveTimer = 4.0f;
	// �ҋ@�p�̃^�C�}�[
	float idleTimer = 3.0f;
	// �ːi�U���p�̃^�C�}�[
	float rushTimer = 4.0f;
	// �^��g�U���p�̃^�C�}�[
	float aerial_slashTimer = 3.0f;

	// IEnumerator�Œ�~����b��
	float StopTime = 0.5f;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		// �v���C���[�̃I�u�W�F�N�g���擾
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

		// �e��s���̃t���O�s���Ǘ�
		if (babu_endFlag == true)
		{
			Flying_Act_Flag();
		}

		// MOVE
		// 4�b�ԁA�v���C���[�Ɍ������Ĉړ�������
		if (moveFlag == true)
		{
			Debug.Log("MOVE");

			// moveTimer��1�b�����炷
			moveTimer -= 1.0f * Time.deltaTime;

			// �v���C���[�Ɍ������Ĉړ�
			EM_forPlayer();

			// moveTimer��0�b�ȉ��Ȃ���s
			if (moveTimer <= 0)
			{
				// moveFlag��false�ɂ���
				moveFlag = false;
				// duringFlag��false�ɂ���
				duringFlag = false;
			}
		}

		// IDLE
		// 3�b�ԁA���̏�őҋ@
		if (idleFlag == true)
		{
			Debug.Log("IDLE");

			// idleTimer��1�b�����炷
			idleTimer -= 1.0f * Time.deltaTime;

			// idleTimer��0�b�ȉ��Ȃ���s
			if (idleTimer <= 0)
			{
				// idleFlag��false�ɂ���
				idleFlag = false;
				// duringFlag��false�ɂ���
				duringFlag = false;
			}
		}

		// RUSH_ATTACK
		// 2�b�����ăv���C���[�̍��W�܂ňړ��A���2�b�����Č���y�ɖ߂�
		if(rushFlag == true)
		{
			Debug.Log("RUSH");

			// rushTimer��1�b�����炷
			rushTimer -= 1.0f * Time.deltaTime;

			// coordinate_getFlag��false�Ȃ���s
			if (coordinate_getFlag == false)
			{
				// ���g�̍��W���擾
				BackPosition = transform.position;
				// �v���C���[�̍��W���擾
				PlayerPosition = PlayerObject.transform.position;
				// �擾�����̂�true�ɂ���
				coordinate_getFlag = true;
			}

			// rushTimer��2�b�ȏ�A3�b�����Ȃ���s
			if (2.0 <= rushTimer && rushTimer < 3.0)
			{
				// �v���C���[�Ɍ������ēːi
				Attack_Rush();
			}
			// rushTimer��0�b����A2.0�b�����Ȃ���s
			else if(0.0 < rushTimer && rushTimer < 2.0)
			{
				// �߂�
				Rush_Back();
			}
			//rushTimer��0�b�ȉ��Ȃ���s
			else if (rushTimer <= 0)
			{
				// coordinate_getFlag��false�ɂ���
				coordinate_getFlag = false;
				// rushFlag��false�ɂ���
				rushFlag = false;
				// duringFlag��false�ɂ���
				duringFlag = false;
			}
		}

		// AERIAL-SLASH_ATTACK
		// �������C���Ƀv���C���[�֌������Ĕ��
		if(aerial_slashFlag == true)
		{
			Debug.Log("AERIAL-SLASH");

			// aerial_slashTimer��1�b�����炷
			aerial_slashTimer -= 1.0f * Time.deltaTime;

			// aerial_slashTimer��2�b�ȏ�A3�b�����Ȃ���s
			if (2.0 <= aerial_slashTimer && aerial_slashTimer < 3.0)
			{
				// Debug.Log("�U������");
				// �U������
			}
			// aerial_slashTimer��0�b����A2�b�����Ȃ���s
			else if (0.0 < aerial_slashTimer && aerial_slashTimer < 2.0)
			{
				// Debug.Log("�U��");

				// ���łɐ^��g������Ă�������s
				if(fireFlag == true)
				{
					// Debug.Log("���ˍς݂���`��");
				}
				// �܂��^��g������Ă��Ȃ���������s
				else if(fireFlag == false)
				{
					// Debug.Log("���ˁI");

					// �^��g�v���n�u��GameObject�^�Ŏ擾
					GameObject aerialSlash = (GameObject)Resources.Load("Prefabs/Flying_AerialSlash");
					// �^��g�v���n�u�����ɁA�C���X�^���X�𐶐�
					Instantiate(aerialSlash, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

					// fireFlag��true�ɂ���
					fireFlag = true;
				}
			}
			// aerial_slashTimer��0�b�ȉ��Ȃ�A���s
			else if (aerial_slashTimer <= 0)
			{
				// fireFlag��false�ɂ���
				fireFlag = false;
				// aerial_slashFlag��false�ɂ���
				aerial_slashFlag = false;
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

	// �ːi�p�֐�
	private void Attack_Rush()
	{
		float rush = RushSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, PlayerPosition*2, rush);
	}

	// �ːi��ɖ߂�p�֐�
	private void Rush_Back()
	{
		float back = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, BackPosition, back);
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
	private void Flying_Act_Flag()
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

				// Timer�����ɖ߂�
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

				// Timer�����ɖ߂�
				idleTimer = 3.0f;
			}

			// Debug.Log("stay");
		}
		else if (flying_act == 2)       // �ːi�U��
		{          
			// duringFlag ��false�Ȃ���s
			if (duringFlag == false)
			{
				// rushFlag��duringFlag��true�ɂ���
				rushFlag = true;
				duringFlag = true;

				// Timer�����ɖ߂�
				rushTimer = 4.0f;
			}

			// Debug.Log("attack");
		}
		else if (flying_act == 3)       // �^��g�U��
		{           
			// duringFlag ��false�Ȃ���s
			if (duringFlag == false)
			{
				// aerial_slashFlag��duringFlag��true�ɂ���
				aerial_slashFlag = true;
				duringFlag = true;

				// Timer�����ɖ߂�
				aerial_slashTimer = 3.0f;
			}

			// Debug.Log("wave_attack");
		}
	}

	// Babu��p�̃I�u�W�F�N�g��~�R���[�`��
	IEnumerator Stop()
	{
		 EM_forPlayer();

		// 0.5�b�҂�
		yield return new WaitForSeconds(StopTime);

		// ������肽�ĂłȂ��Ȃ�
		spawnFlag = false;

		// Debug.Log("��������s�\���");

		// 2�b�҂�
		yield return new WaitForSeconds(2);

		babu_endFlag = true;

		// Debug.Log("babu���I���܂���");
		// Debug.Log(spawnFlag);
	}
}