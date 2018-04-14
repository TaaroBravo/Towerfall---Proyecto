using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {

	PlayerController player = null;
	public Image greenLife;

	void Start () {
		player = FindMyPlayer (transform);
		greenLife = GetComponent<Image>();
	}

	public void UpdateMyLife(float life)
	{
		greenLife.fillAmount -= life;
	}

	PlayerController FindMyPlayer(Transform t)
	{
		if (t.GetComponent<PlayerController> ())
			return t.GetComponent<PlayerController>();
		return FindMyPlayer (t.parent);
	}
}
