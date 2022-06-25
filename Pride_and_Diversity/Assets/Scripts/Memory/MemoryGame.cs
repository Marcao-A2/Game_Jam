using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MonoBehaviour
{
    [SerializeField] private GameObject imageUnknown;
    [SerializeField] private GameControllerScript gameController;

    private int _spriteId;

    public int spriteId
    {
        get { return _spriteId; }
    }
    public void OnMouseDown()
    {
        if (imageUnknown.activeSelf && gameController.canOpen)
        {
            imageUnknown.SetActive(false);
            gameController.ImageOpened(this);
        }
    }

    public void  ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<Image>().sprite = image;
    }

    public void Close()
    {
        imageUnknown.SetActive(true);
    }
}
