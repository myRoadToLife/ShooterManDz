using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables

    [SerializeField] private int _damage;

    #endregion

    #region Unity lifecycle

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    #endregion
}
