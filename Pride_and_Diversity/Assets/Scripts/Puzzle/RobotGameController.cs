using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotGameController : MonoBehaviour
{
    public PuzzleController controller;
    public RobotMinigame[] bodyParts;
    public GameObject nextGame;
    int count;
    bool[] flags;
    bool itsOver;
    public int levelTag;

    public float timeToLose;
    public Text deathTimer;

    private void OnEnable()
    {
        count = 0;
        itsOver = false;
        flags = new bool[9];
        for (int j = 0; j < flags.Length; j++)
        {
            flags[j] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (bodyParts[i].isConnected && !flags[i])
            {
                count++;
                flags[i] = true;
            }
        }

        if (count == 9 && !itsOver)
        {
            StartCoroutine(Delay());
            itsOver = true;
        }

        if (timeToLose >= 0 && !itsOver)
        {
            timeToLose -= Time.deltaTime;
            DisplayDeathTime(timeToLose);
        }

        if (timeToLose <= 0)
        {
            StartCoroutine(Death());
        }
    }

    void DisplayDeathTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        deathTimer.text = seconds.ToString();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        controller.count += 1;
        controller.levelTag = levelTag;
        PuzzleController.ActivatePuzzle(nextGame);
        gameObject.SetActive(false);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        controller.levelTag = levelTag;
        PuzzleController.ActivatePuzzle(nextGame);
        gameObject.SetActive(false);
    }
}
