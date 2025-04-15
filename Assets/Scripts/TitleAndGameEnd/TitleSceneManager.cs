using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] Canvas _tutorialCanvas;
    [SerializeField] GameObject[] _tutorials;
    int _tutoIndex = 0;
    public void StartButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ToggleTutorialCanvas(bool state)
    {
        _tutorialCanvas.enabled = state;
    }

    public void PrevTutoPanel()
    {
        if(_tutoIndex != 0){
            _tutorials[_tutoIndex].SetActive(false);
            _tutoIndex -= 1;
            _tutorials[_tutoIndex].SetActive(true);
        }
    }
    public void NextTutoPanel()
    {
        if (_tutoIndex != _tutorials.Length - 1)
        {
            _tutorials[_tutoIndex].SetActive(false);
            _tutoIndex += 1;
            _tutorials[_tutoIndex].SetActive(true);
        }
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
