using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity.Mathematics;
public class BattlePoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int Bluepoint = 0; //藍方船隻數量
    public int Redpoint = 0; //紅方船隻數量
    public int TimeToContro;
    float colorr = 1;
    float colorb = 1;
    float colorg = 1;
    public int status=0;
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
        float delta = 1.0f / TimeToContro;
        if (Bluepoint < Redpoint)
        {
            colorb = math.max(colorb - delta, 0);
            colorr = math.min(colorr + delta, 1);
        }
        else if (Bluepoint > Redpoint)
        {
            colorr = math.max(colorr - delta, 0);
            colorb = math.min(colorb + delta, 1);
        }
        if (Bluepoint + Redpoint > 0 && colorg > 0) colorg -= delta;
        spriteRenderer.color = new Color(colorr, colorg, colorb, 0.5f); //佔領時的顏色變化

        if (spriteRenderer.color.b < 0.05) status = -1;
        else if (spriteRenderer.color.r < 0.05) status = 1;
        else status = 0; //傳遞當前佔領區所屬權 1為藍方 -1紅方 0為無特定屬於
    }
}
