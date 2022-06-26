using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Menu");
    }

}
