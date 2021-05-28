using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
	// ボタン
	Button Stage1;
	Button Stage2;

	// Start is called before the first frame update
	void Start()
	{
		// ボタンコンポーネントの取得
		Stage1 = GameObject.Find("/Canvas/Stage1_Select_Button").GetComponent<Button>();
		Stage2 = GameObject.Find("/Canvas/Stage2_Select_Button").GetComponent<Button>();

		// 最初に選択状態にしたいボタンの設定
		Stage1.Select();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}