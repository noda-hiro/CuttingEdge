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
	// �񑫕��s�̈ړ����x
	[SerializeField]
	float SPEED = 0.0f;

	// ������肽�Ă��ǂ���
	bool spawnFlag = true;

	// IEnumerator�Œ�~����b��
	float StopTime = 1f;

	// �v���C���[�Ɍ������Ĉړ�����Ƃ��ɕK�v�Ȃ��
	Vector3 targetPosition;

	// ����Ԃ��ǂ���
	private bool isVisible = false;
	// ���̃v���p�e�B�[
	public bool IsVisible
	{
		get { return isVisible; }
	}

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		// HP�Ȃ��Ȃ����玀�ʁB��l�������ɂȁI
		Death();

		// ���W���擾�����`��^^
		Coordinate();

		// �����Ⴈ���Ⴕ���̂Ŏ��s�I
		babubabu();
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

	// ���W���擾����֐�
	private void Coordinate()
	{
		// �v���C���[�̃I�u�W�F�N�g�̍��W���擾
		targetPosition = GameObject.Find("Test_Player").transform.position;
	}

	// �G�l�~�[���A�v���C���[�Ɍ������Ĉړ�����֐�
	private void EM_forPlayer()
	{
		float move = SPEED * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
	}

	// �X�|�[�������Ă̂Ƃ��Ɏ��s����֐�
	private void babubabu()
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

	// �I�u�W�F�N�g�̒�~�֐�
	IEnumerator Stop()
	{
		EM_forPlayer();
		// 1�b�҂�
		yield return new WaitForSeconds(StopTime);
		// ������肽�ĂłȂ��Ȃ�
		spawnFlag = false;
		// Debug.Log(spawnFlag);
	}
}
