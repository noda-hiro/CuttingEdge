using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect_to_Stage : MonoBehaviour
{
    public void StageChange(string Stage)
	{
        SceneManager.LoadScene(Stage);
	}
}
