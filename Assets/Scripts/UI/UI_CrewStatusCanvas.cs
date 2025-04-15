using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CrewStatusCanvas : MonoBehaviour
{
    [SerializeField] Image CrewIllustration;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI CrewInfo;

    [SerializeField] RectTransform DefaultAttackSpeed;
    [SerializeField] RectTransform AdditionalAttackSpeed;

    [SerializeField] RectTransform DefaultRepairSpeed;
    [SerializeField] RectTransform AdditionalRepairSpeed;

    [SerializeField] RectTransform DefaultHealSpeed;
    [SerializeField] RectTransform AdditionalHealSpeed;

    [SerializeField] RectTransform DefaultAvoidanceSpeed;
    [SerializeField] RectTransform AdditionalAvoidanceSpeed;

    [SerializeField] RectTransform DefaultMoveSpeed;
   
    [SerializeField] RectTransform DefaultEvilRate;
    
    float defaultSize = 42;
    float defaultFullSize = 84;
    //최댓값에 따라 수정 필요
    public void SetInfo(CrewSO crewInfo, float[] additionalStats)
    {
        CrewIllustration.sprite = crewInfo.Illustration;
        Name.text = crewInfo.Name;
        CrewInfo.text = crewInfo.Description;

        DefaultAttackSpeed.sizeDelta = new Vector2(crewInfo.AttackSpeed/6 * defaultSize, DefaultAttackSpeed.sizeDelta.y);
        AdditionalAttackSpeed.sizeDelta = new Vector2(additionalStats[0]/6 * defaultSize, AdditionalAttackSpeed.sizeDelta.y);

        DefaultRepairSpeed.sizeDelta = new Vector2(crewInfo.RepairSpeed / 6 * defaultSize, DefaultRepairSpeed.sizeDelta.y);
        AdditionalRepairSpeed.sizeDelta = new Vector2(additionalStats[1]/6 * defaultSize, AdditionalRepairSpeed.sizeDelta.y);

        DefaultHealSpeed.sizeDelta = new Vector2(crewInfo.HealSpeed / 6 * defaultSize, DefaultHealSpeed.sizeDelta.y);
        AdditionalHealSpeed.sizeDelta = new Vector2(additionalStats[2] / 6 * defaultSize, AdditionalHealSpeed.sizeDelta.y);

        DefaultAvoidanceSpeed.sizeDelta = new Vector2(crewInfo.Avoidance / 6 * defaultSize, DefaultAvoidanceSpeed.sizeDelta.y);
        AdditionalAvoidanceSpeed.sizeDelta = new Vector2(additionalStats[3] / 6 * defaultSize, AdditionalAvoidanceSpeed.sizeDelta.y);

        DefaultMoveSpeed.sizeDelta = new Vector2(crewInfo.MoveSpeed / 6 * defaultFullSize, DefaultMoveSpeed.sizeDelta.y);

        DefaultEvilRate.sizeDelta = new Vector2(crewInfo.EvilRate / 100 * defaultFullSize, DefaultEvilRate.sizeDelta.y);
    }
} 
