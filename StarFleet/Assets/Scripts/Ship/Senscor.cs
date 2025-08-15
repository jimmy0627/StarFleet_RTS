using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Senscor : MonoBehaviour
{
    //利用CircleCollider的覆蓋範圍當作雷達範圍 在利用contacs印出範圍內的物件
    public List<Transform> Radarimage = new List<Transform>();
    private List<GameObject> objectsToControl = new List<GameObject>();//
    public float Radarsize;
    private CircleCollider2D Radar;
    void Start()
    {
        Radarsize = transform.parent.GetComponent<ShipBase>().RadarSize;
        Radar = gameObject.GetComponent<CircleCollider2D>();
        Radar.radius = Radarsize; //設置雷達範圍
        GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");//
        objectsToControl.AddRange(Enemy);
        foreach (GameObject obj in objectsToControl)
        {
            SpriteRenderer srr = obj.GetComponent<SpriteRenderer>();
            if (srr != null)
            {
                Color c = srr.color;
                srr.color = new Color(c.r, c.g, c.b, 0.2f); // 半透明
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<ShipBase>()!=null)
        {
            if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
            {
                Radarimage.Add(collision.transform);  //進入碰撞圈，加入雷達範圍
            } 
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var i in Radarimage)
        {
            SpriteRenderer sr = i.Find("Hull").GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);//處於雷達探測範圍，設置為不透明
                Debug.Log(i.transform.localScale);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (Radarimage.Find(x => x = collision.transform))
        {
            SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);//不透明
            }
            Radarimage.Remove(collision.transform);
        }
    }
}