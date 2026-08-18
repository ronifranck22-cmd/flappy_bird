using UnityEngine;
  using TMPro;

  public class GameOverUI : MonoBehaviour
  {
      public TextMeshProUGUI finalScoreText;

      void OnEnable()
      {
          if (finalScoreText != null)
          {
              finalScoreText.text = ScoreManager.score.ToString();
          }
      }
  }