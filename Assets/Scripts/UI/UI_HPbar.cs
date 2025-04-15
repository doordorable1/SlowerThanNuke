using UnityEngine;
using UnityEngine.UI;

public class UI_HPbar : MonoBehaviour
{
    [SerializeField] Image hpBar;

    float maxHpBar_Gauge;
    [SerializeField] float currentHpBar_Gauge = 100;
    void Start()
    {
        UpdateHPBar();
    }

    public void UpdateHP(float health)
    {
        currentHpBar_Gauge = health;
        UpdateHPBar();
    }
    void UpdateHPBar()
    {
        hpBar.fillAmount = (float)currentHpBar_Gauge / maxHpBar_Gauge;
    }

    public void SetMax(float max)
    {
        maxHpBar_Gauge = max;
        currentHpBar_Gauge = maxHpBar_Gauge;
    }
}