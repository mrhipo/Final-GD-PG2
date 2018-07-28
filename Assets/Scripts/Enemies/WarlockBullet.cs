using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockBullet : MonoBehaviour, IUpdate
{
	public float speed;
	public float lifeTime = 10;

	private float _damage;

	public void Initialize(Vector3 initPosition, Vector3 direction, float dmg)
	{
		_damage = dmg;
		transform.position = initPosition;
		transform.forward = direction - transform.position;
		
		UpdateManager.instance.AddUpdate(this);
		
		Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void IUpdate.Update ()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		var hitObject = other.gameObject.GetComponent<HitObject>();
		
		if (hitObject != null)
		{
			hitObject.OnTakeDamage(new Damage(gameObject, _damage));
		}
		
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		UpdateManager.instance.RemoveUpdate(this);
	}
}
