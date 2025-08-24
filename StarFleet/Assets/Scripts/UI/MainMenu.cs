using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Gotogame(string gamename)
    {
        SceneManager.LoadScene(gamename);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
