using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public List<GameObject> ControPointlist = new List<GameObject>();
    public GameObject RedSideBar;
    public GameObject BlueSideBar;
    private Coroutine GetScoreRoutine;
    public int TotalScore=0;
    void Start()
    {
        BlueSideBar.GetComponent<Image>().fillAmount = 1;
        RedSideBar.GetComponent<Image>().fillAmount = 1;
    }
    void Update()
    {
        foreach (var item in ControPointlist)
        {
            TotalScore += item.GetComponent<BattlePoint>().status;
        }
        if (TotalScore < 0) BlueSideBar.GetComponent<Image>().fillAmount -= 0.1f*Time.deltaTime;
        else if (TotalScore > 0) RedSideBar.GetComponent<Image>().fillAmount -= 0.1f*Time.deltaTime;
        new WaitForSeconds(5);
        TotalScore = 0;
    }
    private IEnumerator GetScore()
    {
        if (TotalScore < 0) BlueSideBar.GetComponent<Image>().fillAmount -= 0.1f;
        else if (TotalScore > 0) RedSideBar.GetComponent<Image>().fillAmount -= 0.1f;
        yield return new WaitForSeconds(5);

    }
}
