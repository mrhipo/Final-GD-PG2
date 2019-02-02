using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoltSpell : MonoBehaviour, IUpdate , ILevel
{
    public float speed; //Velocidad de movimientio
    public float power; //Daño que causa al impactar.

    public int durability; //Cuantos enemigos daña hasta destruirse.
    public float _decrementSize; //Cuanto se achica luego de impactar con un enemigo.

    public float radius; //Radio para buscar al siguiente objetivo.

    //ToDo: Change for Enemy.
    private HashSet<HitObject> _hitedEnemys;

    private float initialPower;
    private int initialDurability;

    private int kills = 0;
    // Use this for initialization
    void Start()
    {
        _decrementSize = 1 - (1 / (float)durability);

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
        hitObject.OnTakeDamage(new Damage(gameObject, power));
        transform.localScale *= _decrementSize;
        speed *= 1.25f;

        var nearEnemy = Physics.OverlapSphere(transform.position, radius)
                                .Select(c => c.gameObject.GetComponent<HitObject>())
                                .Where(ho => ho != null)
                                .Where(ho => !_hitedEnemys.Contains(ho))
                                .OrderBy(ho => Vector3.Distance(transform.position, ho.gameObject.transform.position));

        
        
        //Achievement
        var life = hitObject as LifeObject;
        if(life != null && life.hp.CurrentValue == 0)
            kills++;
        //End Achievement
        
        if (!nearEnemy.Any() || durability <= 0)
        {
            DestroySpell();
            GlobalEvent.Instance.Dispatch(new VoltKillEvent{killed = kills});
        } else
        {
            transform.forward = nearEnemy.First().transform.position - transform.position;
            durability--;
        }
    }

    private void DestroySpell()
    {
        UpdateManager.instance.RemoveUpdate(this);
        Destroy(gameObject);
    }

    public void SetLevel(int level, float percentage)
    {
        initialPower = power;
        initialDurability = durability;

        power = initialPower + initialPower * level * percentage;
        durability = initialDurability + level;
    }
}
