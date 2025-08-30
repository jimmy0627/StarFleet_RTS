using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitOrder : MonoBehaviour
{
    public List<GameObject> Unitinlist = new List<GameObject>();
    private List<Vector3> TargetDes = new List<Vector3>();
    private bool isPlanningPath = false;

    void Update()
    {
        Unitinlist = transform.GetComponent<SelectionBox>().selectedUnits;
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;

        // 開始多點規劃（按住 Shift 並點右鍵）
        if (Unitinlist.Any() && Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            isPlanningPath = true;
            TargetDes.Add(mousepos);
        }

        // 結束多點規劃（放開 Shift）
        if (isPlanningPath && !Input.GetKey(KeyCode.LeftShift))
        {
            foreach (var item in Unitinlist)
            {
                List<Vector3> copiedPath = new List<Vector3>(TargetDes);
                item.transform.GetComponent<ShipBase>().TargetDes = copiedPath;
            }

            TargetDes.Clear();
            isPlanningPath = false;
        }

        // 單點移動（沒按 Shift 時點右鍵）
        if (Unitinlist.Any() && Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift))
        {
            TargetDes.Clear(); // 清空之前的路徑
            TargetDes.Add(mousepos);

            foreach (var item in Unitinlist)
            {
                List<Vector3> copiedPath = new List<Vector3>(TargetDes);
                if(item!=null) item.transform.GetComponent<ShipBase>().TargetDes = copiedPath;
            }

            TargetDes.Clear(); // 清空原始路徑
        }
    }
}
