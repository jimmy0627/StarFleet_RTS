using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class SelectionBox : MonoBehaviour
{
    bool clickStartOnUI = false;
/*    [SerializeField]
    float cameraMoveSpeed = 10f;
    [SerializeField]
    float cameraZoomSpeed = 0.05f;
    [SerializeField]
    float scrollSpeedMin = 1f, scrollSpeedMax = 3f;
    [SerializeField]
    float minZoom = 5f, maxZoom = 30f;
*/
    [SerializeField]
    RectTransform selectionBoxUI;  // UI 選取框 (需要有 Image)
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    GameObject singleUnitUI;
    [SerializeField]
    GameObject multiplyUnitUI;
    [SerializeField]
    GameObject UnitActionUI;

    private bool isSelecting = false;
    private Vector2 startPos, endPos, targetPos;
    Vector3 moveDirection = Vector3.zero;
    List<GameObject> selectedUnits = new List<GameObject>();


    void Update()
    {
        UnitSelect();
        if(selectedUnits.Count == 1)
        {
            if(selectedUnits[0] == null)
            {
                selectedUnits.Clear();
            }
            else
            {
                ////Unit a = selectedUnits[0].GetComponent<Unit>();
                ////singleUnitUI.GetComponent<SingleUnitInfo>().SetUI(a.GetName(), a.GetMaxHp(), a.GetHp());
            }

        }


        moveDirection = Vector2.zero;
        // 檢測鼠標是否在螢幕邊緣
        if (Input.mousePosition.x <= 10f)
        {
            //if (moveDirection.x < 0) moveDirection.x = -1*Mathf.Clamp(Mathf.Abs(moveDirection.x) * 1.05f, scrollSpeedMin, scrollSpeedMax);
            //else moveDirection.x = -1;
            moveDirection.x = -1;
        }
        else if (Input.mousePosition.x >= Screen.width - 10f)
        {
            //if (moveDirection.x > 0) moveDirection.x = Mathf.Clamp(Mathf.Abs(moveDirection.x) * 1.05f, scrollSpeedMin, scrollSpeedMax);
            //else moveDirection.x = 1;
            moveDirection.x = 1;
        }

        if (Input.mousePosition.y <= 10f)
        {
            //if (moveDirection.y < 0) moveDirection.y = -1 * Mathf.Clamp(Mathf.Abs(moveDirection.y) * 1.05f, scrollSpeedMin, scrollSpeedMax);
            //else moveDirection.y = -1;
            moveDirection.y = -1;
        }
        else if (Input.mousePosition.y >= Screen.height - 10f)
        {
            //if (moveDirection.y > 0) moveDirection.y = Mathf.Clamp(Mathf.Abs(moveDirection.y) * 1.05f, scrollSpeedMin, scrollSpeedMax);
            //else moveDirection.y = 1;
            moveDirection.y = 1;
        }

/*        // 移動攝影機
        mainCamera.transform.position += moveDirection * cameraMoveSpeed * Time.deltaTime;


        float scrollInput = Input.mouseScrollDelta.y;

        if (scrollInput != 0)
        {
            float zoomFactor = 1f + (scrollInput * cameraZoomSpeed);
            mainCamera.orthographicSize *= zoomFactor;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
*/
    }
    // 更新 UI 矩形顯示範圍
    void UpdateSelectionBox()
    {
        Vector2 boxStart = startPos;
        Vector2 boxEnd = endPos;

        float width = Mathf.Abs(boxEnd.x - boxStart.x);
        float height = Mathf.Abs(boxEnd.y - boxStart.y);

        Vector2 center = (boxStart + boxEnd) / 2;

        selectionBoxUI.position = center;
        selectionBoxUI.sizeDelta = new Vector2(width, height);
    }
    void UnitSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickStartOnUI = EventSystem.current.IsPointerOverGameObject();
            if (clickStartOnUI)
                return;
            UnSelectUnits();
            startPos = Input.mousePosition;
            selectionBoxUI.gameObject.SetActive(true);
            isSelecting = true;

        }

        // 更新選取範圍
        if (isSelecting)
        {
            endPos = Input.mousePosition;
            UpdateSelectionBox();
        }

        // 結束選取
        if (Input.GetMouseButtonUp(0))
        {
            if (clickStartOnUI)
            {
                // 清除旗標後離開，不進行選取
                clickStartOnUI = false;
                return;
            }
            isSelecting = false;
            selectionBoxUI.gameObject.SetActive(false);
            SelectUnits();
            //UnitActionUI.GetComponent<UnitUIAction>().ActionClear();
            if (selectedUnits.Count() == 1)
            {
                /*
                singleUnitUI.SetActive(true);
                Unit a = selectedUnits[0].GetComponent<Unit>();
                singleUnitUI.GetComponent<SingleUnitInfo>().SetUI(a.GetName(), a.GetMaxHp(), a.GetHp());

                if (a is ConstructionShip) UnitActionUI.GetComponent<UnitUIAction>().SetActionMenu(1);
                multiplyUnitUI.SetActive(false);
                */
            }
            else if (selectedUnits.Count() > 1)
            {
                singleUnitUI.SetActive(false);
                multiplyUnitUI.SetActive(true);
            }
            else
            {
                singleUnitUI.SetActive(false);
                multiplyUnitUI.SetActive(false);
            }
            clickStartOnUI = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(selectedUnits.Count > 0)
            {
                startPos = Input.mousePosition;
                UnitsAction();
            }
            

            
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //  if (selectedUnits.Count > 0)
        //  {
        //      startPos = Input.mousePosition;
        //      UnitAttack();
        //  }
        //}
    }
    void UnitAttack()
    {
        endPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        
    }
    
    void UnitsAction() {
        Vector2 target = mainCamera.ScreenToWorldPoint(startPos);
        Collider2D selectedObjects = Physics2D.OverlapCircle(mainCamera.ScreenToWorldPoint(startPos), 0.01f);
        endPos = Input.mousePosition;
        Debug.Log(target);
        if (selectedObjects == null || !selectedObjects.TryGetComponent(out ShipBase shipBase)){
            foreach (var obj in selectedUnits)
            {
                ////obj.GetComponent<ShipBase>().SettingTarget(null);
                ////obj.GetComponent<ShipBase>().Move(target);
            }
        }
        else
        {
            foreach (var obj in selectedUnits)
            {
                ////obj.GetComponent<ShipBase>().SettingTarget(selectedObjects.GetComponent<Unit>());
            }
        }

    }
    // 選取範圍內的物件
    void SelectUnits()
    {
        UnSelectUnits();

        Vector2 worldStart = mainCamera.ScreenToWorldPoint(startPos);
        Vector2 worldEnd = mainCamera.ScreenToWorldPoint(endPos);

        Collider2D[] selectedObjects = Physics2D.OverlapAreaAll(worldStart, worldEnd);

        foreach (var obj in selectedObjects)
        {
            // 如果物體是Trigger，跳過
            if (obj.isTrigger)
            {
                // 檢查物體的bounds是否完全在選取範圍內
                if (!IsObjectInSelectionBox(obj))
                    continue;
            }
            
            if (!obj.TryGetComponent(out ShipBase shipBase)) continue;
            if (!selectedUnits.Contains(obj.gameObject)) { selectedUnits.Add(obj.gameObject); }
            Debug.Log("選取到：" + obj.gameObject.name);
            // 這裡可以改變物件外觀，例如變色
            
        }
    }
    void UnSelectUnits() {
        foreach (var obj in selectedUnits) {
            
            //做變化
        }
        selectedUnits.Clear();
    }
    bool IsObjectInSelectionBox(Collider2D obj)
    {
        if(obj == null) return false;
        Vector2 worldStart = mainCamera.ScreenToWorldPoint(startPos);
        Vector2 worldEnd = mainCamera.ScreenToWorldPoint(endPos);

        // 取得選取範圍的最小和最大座標
        float minX = Mathf.Min(worldStart.x, worldEnd.x);
        float maxX = Mathf.Max(worldStart.x, worldEnd.x);
        float minY = Mathf.Min(worldStart.y, worldEnd.y);
        float maxY = Mathf.Max(worldStart.y, worldEnd.y);

        // 檢查物體的邊界是否完全在選取框內
        return obj.bounds.min.x >= minX && obj.bounds.max.x <= maxX &&
            obj.bounds.min.y >= minY && obj.bounds.max.y <= maxY;
    }
}

