using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float coolDown;
    public float mpCost;
    public bool canUseSpell;

    public GameObject spellPrefab;

    private WaitForSeconds coolDownWait;

    private void Start()
    {
        coolDownWait = new WaitForSeconds(coolDown);
    }

    public void UseSpell()
    {
        GameObject spell = Instantiate(spellPrefab);
        canUseSpell = false;

        StartCoroutine(CoolDownTimer());
    }

    private IEnumerator CoolDownTimer()
    {
        yield return coolDownWait;
        canUseSpell = true;
    }
}
