using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class FinishPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private void Start()
    {
        SurvivalRunManager.Instance.OnGameFinishedEvent += SurvivalRunManager_OnGameFinished;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        SurvivalRunManager.Instance.OnGameFinishedEvent -= SurvivalRunManager_OnGameFinished;
    }
    private void SurvivalRunManager_OnGameFinished(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        scoreText.text = PointManager.Instance.GetGamePoint().ToString();
    }
    public void OnClick_PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClick_Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
