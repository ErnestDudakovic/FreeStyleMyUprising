using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    [Header("Game Over")]
    public GameObject gameOverScreen;
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;

    [Header("Win Screen")]
    public GameObject winScreen;
    public Button winMainMenuButton;
    public Button winQuitButton;


    [Header("Pause")]
    public GameObject pauseScreen;

   private Player player;

    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        winMainMenuButton.onClick.AddListener(GoToMainMenu);
        winQuitButton.onClick.AddListener(QuitGame);

        

        player = FindAnyObjectByType<Player>();
    }

    private void Update()
{
    if(Input.GetKeyDown(KeyCode.Escape)){
        if(pauseScreen.activeInHierarchy)
            PauseGame(false);
        else
            PauseGame(true);
    }
}
    #region Game Over

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.25f; // Pause the game
    }

    void RestartGame()
    {

        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Make sure you have a scene named "MainMenu"
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void ShowWinScreen()
    {
        if(winScreen != null)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0.30f;
        }
    }
#endregion

    #region Pause
    public void PauseGame(bool status){
        pauseScreen.SetActive(status);

    if(status)
        Time.timeScale = 0;
    else
        Time.timeScale = 1;

    }

    public void SoundVolume()
    {

    }

    public void MusicVolume()
    {
        
    }
    #endregion
}


