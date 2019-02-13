using UnityEngine;

public class ShieldSpell : MonoBehaviour, ILevel
{
    private GameObject player;
    private LifeObject playerLifeObject;

    private float initialDuration = 5;
    private float duration;

    void Start()
    {
        player = GameObject.Find("PREF_Player");
        playerLifeObject = player.GetComponent<LifeObject>();

        playerLifeObject.IsInvulnerable = true;
        transform.parent = player.transform;

        transform.localPosition = new Vector3(0, 1, 0);

        FrameUtil.AfterDelay(duration, () => Destroy(this.gameObject));
    }

    public void SetLevel(int level, float percentage)
    {
        duration = initialDuration + initialDuration * level * percentage;
    }

    private void OnDestroy()
    {
        playerLifeObject.IsInvulnerable = false;
    }
}
