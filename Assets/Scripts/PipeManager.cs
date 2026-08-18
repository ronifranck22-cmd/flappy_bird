using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject pipePairPrefab; // לכאן נגרור את ה-PipePair המוכן
    public float managerRate = 2f;    // כל כמה שניות נוצר זוג חדש
    public float heightRange = 2f;  // טווח הגובה האקראי

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= managerRate)
        {
            SpawnPipePair();
            timer = 0;
        }
    }

    void SpawnPipePair()
    {
        // יצירת גובה אקראי לזוג
        float randomY = Random.Range(-heightRange, heightRange);
        
        // יצירת הזוג מחוץ למסך מימין (X=8)
        Vector3 spawnPosition = new Vector3(8f, randomY+1.5f, 0);
        Instantiate(pipePairPrefab, spawnPosition, Quaternion.identity);
    }
} 