using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void OnClick_PlayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
