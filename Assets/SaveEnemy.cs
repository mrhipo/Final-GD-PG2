using UnityEngine;

public class SaveEnemy : SaveObject
{
    EnemyStats lo;

    public void Awake()
    {
        lo = GetComponent<EnemyStats>();
    }

    public override void Load()
    {
        var dead = GetValue<BooleanMemento>();
        if (dead != null && dead.boolean)
        {
            gameObject.SetActive(false);
            lo.lifeObject.hp.crrValue = 0;
        }
    }

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(new BooleanMemento { boolean = lo.lifeObject.IsDead }));
    }

}

public class BooleanMemento
{
    public bool boolean;

    public BooleanMemento() { }
    public BooleanMemento(bool bbb)
    {
        boolean = bbb;
    }
}

