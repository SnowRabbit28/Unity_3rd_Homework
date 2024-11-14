using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Text coin = null;
    public Text distance = null;
    public GameObject guiGameOver = null;

    private void Start()
    {
        Manager.instance.coins += UpdateCoinCount;
        Manager.instance.distance += UpdateDistanceCount;
        Manager.instance.gameOver += GameOver;
    }

    public void UpdateCoinCount(int value)
    {
        coin.text = value.ToString();
    }

    public void UpdateDistanceCount(int value)
    {
        distance.text = value.ToString();
    }

    void GameOver()
    {
        guiGameOver.SetActive(true);
    }

    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        Manager.instance.coins -= UpdateCoinCount;
        Manager.instance.distance -= UpdateDistanceCount;
        Manager.instance.gameOver -= GameOver;
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
