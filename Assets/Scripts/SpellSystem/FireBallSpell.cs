using System.Linq;
using UnityEngine;

public class FireBallSpell : MonoBehaviour, IUpdate , ILevel
{
    public float power;
    public float explosionRadius;
    public float speed;

    public GameObject fireExplosion;

    float initialPower;
    float initialExplosionRadius;

    // Use this for initialization
    private void Start()
    {
       UpdateManager.instance.AddUpdate(this);
    }

    void IUpdate.Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        UpdateManager.instance.RemoveUpdate(this);
        Exploit();
    }

    private void Exploit()
    {
        var explosion = Instantiate(fireExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);

        var nearEnemys = Physics.OverlapSphere(transform.position, explosionRadius)
                              .Select(c => c.gameObject.GetComponent<HitObject>())
                              .Where(ho => ho != null);
        
       
        foreach (var hitObject in nearEnemys)
            hitObject.OnTakeDamage(new Damage(gameObject, power));
        
        //Achievement
        var kills = 0;
        foreach (var hitObject in nearEnemys)
        {
            var life = hitObject as LifeObject;
            if(life != null && life.hp.CurrentValue == 0)
                kills++;
        }
        //End Achievement

        GlobalEvent.Instance.Dispatch(new FireBallKillEvent{ killed = kills});
        
        Destroy(gameObject);
    }

    public void SetLevel(int level, float percentage)
    {
        initialPower = power;
        initialExplosionRadius = explosionRadius;

        power = initialPower + initialPower * level * percentage;
        explosionRadius = initialExplosionRadius + initialExplosionRadius * level * percentage;

    }
}
