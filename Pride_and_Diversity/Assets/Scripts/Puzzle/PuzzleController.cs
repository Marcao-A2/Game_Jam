using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public static PuzzleController instance;
    public RobotGameController first;

    private void Start()
    {
        StartCoroutine(BeginGame());
    }

    public static void ActivatePuzzle(RobotGameController game)
    {
        game.gameObject.SetActive(true);
    }

    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(2f);
        first.gameObject.SetActive(true);
    }
}
