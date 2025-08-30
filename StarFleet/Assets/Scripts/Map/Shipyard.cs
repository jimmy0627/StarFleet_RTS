using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Shipyard : MonoBehaviour
{
    public List<Vector3> GoToPoint = new List<Vector3>();
    public int mode;
    public GameObject Destroier;
    public int numberD;
    public GameObject Crusier;
    public int numberC;
    public GameObject Battleship;
    public int numberB;
    private Vector3 delta;
    int counts = 0;
    int dis = 3;
    void Start()
    {
        
        if (mode == 1)
        {
            Spwanwhillstart();//遊戲開始時生成一批，並往後不會再生成
        }
        if (mode == 2)
            {
                //遊戲進行到某個階段時生成，可用在關卡2/3
            }
    }
    Vector3 Spawnspread()//隨機加上一個位移 避免生出來的船撞在一起
    {
        delta = Quaternion.AngleAxis(counts, Vector3.forward)*Vector3.up*dis;
        counts += 60;
        if (counts >= 360)
        {
            dis += 3;
            counts = 0;
        }
        return delta;
    }
    void Spwanwhillstart()//mode1 依序生出驅逐/巡洋/戰列
    {
        for (int i = 0; i < numberB; i++)
        {
            if (Battleship != null)
            {
                Vector3 SpwanSpot = Spawnspread() + transform.position;
                SpwanSpot.z = 0;
                if (GoToPoint.Any())
                {
                    List<Vector3> copiedPath = new List<Vector3>(GoToPoint);
                    Battleship.GetComponent<ShipBase>().TargetDes = copiedPath;
                }
                else Battleship.GetComponent<ShipBase>().TargetDes = new List<Vector3>();
                Instantiate(Battleship, SpwanSpot, Quaternion.identity);
            }

        }
        for (int i = 0; i < numberC; i++)
        {
            if (Crusier!=null)
            {
                Vector3 SpwanSpot = Spawnspread() + transform.position;
                SpwanSpot.z = 0;
                if (GoToPoint.Any())
                {
                    List<Vector3> copiedPath = new List<Vector3>(GoToPoint);
                    Crusier.GetComponent<ShipBase>().TargetDes = copiedPath;
                }
                else Crusier.GetComponent<ShipBase>().TargetDes = new List<Vector3>();
                Instantiate(Crusier, SpwanSpot, Quaternion.identity);
            }
        }
        for (int i = 0; i < numberD; i++)
        {
            if (Destroier != null)
            {
                Vector3 SpwanSpot = Spawnspread() + transform.position;
                SpwanSpot.z = 0;
                if (GoToPoint.Any())
                {
                    List<Vector3> copiedPath = new List<Vector3>(GoToPoint);
                    Destroier.GetComponent<ShipBase>().TargetDes = GoToPoint;
                }
                else Destroier.GetComponent<ShipBase>().TargetDes = new List<Vector3>();
                Instantiate(Destroier, SpwanSpot, Quaternion.identity);
            }
        }

    }
}
