using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
  
    [SerializeField] private TextMeshProUGUI HealthText;
    [SerializeField] private TextMeshProUGUI ArmorText;
    [SerializeField] private TextMeshProUGUI AmmoText;
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider ArmorSlider;
    [SerializeField] private Slider StrafeSlider;
    [SerializeField] private Image CurrentWeaponImage;
    [SerializeField] private Image CurrentWeaponTarget;
    [SerializeField] private Image BloodVignett;


    private void OnEnable()
    {
        PlayerStats.UpdateArmorEventHandler += UpdateArmor;
        PlayerStats.UpdateHelthEventHandler += UpdateHealht;

        PlayerMove.UpdateStrafeEventHendler += UpdateStrafe;
        AmmoCounter.UpdateAmmoCountEventHandler += UpdateAmmoText;

        WeaponChanger.UpdateWeaponImageEventHandler += UpdateWeaponImage;
        BloodVignett.color = new Color(1, 0, 0, 0);
    }

    private void UpdateArmor(int armor)
    {
        ArmorText.text = armor.ToString();
        ArmorSlider.value = armor;
    }

    public void UpdateHealht(int health)
    {
        if (health<int.Parse(HealthText.text))
        {
            StartCoroutine(BloodEffect((health+0.1f)/100f));
        }
        HealthText.text = health.ToString();
        HealthSlider.value = health;
    }
    public void UpdateStrafe(float value)
    {
        StrafeSlider.value = value;
        
    }
    public void UpdateAmmoText(int value)
    {
        AmmoText.text = value.ToString();
    }
    public void UpdateWeaponImage(Sprite weapon, Sprite target)
    {
        if (weapon == null)
        {
            CurrentWeaponImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            CurrentWeaponImage.color = new Color(1, 1, 1, 1);
            CurrentWeaponImage.sprite = weapon;
        }
        if (target == null)
        {
            CurrentWeaponTarget.color = new Color(1, 1, 1, 0);
        }
        else
        {
            CurrentWeaponTarget.color = new Color(1, 1, 1, 1);
            CurrentWeaponTarget.sprite = target;
        }
    }


    private IEnumerator BloodEffect(float strange)
    {
        float t = 0;
        float tStrange = 1-strange;
        BloodVignett.color = new Color(1, 0, 0, 1 - strange);
        print("Strange def_" + strange + " Strange inv_" + (1 - strange) + "t max_" + (10 * (1 - strange)));
        while (true)
        {
            t += Time.deltaTime;
            if (t> 10*(1-strange))
            {
                BloodVignett.color = new Color(1, 0, 0, tStrange -= Time.deltaTime*strange);
                if (BloodVignett.color.a <= 0.1f)
                {
                    BloodVignett.color = new Color(1, 0, 0, 0);
                    yield break;
                }
            }
            
            yield return null;
        }
    }
}
