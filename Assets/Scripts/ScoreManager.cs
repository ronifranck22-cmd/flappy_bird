using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    private TextMeshProUGUI scoreText;
    private bool counted = false; // הגנה כדי שלא יספור פעמיים

    void Start()
    {
        GameObject textObj = GameObject.Find("Text (TMP)");
        if (textObj != null)
        {
            scoreText = textObj.GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // בודקים שזו הציפור ושהנקודה עוד לא נספרה
        if (collision.CompareTag("Bird") && !counted)
        {
            score++;
            counted = true; // נועל את הספירה כדי שלא יתקע
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }
    }
}