using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int rows = 4;
    int colunes;

    public const float xSpace = 4f;
    public const float ySpace = 5f;
    public int ganha;
    public int perde;
    public GameObject pasta;
    public GameObject proximo;
    public GameObject este;
    public GameObject derrota;

    [SerializeField] private MemoryGame startObject;
    [SerializeField] private Sprite[] images;

    [SerializeField] private SingleLevel nextLevel;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Audience");
    }

    private void OnEnable()
    {
        colunes = rows * 2;
        List<int> locations = new List<int>();
        for (int i = 0; i < colunes; i++)
        {
            locations.Add(i);
            locations.Add(i);
        }
        //locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        for (int i = 0; i < colunes; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                MemoryGame gameImage;
                if (i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    gameImage = Instantiate(startObject) as MemoryGame;
                    gameImage.gameObject.transform.SetParent(pasta.transform);
                    gameImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }

                int index = 0;
                if (locations.Count >= 1)
                {
                    index = Random.Range(0, locations.Count);
                }
                else
                {
                    index = 0;
                }
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);
                locations.Remove(locations[index]);

                float positionX = (xSpace * i) + startPosition.x;
                float positionY = (ySpace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }
    }

    private MemoryGame firstOpen;
    private MemoryGame lastOpen;

    private int score = 0;
    private int attempts = 0;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text attemptsText;

    public bool canOpen
    {
        get { return lastOpen == null; }
    }

    public void ImageOpened(MemoryGame startObject)
    {
        if (firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            lastOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == lastOpen.spriteId)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            lastOpen.Close();
        }

        attempts++;
        attemptsText.text = "Attempts: " + attempts;

        firstOpen = null;
        lastOpen = null;

        if(score >= 8)
        {
            yield return new WaitForSeconds(1.5f);
            nextLevel.PressStarsButton(2);
            nextLevel.BackButton();
            FindObjectOfType<AudioManager>().Stop("Audience");
        }

        if (score == ganha)
        {
            scoreText.text = "Success";
            yield return new WaitForSeconds(1.5f);
            proximo.SetActive(true);
            este.SetActive(false);
            
        }
        if(attempts == perde)
        {
            scoreText.text = "Fail";
            yield return new WaitForSeconds(1.5f);
            derrota.SetActive(true);
        }
        //else if (attempts > 8)
        //{
        //    scoreText.text = "Fail";
        //    yield return new WaitForSeconds(1.5f);
        //    PlayerPrefs.DeleteAll();
        //    nextLevel.PressStarsButton(0);
        //    nextLevel.BackButton();
        //}
    }

    public void Restart()
    {
        SceneManager.LoadScene("MemoryGame");
    }
}
