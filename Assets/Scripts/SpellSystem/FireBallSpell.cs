using UnityEngine;

public class FireBallSpell : MonoBehaviour, IUpdate
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
        var nearEnemys = Physics.OverlapSphere(transform.position, explosionRadius, 1 << LayerMask.NameToLayer("Enemy"));

        foreach (var item in nearEnemys)
        {
            Debug.Log("Enemy -> DoDamage()");
        }

        //ToDo.
        //Agarrar cada enemigo y hacerles un DoDamage.
        //Destruirme.

        Destroy(gameObject);
    }
}
