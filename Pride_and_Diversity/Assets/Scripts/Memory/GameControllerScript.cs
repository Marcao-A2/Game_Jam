using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public const int colunes = 4;
    public const int rows = 2;

    public const float xSpace = 4f;
    public const float ySpace = 5f;

    public GameObject pasta;

    [SerializeField] private MemoryGame startObject;
    [SerializeField] private Sprite[] images;

    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3 };
        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        for (int i = 0; i < colunes; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                MemoryGame gameImage;
                if(i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    gameImage = Instantiate(startObject) as MemoryGame;
                    gameImage.gameObject.transform.SetParent(pasta.transform);
                    gameImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }

                int index = j * colunes + i;
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

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
        if(firstOpen.spriteId == lastOpen.spriteId)
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
    }

    public void Restart()
    {
        SceneManager.LoadScene("MemoryGame");
    }
}
