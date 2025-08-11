
using System;
using UnityEngine;

public class playermove : MonoBehaviour
{
    private Vector2 movedir;
    private Rigidbody2D rb;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movedir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = movedir * 3;
        RotateFaceMoveDIR();

    }
    private void RotateFaceMoveDIR()
    {
        Vector2 MoveDIR = GetComponent<Rigidbody2D>().linearVelocity;
        if (MoveDIR != Vector2.zero)
        {
            float angel = (float)(Math.Atan2(MoveDIR.x, MoveDIR.y) * Mathf.Rad2Deg);
            Quaternion targetRotation = Quaternion.AngleAxis(angel, Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
        }

    }
}
