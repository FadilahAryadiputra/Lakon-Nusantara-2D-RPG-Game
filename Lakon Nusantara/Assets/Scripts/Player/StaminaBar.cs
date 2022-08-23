using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public PlayerController player;
	public Slider staminaSlider;
	public Slider staminaSliderSlow;
	public Gradient gradient;
	public Image fill;

    float currentVelocity = 0;

    void Update()
	{
		float currentSliderValue = Mathf.SmoothDamp(staminaSlider.value, player.currentStamina, ref currentVelocity, 5 * Time.deltaTime);
		staminaSlider.value = currentSliderValue;
	}


	public void SetMaxStamina(int stamina)
	{
		staminaSlider.maxValue = stamina;
		staminaSlider.value = stamina;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetStamina(int stamina)
	{
		staminaSlider.value = stamina;

		fill.color = gradient.Evaluate(staminaSlider.normalizedValue);
	}
}
