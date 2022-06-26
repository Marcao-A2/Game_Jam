using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
    private int currentStarsNum = 0;
    public int levelIndex;

    public void BackButton()
    {
        SceneManager.LoadScene("LoadLevel");
    }

    public void PressStarsButton(int _starsNum)
    {
        currentStarsNum = _starsNum;

        if (currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _starsNum);
        }

        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, _starsNum));

        BackButton();
    }
}
