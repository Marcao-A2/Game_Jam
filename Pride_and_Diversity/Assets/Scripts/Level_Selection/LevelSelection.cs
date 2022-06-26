using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public Image unlockImage;

    private void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)//If the firts level star is bigger than 0, second level can play
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if (!unlocked)//MARKER if unclock is false means This level is clocked!
        {
            unlockImage.gameObject.SetActive(true);
        }
        else//if unlock is true means This level can play !
        {
            unlockImage.gameObject.SetActive(false);
        }
    }

    public void PressSelection(string levelName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
