using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public static PuzzleController instance;
    public GameObject first;
    public int count;
    public int levelTag;

    public SingleLevel endLevel;
    public GameObject defeat;

    private void Start()
    {
        StartCoroutine(BeginGame());
        count = 0;
        FindObjectOfType<AudioManager>().Stop("Menu");
        FindObjectOfType<AudioManager>().Play("Level");
        FindObjectOfType<AudioManager>().Play("Audience");
    }

    private void Update()
    {
        if (count >= 2 && levelTag == 3)
        {
            StartCoroutine(Next());
            FindObjectOfType<AudioManager>().Play("Clap");
        }

        if (levelTag == 3 && count < 2)
        {
            defeat.SetActive(true);
        }
    }

    public static void ActivatePuzzle(GameObject game)
    {
        game.gameObject.SetActive(true);
    }

    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(2f);
        first.gameObject.SetActive(true);
    }

    IEnumerator Next()
    {
        FindObjectOfType<AudioManager>().Stop("Audience");
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Stop("Clap");
        endLevel.PressStarsButton(1);
    }
}
