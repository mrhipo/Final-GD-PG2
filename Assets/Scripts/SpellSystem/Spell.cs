using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Spell : IUpdate
{
    public SpellType type;
    public float coolDown;
    public float mpCost;
    public bool CanUseSpell { get; private set; }

    public GameObject spellPrefab;

    public Image uiSprite;

    public bool IsBlocked { get; private set; }

    //private WaitForSeconds coolDownWait;
    private float timer;

    public void Init()
    {
        CanUseSpell = true;
        IsBlocked = true;
        GlobalEvent.Instance.AddEventHandler<NewSkillEvent>(OnNewSkill);
    }

    private void OnNewSkill(NewSkillEvent newSkillEvent)
    {
        if (newSkillEvent.type == type)
            IsBlocked = false;
    }

    public void UseSpell(Vector3 position, int level)
    {
        GameObject spell = UnityEngine.Object.Instantiate(spellPrefab, position, Quaternion.identity);
        spell.transform.forward = GetHitPoint() - position;

        spell.GetComponent<ILevel>().SetLevel(level, 1.1f);

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
        uiSprite.fillAmount = timer / coolDown;

        if (timer >= coolDown)
        {
            CanUseSpell = true;
            UpdateManager.instance.RemoveUpdate(this);
        }

    }

}
