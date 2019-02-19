using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public BatteryType batteryType;

    bool used;
    Light _light;

    // Use this for initialization
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            if (other.gameObject.layer == Layers.player.Index)
            {
                switch (batteryType)
                {
                    case BatteryType.Health:
                        var life = other.GetComponent<LifeObject>();
                        if (life.hp.CurrentValue < life.hp.MaxValue)
                        {
                            life.Heal(life.hp.MaxValue);
                            used = true;
                            SoundManager.instance.PlayFX("Use Battery");
                            _light.enabled = false;
                        }
                        break;
                    case BatteryType.Mana:
                        var mana = other.GetComponent<PlayerStats>();
                        if (mana.mp.CurrentValue < mana.mp.MaxValue)
                        {
                            mana.RecoverMp(mana.mp.MaxValue);
                            used = true;
                            SoundManager.instance.PlayFX("Use Battery");
                            _light.enabled = false;
                        }
                        break;
                }

                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}

public enum BatteryType
{
    Health,
    Mana
}

