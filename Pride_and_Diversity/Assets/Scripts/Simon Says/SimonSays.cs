using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject[] lightArray;
    [SerializeField] int[] lightOrder;
    //[SerializeField] GameObject simonSaysGamePanel;

    int level = 0;
    int buttonsclicked = 0;
    int colorOrderRunCount = 0;
    bool passed = false;
    bool won = false;
    Color32 red = new Color32(255, 39, 0, 255);
    Color32 blue = new Color32(25, 195, 21, 255);
    Color32 invisible = new Color32(74, 82, 74, 255);
    Color32 white = new Color32(255, 255, 255, 255);
    public float lightSpeed;
    [Range(5, 9)]
    public int maxLevel;
    public GameObject nextGame;
    public SimonController controller;
    public int levelTag;

    private void OnEnable()
    {
        level = 0;
        buttonsclicked = 0;
        colorOrderRunCount = -1;
        won = false;

        for (int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = Random.Range(0, 8);
        }

        level = 1;
        StartCoroutine(ColorOrder());
    }

    private void Update()
    {

    }

    public void ButtonClickOrder(int button)
    {
        buttonsclicked++;
        if (button == lightOrder[buttonsclicked - 1])
        {
            passed = true;
        }
        else
        {
            won = false;
            passed = false;
            controller.chances -= 1;
            StartCoroutine(ColorBlink(red));
        }

        if (buttonsclicked == level && passed && buttonsclicked != maxLevel)
        {
            level++;
            passed = false;
            StartCoroutine(ColorOrder());
        }

        if (buttonsclicked == level && passed && buttonsclicked == maxLevel)
        {
            won = true;
            StartCoroutine(ColorBlink(blue));
        }
    }

    private IEnumerator ColorOrder()
    {
        buttonsclicked = 0;
        colorOrderRunCount++;
        DisableInteractableButtons();

        for (int i = 0; i <= colorOrderRunCount; i++)
        {
            if (level >= colorOrderRunCount)
            {
                lightArray[lightOrder[i]].GetComponent<Image>().color = invisible;
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = blue;
                yield return new WaitForSeconds(lightSpeed);
                lightArray[lightOrder[i]].GetComponent<Image>().color = invisible;
            }
        }

        EnableInteractableButtons();
    }

    private IEnumerator ColorBlink(Color32 colorToBlink)
    {
        DisableInteractableButtons();

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = colorToBlink;
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = white;
            }

            yield return new WaitForSeconds(0.5f);
        }

        if (won)
        {
            controller.levelTag = levelTag;
            SimonController.ActivateSimon(nextGame);
            gameObject.SetActive(false);
        }
        else
        {
            EnableInteractableButtons();
            OnEnable();
        }

        if (controller.chances == 0)
        {
            gameObject.SetActive(false);
        }
    }

    void DisableInteractableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    void EnableInteractableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }
}
