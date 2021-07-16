using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {

	public Text HealthText;
	private Image healthBar;
	public int HealthCurrent;
	public int HealthMax;
	// Use this for initialization
	void Start ()
	{
		HealthMax = Player.startheart;
		healthBar = GetComponent<Image>();
		HealthCurrent = Player.startheart;
	}
	
	// Update is called once per frame
	void Update () 
	{
		HealthCurrent = Player.heart;
		if (HealthCurrent > HealthMax) HealthMax = HealthCurrent;
		healthBar.fillAmount = (float) HealthCurrent / HealthMax;
		HealthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
	}
}
