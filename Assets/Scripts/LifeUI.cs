using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {

	public Image greenLife;

	void Start () {
		greenLife = GetComponent<Image>();
	}

	public void UpdateMyLife(float life)
	{
		greenLife.fillAmount -= life/2;
	}
}
