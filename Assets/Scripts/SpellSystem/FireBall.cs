using UnityEngine;

public class FireBall : MonoBehaviour, IUpdate
{
    public float power;
    public float explosionRadius;
    public float speed;

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
        var nearEnemys = Physics.OverlapSphere(transform.position, explosionRadius);
        //ToDo.
        //Agarrar cada enemigo y hacerles un DoDamage.
    }
}
