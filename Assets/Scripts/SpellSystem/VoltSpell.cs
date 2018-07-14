using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoltSpell : MonoBehaviour, IUpdate
{
    public float speed; //Velocidad de movimientio
    public float power; //Daño que causa al impactar.

    public int durability; //Cuantos enemigos daña hasta destruirse.
    private float _decrementSize; //Cuanto se achica luego de impactar con un enemigo.

    public float radius; //Radio para buscar al siguiente objetivo.

    //ToDo: Change for Enemy.
    private HashSet<HitObject> _hitedEnemys;

    // Use this for initialization
    void Start()
    {
        _decrementSize = 1 - 1 / durability;

        _hitedEnemys = new HashSet<HitObject>();
        UpdateManager.instance.AddUpdate(this);
    }

    void IUpdate.Update() { transform.position += transform.forward * speed * Time.deltaTime; }

    private void OnTriggerEnter(Collider collision)
    {
        HitObject hitObject = collision.GetComponent<HitObject>();

        if (hitObject == null)
        {
            DestroySpell();
            return;
        }

        _hitedEnemys.Add(collision.GetComponent<HitObject>());

        var nearEnemy = Physics.OverlapSphere(transform.position, radius)
                                .Select(c => c.gameObject.GetComponent<HitObject>())
                                .Where(ho => ho != null)
                                .Where(ho => !_hitedEnemys.Contains(ho))
                                .OrderBy(ho => Vector3.Distance(transform.position, ho.gameObject.transform.position))
                                .ToList();

        if (!nearEnemy.Any() || durability <= 0)
        {
            DestroySpell();
        } else
        {
            _hitedEnemys.Add(nearEnemy.First());
            transform.forward = nearEnemy.First().transform.position - transform.position;
            durability--;

            transform.localScale *= _decrementSize;
        }
    }

    private void DestroySpell()
    {
        UpdateManager.instance.RemoveUpdate(this);
        Destroy(gameObject);
    }

}
