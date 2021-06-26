using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerial_Slash_Control : MonoBehaviour
{
	// �^��g�̑��x
	[SerializeField]
	float aerial_slashSpeed = 0.0f;

	// �ʂ̃X�N���v�g�ɂ���l�����Ă����ϐ�
	int PlayerHP = 0;
	int EnemyATK = 0;

	// �o��������ł̃^�C�}�[
	float disappearTimer = 2.0f;

	// �v���C���[�̍��W���擾�������ǂ����̃t���O
	bool player_coordinateFlag = false;

	// GameObject�����Ă������
	private GameObject PlayerObject;
	private GameObject EnemyObject;

	// ���W�����Ă������
	private Vector3 player_position;

	// �X�N���v�g�����Ă������
	private Test_Player_Control PlayerScript;
	private Enemy_Flying_Control FlyingScript;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		// �v���C���[�̃I�u�W�F�N�g���擾
		PlayerObject = GameObject.Find("Test_Player");
		// �v���C���[�̃X�N���v�g���擾
		PlayerScript = PlayerObject.GetComponent<Test_Player_Control>();
		// �v���C���[��HP���擾
		PlayerHP = PlayerScript.hp;

		// �G�l�~�[�E���ẴI�u�W�F�N�g���擾
		EnemyObject = GameObject.Find("Enemy_Flying(Clone)");
		// �G�l�~�[�E���ẴX�N���v�g���擾
		FlyingScript = EnemyObject.GetComponent<Enemy_Flying_Control>();
		// �G�l�~�[�E���Ă�ATK���擾
		EnemyATK = FlyingScript.atk;
	}

	// Update is called once per frame
	void Update()
	{
		// 1�b�����炷
		disappearTimer -= Time.deltaTime;

		// �v���C���[�̍��W��1�񂾂��擾����
		PLayerPosition();

		// �擾�����v���C���[�̍��W�Ɍ������Ĉړ�����
		AerialSlash_Move();

		// 4�b�o�߂�����A������j�󂷂�
		TimeOver();
	}

	// �v���C���[�ɐ^��g�����������Ƃ�
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// �v���C���[��HP���G�l�~�[�E���Ă�ATK���������炷
		PlayerHP -= EnemyATK;
		PlayerScript.hp = PlayerHP;

		// ���ł���
		Disappear();
	}

	// ���g��j�󂷂�֐�
	private void Disappear()
	{
		Destroy(this.gameObject);
	}

	// �v���C���[�̍��W��1�񂾂��擾����֐�
	private void PLayerPosition()
	{
		// player_coordinateFlag��false�̂Ƃ����s
		if (player_coordinateFlag == false)
		{
			// �v���C���[�̍��W���擾
			player_position = PlayerObject.transform.position;

			// player_coordinateFlag��true�ɂ���
			player_coordinateFlag = true;
		}
	}

	// �v���C���[���������W�܂ŁA���̑��x�Ői�ފ֐�
	private void AerialSlash_Move()
	{
		float move = aerial_slashSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, player_position*2, move);
	}

	// 4�b�o�߂�����A���M��j�󂷂�֐�
	private void TimeOver()
	{
		// disappeatTimer��0�b�ȉ��ɂȂ�������s
		if(disappearTimer <= 0)
		{
			Disappear();
		}
	}
}
