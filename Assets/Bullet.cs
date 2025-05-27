using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Ispiši u konzolu s kim smo se sudarili
        Debug.Log($"[Bullet] Sudar s {collision.gameObject.name} (tag = {collision.gameObject.tag})");

        // Ako je objekt označen tagom "Target"
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("[Bullet] Tag je Target → uništavam metu i metak");
            Destroy(collision.gameObject); // uništi metu
            Destroy(gameObject);           // uništi i metak
        }
        else
        {
            // Opcionalno: uništi metak i kad udari u nešto drugo
            Destroy(gameObject, 2f);
        }
    }
}
