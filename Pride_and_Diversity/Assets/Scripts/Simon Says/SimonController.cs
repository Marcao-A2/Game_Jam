using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonController : MonoBehaviour
{
    public static SimonController instance;
    public GameObject easy;
    public int levelTag;
    public Text lives;
    public GameObject victory;
    public GameObject defeat;
    public int chances;

    public Text one;

    private void Start()
    {
        StartCoroutine(BeginGame());
        lives.text = chances.ToString();
        FindObjectOfType<AudioManager>().Play("Audience");
    }

    private void Update()
    {
        if (levelTag == 3)
        {
            FindObjectOfType<AudioManager>().Stop("Audience");
            victory.SetActive(true);
            lives.gameObject.SetActive(false);
            one.gameObject.SetActive(false);
        }

        if (chances == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Audience");
            defeat.SetActive(true);
            lives.gameObject.SetActive(false);
            one.gameObject.SetActive(false);
        }

        lives.text = chances.ToString();
    }

    public static void ActivateSimon(GameObject game)
    {
        game.gameObject.SetActive(true);
    }

    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(2f);
        easy.gameObject.SetActive(true);
    }
}
