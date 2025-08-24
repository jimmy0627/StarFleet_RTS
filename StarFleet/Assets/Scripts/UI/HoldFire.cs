using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HoldFire : MonoBehaviour
{
    //此腳本請放在UIActionScipt物件中，並且從Botton的On Click中呼叫CallthisInBotton函數

    public List<GameObject> Unitinlist = new List<GameObject>();
    public TextMeshProUGUI BottonText;
    private Button HoldBotton;
    void Start()
    {
        HoldBotton = transform.parent.Find("OrderPanel/HoldFire").GetComponent<Button>();
    }
    void Update() //更新框選範圍內的單位 且無被框選單位時讓按鈕無法使用
    {
        Unitinlist = transform.GetComponent<SelectionBox>().selectedUnits;
        if (!Unitinlist.Any()) HoldBotton.interactable = false;
        else HoldBotton.interactable = true;
    }
    private void HoldFireFunction() //將匡選範圍內的單位設置為停火 並設置按鈕為開火
    {
        foreach (var item in Unitinlist)
        {
            item.transform.Find("Weapon").GetComponent<Attack>().HoldFire = true;
        }
        BottonText.GetComponent<TextMeshProUGUI>().text = "Open Fire";
    }
    private void OpenFireFunction() //將匡選範圍內的單位設置為開火 並設置按鈕為停火
    {
        foreach (var item in Unitinlist)
        {
            item.transform.Find("Weapon").GetComponent<Attack>().HoldFire = false;
        }
        BottonText.GetComponent<TextMeshProUGUI>().text = "Hold Fire";
        Debug.Log(1);
    }
    public void CallthisInBotton() //On click調度時用 框選單位中超過一半單位處於停火狀態時觸發設置開火程序 反之
    {
        int amont = 0;
        foreach (var item in Unitinlist)
        {
            if (item.transform.Find("Weapon").GetComponent<Attack>().HoldFire) amont += 1;
        }
        if (amont <= Unitinlist.Count / 2)
        {
            HoldFireFunction();
        }
        else OpenFireFunction();
    }
}