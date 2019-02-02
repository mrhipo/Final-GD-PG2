using UnityEngine;

public class FreezeSpell : MonoBehaviour, IUpdate , ILevel
{
    public float power;
    public float freezeTime;
    public float speed;

    private float initialPower;
    private float initialFreezeTime;

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

        if (collision.gameObject.layer == Layers.enemies.Index)
        {
            HitObject ho = collision.gameObject.GetComponent<HitObject>();
            if (ho) ho.OnTakeDamage(new Damage(gameObject, power));

            //ToDo: Do Freeze.
            Freeze freeze = collision.gameObject.GetComponent<Freeze>();

            if (freeze)
                freeze.RestartTime();
            else
            {
                freeze = collision.gameObject.AddComponent<Freeze>();
                freeze.duration = freezeTime;
            }
        }

        Destroy(gameObject);
    }

    public void SetLevel(int level, float percentage)
    {

        initialPower = power;
        initialFreezeTime = freezeTime;

        power = initialPower + initialPower * level * percentage;
        freezeTime = initialFreezeTime + initialFreezeTime * level * percentage;
    }
}
