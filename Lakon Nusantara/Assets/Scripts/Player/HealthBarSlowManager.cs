using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlowManager : MonoBehaviour
{
    public PlayerController player;
	public Slider healthSliderSlow;
	public Gradient gradient;
	public Image fill;

    float currentVelocity = 0;
    // Update is called once per frame
    void Update()
    {
        float currentSliderSlowValue = Mathf.SmoothDamp(healthSliderSlow.value, player.currentHealth, ref currentVelocity, 100 * Time.deltaTime);
		healthSliderSlow.value = currentSliderSlowValue;
    }

    public void SetMaxHealth(int health)
	{
		healthSliderSlow.maxValue = health;
		healthSliderSlow.value = health;

		fill.color = gradient.Evaluate(1f);
	}
}
