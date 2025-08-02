using System;
using System.IO.Compression;
using Unity.Mathematics;
using UnityEngine;

public class Shipyard : MonoBehaviour
{
    public int mode;
    public GameObject Destroier;
    public int numberD;
    public GameObject Crusier;
    public int numberC;
    public GameObject Battleship;
    public int numberB;
    private Vector3 delta;
    int counts = 0;
    int dis = 1;
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
            dis += 1;
            counts = 0;
        }
        return delta;
    }
    void Spwanwhillstart()//mode1 依序生出驅逐/巡洋/戰列
    {
        for (int i = 0; i < numberB; i++)
        {
            if (Battleship!=null)
            {
                Instantiate(Battleship, Spawnspread(), Quaternion.identity);
            }
            
        }
        for (int i = 0; i < numberC; i++)
        {
            if (Crusier!=null)
            {
                Instantiate(Crusier, Spawnspread(), Quaternion.identity);
            }
        }
        for (int i = 0; i < numberD; i++)
        {
            if (Destroier != null)
            {
                Instantiate(Destroier, Spawnspread(), Quaternion.identity);
            }
        }

    }
}
