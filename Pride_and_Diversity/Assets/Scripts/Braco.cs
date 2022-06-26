using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braco : MonoBehaviour
{
    public Rigidbody2D torreta;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePosition - torreta.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        torreta.rotation = angle;
    }
}
