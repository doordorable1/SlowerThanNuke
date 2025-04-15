using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    public void ReturnToTitleClick()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
