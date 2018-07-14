using UnityEngine;

[System.Serializable]
public class Spell : IUpdate
{
    public string name;
    public float coolDown;
    public float mpCost;
    public bool CanUseSpell { get; private set; }

    public GameObject spellPrefab;

    //private WaitForSeconds coolDownWait;
    private float timer;

    public Spell()
    {
        CanUseSpell = true;
    }

    public void UseSpell(Vector3 position, Vector3 direction)
    {
        GameObject spell = Object.Instantiate(spellPrefab, position, Quaternion.identity);

        spell.transform.forward = direction;
        CanUseSpell = false;

        timer = 0;
        UpdateManager.instance.AddUpdate(this);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= coolDown)
        {
            CanUseSpell = true;
            UpdateManager.instance.RemoveUpdate(this);
        }

    }
}
