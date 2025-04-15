using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndCombat : MonoBehaviour
{
    int _fuelReward;
    [SerializeField] TextMeshProUGUI _fuelText;
    Button _continueButton;

    void Start()
    {
        _continueButton = GetComponentInChildren<Button>();
        _continueButton.onClick.AddListener(CloseCanvas);
    }

    public void UpdateCanvas()
    {
        _fuelReward = Random.Range(5, 11);
        _fuelText.text = $"+{_fuelReward}";
    }
    
    void CloseCanvas()
    {
        GameManager.UI.ToggleEndCombatCanvas(false);
        GameManager.Truck.AddFuel(_fuelReward);
    }
}
