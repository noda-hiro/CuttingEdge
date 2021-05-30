using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Create : MonoBehaviour
{
	// 何回、二足歩行を生成するかをインスペクターで操作
	[SerializeField]
	int spawnCount_Walk = 0;
	// 何回、飛翔を生成するかをインスペクターで操作
	[SerializeField]
	int spawnCount_Fly = 0;

	// 二足歩行のX座標
	public float sWalk_X;
	// 二足歩行のY座標
	public float sWalk_Y = 0.0f;

	// 飛翔のX座標
	public float sFly_X;
	// 飛翔のY座標
	public float sFly_Y;

	// 当たったとき
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 二足歩行生成用Randomクラスのインスタンスを生成
		var rand_walk = new System.Random();

		// 飛翔生成用Randomクラスのインスタンスを生成
		var rand_fly = new System.Random();

		// 二足歩行を正の側か負の側のどちらに生成するか決めるRandomクラスのインスタンスを生成
		var rand_which = new System.Random();

		// sCount_Walk分、二足歩行を生成
		for (int i = 0; i < spawnCount_Walk; i++)
		{
			// 適当な乱数を生成する
			int one_or_two = rand_which.Next();
			// 生成された乱数を2で割った余りを求める
			int enemy_which = one_or_two % 2;

			// 二足歩行を正の側に生成
			if (enemy_which == 1)
			{
				// X座標の乱数を1100~1500の間で生成
				sWalk_X = rand_walk.Next(minValue: 1100, maxValue: 1500);

				// 二足歩行プレハブをGameObject型で取得
				GameObject eWalk = (GameObject)Resources.Load("Prefabs/Enemy_Walking");
				// 二足歩行プレハブを元に、インスタンスを生成
				Instantiate(eWalk, new Vector3(sWalk_X, sWalk_Y, 0.0f), Quaternion.identity);
			}
			// 二足歩行を負の側に生成
			else
			{
				// X座標の乱数を1100~1500の間で生成
				sWalk_X = rand_walk.Next(minValue: -1500, maxValue: -1100);

				// 二足歩行プレハブをGameObject型で取得
				GameObject eWalk = (GameObject)Resources.Load("Prefabs/Enemy_Walking");
				// 二足歩行プレハブを元に、インスタンスを生成
				Instantiate(eWalk, new Vector3(sWalk_X, sWalk_Y, 0.0f), Quaternion.identity);
			}
		}

		// sCount_Fly分、飛翔を生成
		for (int i = 0; i < spawnCount_Fly; i++)
		{
			// X座標の乱数を-1500~1500の間で生成
			sFly_X = rand_fly.Next(minValue: -1500, maxValue: 1500);
			// Y座標の乱数を700~1000の間で生成
			sFly_Y = rand_fly.Next(minValue: 700, maxValue: 1000);

			// 飛翔プレハブをGameObject型で取得
			GameObject eFly = (GameObject)Resources.Load("Prefabs/Enemy_Flying");
			// 飛翔プレハブを元に、インスタンスを生成
			Instantiate(eFly, new Vector3(sFly_X, sFly_Y, 0.0f), Quaternion.identity);
		}

		Destroy(this.gameObject);
	}
}