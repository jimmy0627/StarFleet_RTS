using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class BattlePoint : MonoBehaviour
{
    public List<GameObject> InsidePoint = new List<GameObject>();
    public SpriteRenderer spriteRenderer;
    public int Bluepoint = 0;
    public int Redpoint = 0;
    public int Point = 0;
    private Coroutine Occupiedroutine;
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ShipBase>() != null)
        {
            InsidePoint.Add(collision.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var item in InsidePoint)
        {
            if (item.CompareTag("Self") && item!=null) Bluepoint += 1;
            else Redpoint += 1;

            if (Bluepoint > Redpoint) Point += 1;
            else Point -= 1;
        }
        Occupiedroutine = StartCoroutine(Occupied());
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ShipBase>() != null)
        {
            InsidePoint.Remove(collision.gameObject);
        } 
    }

    private IEnumerator Occupied()
    {
        if (Bluepoint > Redpoint) Point += 1;
        else if (Bluepoint < Redpoint) Point -= 1;

        if (Point > 0) spriteRenderer.color = new Color(0f, 0f, 1f, 0.5f);
        else if (Point < 0) spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
        else spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        Redpoint = Bluepoint = Point = 0;
        yield return new WaitForSeconds(1);
    }

}
