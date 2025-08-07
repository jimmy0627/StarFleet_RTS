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
    public float Radarsize ;
    public bool IFF;
    private CircleCollider2D Radar;
    void Start()
    {
        Radarsize = transform.parent.GetComponent<ShipBase>().RadarSize;
        Radar = gameObject.GetComponent<CircleCollider2D>();
        Radar.radius = Radarsize; //設置雷達範圍
/*        
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
        }*/
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
        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);//不透明
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
        {
            Radarimage.Remove(collision.transform);
        }
    }
}
