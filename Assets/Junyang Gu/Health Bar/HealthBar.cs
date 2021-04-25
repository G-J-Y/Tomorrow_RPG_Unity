using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;

    // change health color
	//public Gradient gradient;
	//public Image fill;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		//fill.color = gradient.Evaluate(1f);
	}

}
