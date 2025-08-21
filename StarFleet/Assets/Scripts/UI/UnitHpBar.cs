using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpBar : MonoBehaviour
{
	[SerializeField]
	Color StartColor;
	[SerializeField]
	Color EndColor;

	Image HPImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		HPImage = GetComponent<Image>();
    }
	public void SetHPBar(float nowHP,float maxHP) {
		HPImage.fillAmount = nowHP / maxHP;
		HPImage.color = Color.Lerp(StartColor, EndColor, 1f-HPImage.fillAmount);
	}
}
