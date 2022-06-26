using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGameController : MonoBehaviour
{
    public RobotMinigame[] bodyParts;
    public RobotGameController nextGame;
    int count;
    bool[] flags;
    bool itsOver;

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
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        PuzzleController.ActivatePuzzle(nextGame);
        gameObject.SetActive(false);
    }
}
