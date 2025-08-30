using UnityEngine.SceneManagement;
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
        if (TotalScore < 0) BlueSideBar.GetComponent<Image>().fillAmount -= 0.05f*Time.deltaTime;
        else if (TotalScore > 0) RedSideBar.GetComponent<Image>().fillAmount -= 0.05f*Time.deltaTime;
        new WaitForSeconds(5);
        if (BlueSideBar.GetComponent<Image>().fillAmount <= 0.01f)
        {
            SceneManager.LoadScene("Lose");
        }
        else if (RedSideBar.GetComponent<Image>().fillAmount <= 0.01f) SceneManager.LoadScene("Victory");

        TotalScore = 0;
    }
}
