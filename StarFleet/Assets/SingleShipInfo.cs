using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleShipInfo : MonoBehaviour
{
    public GameObject panel;
    public Dictionary<int,string> shipname=new Dictionary<int, string>
    {
        [1] = "Destroyer",
        [2] = "Crusier",
        [3] = "BattleShip",
        [4] = "Turrent"
    };
    public void Showinfo(GameObject selected)
    {
        panel.transform.Find("Title").GetComponent<TextMeshPro>().text=shipname[selected.GetComponent<ShipBase>().Shiptype];
        Instantiate(panel, new Vector3(803.4f, -467f, 0), Quaternion.identity);
    }
}
