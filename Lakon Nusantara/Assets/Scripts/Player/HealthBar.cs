using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public PlayerController player;
	public Slider healthSlider;
	public Slider healthSliderSlow;
	public Gradient gradient;
	public Image fill;

	// private float targetHealth;
	// private float timeScale = 0;
	// public bool lerpingHealth = false;

	float currentVelocity = 0;

	void Update()
	{
		float currentSliderValue = Mathf.SmoothDamp(healthSlider.value, player.currentHealth, ref currentVelocity, 5 * Time.deltaTime);
		healthSlider.value = currentSliderValue;
	}


	public void SetMaxHealth(int health)
	{
		healthSlider.maxValue = health;
		healthSlider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
		healthSlider.value = health;

		fill.color = gradient.Evaluate(healthSlider.normalizedValue);
	}

	// public void SetHealthLerp(float health)
	// {
	// 	targetHealth = health;
	// 	timeScale = 0;
	// 	if(!lerpingHealth){
	// 		StartCoroutine(LerpHealth());
	// 	}

	// 	fill.color = gradient.Evaluate(healthSlider.normalizedValue);
	// }

	// private IEnumerator LerpHealth()
	// {
	// 	yield return new WaitForSeconds(0);
	// 	float speed = 2;
	// 	float startHealth = healthSlider.value;

	// 	lerpingHealth = true;
		
	// 	while(timeScale < 1)
	// 	{
	// 		timeScale += Time.deltaTime * speed;
	// 		healthSlider.value = Mathf.Lerp(startHealth, targetHealth, timeScale);
	// 	}
	// 	lerpingHealth = false;
	// }
}
