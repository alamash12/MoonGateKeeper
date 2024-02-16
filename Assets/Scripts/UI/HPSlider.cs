using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;       // Slider UI 요소를 저장할 변수
    public Gradient gradient;   // HP 바의 색상을 변경하기 위한 Gradient 변수
    public Image fill;          // HP 바를 채울 이미지를 저장할 변수

    // HP 바의 최대 길이를 설정하는 메서드
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;            // Slider의 최댓값을 설정
        slider.value = maxHealth;               // 현재 HP를 최대 HP로 설정
        fill.color = gradient.Evaluate(1f);     // HP 바의 색상을 최대값 색상으로 설정
    }

    // 현재 HP를 설정하는 메서드
    public void SetHealth(int health)
    {
        slider.value = health;                                  // Slider의 값 설정
        fill.color = gradient.Evaluate(slider.normalizedValue); // HP 바의 색상을 현재 HP에 맞게 변경
    }
}

