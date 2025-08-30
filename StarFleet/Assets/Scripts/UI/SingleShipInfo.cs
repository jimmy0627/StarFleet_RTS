using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SingleShipInfo : MonoBehaviour
{
    public Dictionary<int, string> shipname = new Dictionary<int, string>
    {
        [1] = "Destroyer",
        [2] = "Crusier",
        [3] = "BattleShip",
        [4] = "Turrent"
    };
    public TextMeshProUGUI title;
    public TextMeshProUGUI Damge;
    public TextMeshProUGUI FireRange;
    public TextMeshProUGUI RadarRange;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI ECM;

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void singleinfo(GameObject selected)
    {
        ShipBase ship = selected.GetComponent<ShipBase>();
        title.text = shipname[ship.Shiptype];
        Damge.text = ship.damage.ToString();
        FireRange.text = ship.FireRange.ToString();
        RadarRange.text = ship.RadarSize.ToString();
        HP.text = ship.HP.ToString();
        ECM.text = ship.ECM.ToString(); 
    }
}
