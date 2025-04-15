using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_TruckHPSlider : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    float maxHpBar_Gauge;
    [SerializeField] float currentHpBar_Gauge = 100;
    private Coroutine smoothRoutine;
    void Start()
    {
        hpBar = GetComponent<Slider>();
        UpdateHPBar();
    }

    public void UpdateHP(float health)
    {
        currentHpBar_Gauge = health;
        UpdateHPBar();
    }
    void UpdateHPBar()
    {
        hpBar.value = currentHpBar_Gauge;
    }
    public void SetMax(float max)
    {
        maxHpBar_Gauge = max;
        currentHpBar_Gauge = maxHpBar_Gauge;

        if (hpBar == null) hpBar = GetComponent<Slider>();
        hpBar.maxValue = maxHpBar_Gauge;
        hpBar.value = currentHpBar_Gauge;
    }
}