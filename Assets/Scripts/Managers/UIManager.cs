using UnityEngine;

public class UIManager
{
    Canvas _UI_CrewStatusCanvas;
    Canvas _UI_CombatCanvas;

    public UI_Canvas UI_Canvas { get { return uiCanvas; } }

    UI_Canvas uiCanvas;

    public void Init()
    {
        _UI_CrewStatusCanvas = GameObject.FindAnyObjectByType<UI_CrewStatusCanvas>().GetComponent<Canvas>();
        uiCanvas = GameObject.FindAnyObjectByType<UI_Canvas>();
        _UI_CombatCanvas = GameObject.FindAnyObjectByType<UI_EndCombat>().GetComponent<Canvas>();
    }

    public void ToggleCrewStatCanvas(bool state)
    {
        _UI_CrewStatusCanvas.enabled = state;
    }

    public void UpdateCrewStatCanvas(CrewSO crewInfo, float[] additionalStats)
    {
        UI_CrewStatusCanvas crewStatCanvas = _UI_CrewStatusCanvas.GetComponent<UI_CrewStatusCanvas>();
        crewStatCanvas.SetInfo(crewInfo, additionalStats);
    }

    public void ToggleEndCombatCanvas(bool state)
    {
        if (state)
        {
            _UI_CombatCanvas.enabled = true;
            _UI_CombatCanvas.GetComponent<UI_EndCombat>().UpdateCanvas();
        } else
        {
            _UI_CombatCanvas.enabled = false;
        }
    }
}
