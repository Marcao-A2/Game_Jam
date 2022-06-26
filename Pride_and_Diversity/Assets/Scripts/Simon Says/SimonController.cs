using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonController : MonoBehaviour
{
    public static SimonController instance;
    public SimonSays easy;

    private void Start()
    {
        StartCoroutine(BeginGame());
    }

    public static void ActivateSimon(SimonSays game)
    {
        game.gameObject.SetActive(true);
    }

    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(2f);
        easy.gameObject.SetActive(true);
    }
}
