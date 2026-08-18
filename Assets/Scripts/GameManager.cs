using UnityEngine;
using UnityEngine.SceneManagement;

  public class GameManager : MonoBehaviour
  {
      public void RestartGame()
      {
          ScoreManager.score = 0;
          Time.timeScale = 1f;
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
  }