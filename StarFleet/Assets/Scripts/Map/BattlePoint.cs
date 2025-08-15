using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BattlePoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int Bluepoint = 0; //藍方船隻數量
    public int Redpoint = 0; //紅方船隻數量
    float colorr = 1;
    float colorb = 1;
    float colorg = 1;
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        InvokeRepeating("Occupied", 0, 1);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ShipBase>() != null)
        {
            if (collision.gameObject.CompareTag("Self")) Bluepoint += 1;
            else Redpoint += 1;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ShipBase>() != null)
        {
            if (collision.gameObject.CompareTag("Self")) Bluepoint -= 1;
            else Redpoint -= 1;
        } 
    }

    void Occupied()
    {
        if (Bluepoint < Redpoint)
        {
            if (colorb > 0) colorb -= 0.1f;
            if (colorr < 1) colorr += 0.1f;
        }
        else if (Bluepoint > Redpoint)
        {
            if (colorr > 0) colorr -= 0.1f;
            if (colorb < 1) colorb += 0.1f;
        }
        if (Bluepoint + Redpoint > 0 && colorg>0) colorg -= 0.1f;
        spriteRenderer.color = new Color(colorr, colorg , colorb, 0.5f);
    }

}
