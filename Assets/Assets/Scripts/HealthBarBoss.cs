using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{

	public Text HealthText;
	private Image healthBar;
	public int HealthCurrent;
	public int HealthMax;
	// Use this for initialization
	void Start()
	{
		HealthMax = Boss.startheart;
		healthBar = GetComponent<Image>();
		HealthCurrent = Player.startheart;
	}

	// Update is called once per frame
	void Update()
	{
		HealthCurrent = Boss.heart;
		if (HealthCurrent > HealthMax) HealthMax = HealthCurrent;
		healthBar.fillAmount = (float)HealthCurrent / HealthMax;
		HealthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
	}
}
