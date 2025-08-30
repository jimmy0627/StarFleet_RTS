using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class Senscor : MonoBehaviour
{
    //利用CircleCollider的覆蓋範圍當作雷達範圍 在利用contacs印出範圍內的物件
    public List<Transform> Radarimage = new List<Transform>();
    public float Radarsize;
    public bool IFF;
    private CircleCollider2D Radar;
    private Color set;
    void Start()
    {
        Radarsize = transform.parent.GetComponent<ShipBase>().RadarSize;
        Radar = gameObject.GetComponent<CircleCollider2D>();
        Radar.radius = Radarsize; //設置雷達範圍
    }
    void Update()
    {
        if (Radarimage.Any())
        {
            for (int i = 0; i < Radarimage.Count(); i++)
            {
                if (Radarimage[i].gameObject.GetComponent<ShipBase>().HP == 0 && Radarimage[i] != null)
                {
                    Radarimage.Remove(Radarimage[i]);
                }
                else
                {
                    Radarimage[i].transform.Find("Hull").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    Radarimage[i].transform.Find("Canvas").gameObject.SetActive(true);
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<ShipBase>() != null)
        {
            if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
            {
                SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
                Radarimage.Add(collision.transform);  //進入碰撞圈，加入雷達範圍
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);//不透明
                collision.transform.Find("Canvas").gameObject.SetActive(true);
            }
        }
    }
    /*
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<ShipBase>() != null)
        {
            if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
            {
                SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);//不透明
                collision.transform.Find("Canvas").gameObject.SetActive(true);
            }
        }
    }
    */
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") != transform.parent.GetComponent<ShipBase>().isEnemy)
        {
            SpriteRenderer sr = collision.transform.Find("Hull").GetComponent<SpriteRenderer>();
            Radarimage.Remove(collision.transform);
            if (sr != null)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);//不透明
                collision.transform.Find("Canvas").gameObject.SetActive(false);
            }
        }

    }
}
