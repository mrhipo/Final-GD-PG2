using System;
using UnityEngine;

[System.Serializable]
public class Spell : IUpdate
{
    public string name;
    public float coolDown;
    public float mpCost;
    public bool CanUseSpell { get; private set; }

    public GameObject spellPrefab;

    public bool IsUnlocked { get; private set; }

    //private WaitForSeconds coolDownWait;
    private float timer;

    public void Init()
    {
        CanUseSpell = true;
        IsUnlocked = false;
        GlobalEvent.Instance.AddEventHandler<NewSkillEvent>(OnNewSkill);
    }

    private void OnNewSkill(NewSkillEvent newSkillEvent)
    {
        if (newSkillEvent.skillName == name)
            IsUnlocked = true;
    }

    public void UseSpell(Vector3 position)
    {
        GameObject spell = UnityEngine.Object.Instantiate(spellPrefab, position, Quaternion.identity);

        spell.transform.forward = GetHitPoint() - position;

        CanUseSpell = false;
        timer = 0;
        UpdateManager.instance.AddUpdate(this);

    }

    public Vector3 p;

    private Vector3 GetHitPoint()
    {
        Vector3 hitPoint;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500f, Layers.shootable.Mask))
            hitPoint = hit.point;
        else
            hitPoint = ray.GetPoint(100);

        p = hitPoint;

        return hitPoint;
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
