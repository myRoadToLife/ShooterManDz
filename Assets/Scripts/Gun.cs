using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _holderPoint;

    [SerializeField] private float _force;
    private float _timeToDestroy = 3f;

    public void Shot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _holderPoint);

        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);

        Destroy(bullet.gameObject, _timeToDestroy);
    }
}
