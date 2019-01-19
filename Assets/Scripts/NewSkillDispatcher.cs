using UnityEngine;

public class NewSkillDispatcher : MonoBehaviour
{
    public SpellType type;
    public Color skillColor;
    public GameObject skillObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index)
        {
            GlobalEvent.Instance.Dispatch(new NewSkillEvent(type, skillColor, skillObj));
            Destroy(gameObject);
        }
    }

}
