using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Create : MonoBehaviour
{
	// ����A�񑫕��s�𐶐����邩���C���X�y�N�^�[�ő���
	[SerializeField]
	int spawnCount_Walk = 0;
	// ����A���Ă𐶐����邩���C���X�y�N�^�[�ő���
	[SerializeField]
	int spawnCount_Fly = 0;

	// �񑫕��s��X���W
	public float sWalk_X;
	// �񑫕��s��Y���W
	public float sWalk_Y = 0.0f;

	// ���Ă�X���W
	public float sFly_X;
	// ���Ă�Y���W
	public float sFly_Y;

	// ���������Ƃ�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �񑫕��s�����pRandom�N���X�̃C���X�^���X�𐶐�
		var rand_walk = new System.Random();

		// ���Đ����pRandom�N���X�̃C���X�^���X�𐶐�
		var rand_fly = new System.Random();

		// �񑫕��s�𐳂̑������̑��̂ǂ���ɐ������邩���߂�Random�N���X�̃C���X�^���X�𐶐�
		var rand_which = new System.Random();

		// sCount_Walk���A�񑫕��s�𐶐�
		for (int i = 0; i < spawnCount_Walk; i++)
		{
			// �K���ȗ����𐶐�����
			int one_or_two = rand_which.Next();
			// �������ꂽ������2�Ŋ������]������߂�
			int enemy_which = one_or_two % 2;

			// �񑫕��s�𐳂̑��ɐ���
			if (enemy_which == 1)
			{
				// X���W�̗�����1100~1500�̊ԂŐ���
				sWalk_X = rand_walk.Next(minValue: 1100, maxValue: 1500);

				// �񑫕��s�v���n�u��GameObject�^�Ŏ擾
				GameObject eWalk = (GameObject)Resources.Load("Prefabs/Enemy_Walking");
				// �񑫕��s�v���n�u�����ɁA�C���X�^���X�𐶐�
				Instantiate(eWalk, new Vector3(sWalk_X, sWalk_Y, 0.0f), Quaternion.identity);
			}
			// �񑫕��s�𕉂̑��ɐ���
			else
			{
				// X���W�̗�����1100~1500�̊ԂŐ���
				sWalk_X = rand_walk.Next(minValue: -1500, maxValue: -1100);

				// �񑫕��s�v���n�u��GameObject�^�Ŏ擾
				GameObject eWalk = (GameObject)Resources.Load("Prefabs/Enemy_Walking");
				// �񑫕��s�v���n�u�����ɁA�C���X�^���X�𐶐�
				Instantiate(eWalk, new Vector3(sWalk_X, sWalk_Y, 0.0f), Quaternion.identity);
			}
		}

		// sCount_Fly���A���Ă𐶐�
		for (int i = 0; i < spawnCount_Fly; i++)
		{
			// X���W�̗�����-1500~1500�̊ԂŐ���
			sFly_X = rand_fly.Next(minValue: -1500, maxValue: 1500);
			// Y���W�̗�����700~1000�̊ԂŐ���
			sFly_Y = rand_fly.Next(minValue: 700, maxValue: 1000);

			// ���ăv���n�u��GameObject�^�Ŏ擾
			GameObject eFly = (GameObject)Resources.Load("Prefabs/Enemy_Flying");
			// ���ăv���n�u�����ɁA�C���X�^���X�𐶐�
			Instantiate(eFly, new Vector3(sFly_X, sFly_Y, 0.0f), Quaternion.identity);
		}

		Destroy(this.gameObject);
	}
}