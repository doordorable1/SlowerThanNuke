using UnityEngine;

public class CrewController : MonoBehaviour
{
    public Crew SelectedCrew { get; private set; }
    
    public static CrewController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        GameManager.Input.selectCrewAction += Select;
        GameManager.Input.deselectCrewAction += Deselect;
    }

    // 플레이어 좌클릭시 선택
    // 선택 시 하이라이트 스프라이트 활성화
    void Select(GameObject clickedCrew)
    {
        if (SelectedCrew != null)
        {
            Deselect();
        }
        SelectedCrew = clickedCrew.GetComponent<Crew>();
        float[] additionalStats = SelectedCrew.GetAdditionalStats();
        GameManager.UI.UpdateCrewStatCanvas(SelectedCrew.CrewInfo, additionalStats);
        GameManager.UI.ToggleCrewStatCanvas(true);
        SelectedCrew.ToggleGlow(true);
    }

    public void Deselect()
    {
        GameManager.UI.ToggleCrewStatCanvas(false);
        SelectedCrew.ToggleGlow(false);
        SelectedCrew = null;
    }
}
