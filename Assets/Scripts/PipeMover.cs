using UnityEngine;

public class PipeMover : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        // מחיקת הצינור כשהוא יוצא מהמסך כדי לא להעמיס על המחשב
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}