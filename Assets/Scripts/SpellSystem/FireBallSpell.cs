using System.Linq;
using UnityEngine;

public class FireBallSpell : MonoBehaviour, IUpdate
{
    public float power;
    public float explosionRadius;
    public float speed;

    public GameObject fireExplosion;

    // Use this for initialization
    void Start()
    {
        UpdateManager.instance.AddUpdate(this);
    }

    void IUpdate.Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateManager.instance.RemoveUpdate(this);
        Exploit();
    }

    private void Exploit()
    {
        GameObject explosion = Instantiate(fireExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);

        var nearEnemys = Physics.OverlapSphere(transform.position, explosionRadius)
                              .Select(c => c.gameObject.GetComponent<HitObject>())
                              .Where(ho => ho != null);

        foreach (HitObject hitObject in nearEnemys)
            hitObject.OnTakeDamage(new Damage(gameObject, power));

        Destroy(gameObject);
    }
}
