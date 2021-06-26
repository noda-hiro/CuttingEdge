using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerial_Slash_Control : MonoBehaviour
{
	// 真空波の速度
	[SerializeField]
	float aerial_slashSpeed = 0.0f;

	// 別のスクリプトにある値を入れておく変数
	int PlayerHP = 0;
	int EnemyATK = 0;

	// 出現から消滅のタイマー
	float disappearTimer = 2.0f;

	// プレイヤーの座標を取得したかどうかのフラグ
	bool player_coordinateFlag = false;

	// GameObjectを入れておくやつ
	private GameObject PlayerObject;
	private GameObject EnemyObject;

	// 座標を入れておくやつ
	private Vector3 player_position;

	// スクリプトを入れておくやつ
	private Test_Player_Control PlayerScript;
	private Enemy_Flying_Control FlyingScript;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		// プレイヤーのオブジェクトを取得
		PlayerObject = GameObject.Find("Test_Player");
		// プレイヤーのスクリプトを取得
		PlayerScript = PlayerObject.GetComponent<Test_Player_Control>();
		// プレイヤーのHPを取得
		PlayerHP = PlayerScript.hp;

		// エネミー・飛翔のオブジェクトを取得
		EnemyObject = GameObject.Find("Enemy_Flying(Clone)");
		// エネミー・飛翔のスクリプトを取得
		FlyingScript = EnemyObject.GetComponent<Enemy_Flying_Control>();
		// エネミー・飛翔のATKを取得
		EnemyATK = FlyingScript.atk;
	}

	// Update is called once per frame
	void Update()
	{
		// 1秒ずつ減らす
		disappearTimer -= Time.deltaTime;

		// プレイヤーの座標を1回だけ取得する
		PLayerPosition();

		// 取得したプレイヤーの座標に向かって移動する
		AerialSlash_Move();

		// 4秒経過したら、自分を破壊する
		TimeOver();
	}

	// プレイヤーに真空波が当たったとき
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// プレイヤーのHPをエネミー・飛翔のATK分だけ減らす
		PlayerHP -= EnemyATK;
		PlayerScript.hp = PlayerHP;

		// 消滅する
		Disappear();
	}

	// 自身を破壊する関数
	private void Disappear()
	{
		Destroy(this.gameObject);
	}

	// プレイヤーの座標を1回だけ取得する関数
	private void PLayerPosition()
	{
		// player_coordinateFlagがfalseのとき実行
		if (player_coordinateFlag == false)
		{
			// プレイヤーの座標を取得
			player_position = PlayerObject.transform.position;

			// player_coordinateFlagをtrueにする
			player_coordinateFlag = true;
		}
	}

	// プレイヤーが居た座標まで、一定の速度で進む関数
	private void AerialSlash_Move()
	{
		float move = aerial_slashSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, player_position*2, move);
	}

	// 4秒経過したら、自信を破壊する関数
	private void TimeOver()
	{
		// disappeatTimerが0秒以下になったら実行
		if(disappearTimer <= 0)
		{
			Disappear();
		}
	}
}
