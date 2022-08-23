using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarSlowManager : MonoBehaviour
{
    public PlayerController player;
	public Slider staminaSliderSlow;
	public Gradient gradient;
	public Image fill;

    float currentVelocity = 0;
    // Update is called once per frame
    void Update()
    {
        float currentSliderSlowValue = Mathf.SmoothDamp(staminaSliderSlow.value, player.currentStamina, ref currentVelocity, 100 * Time.deltaTime);
		staminaSliderSlow.value = currentSliderSlowValue;
    }

    public void SetMaxStamina(int stamina)
	{
		staminaSliderSlow.maxValue = stamina;
		staminaSliderSlow.value = stamina;

		fill.color = gradient.Evaluate(1f);
	}
}
