using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReloadAmmoHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bulletCountText;
    [SerializeField] Slider slider;
    [SerializeField] Image healthBarImage;
    [SerializeField] Gradient gradientColor;

    public static ReloadAmmoHandler ammoHandler;

    private void Awake()
    {
        ammoHandler = this;
    }
    private void Start()
    {
        UpdateBulletCount();
    }
    public void UpdateBulletCount()
    {
        bulletCountText.text = AimGun.aimGun.bulletCount.ToString() + "/" + AimGun.aimGun.remainingBulletCount.ToString();
        slider.value = AimGun.aimGun.bulletCount;
        healthBarImage.color = gradientColor.Evaluate(slider.value / 20);
    }
    public void Reload()
    {
        AimGun.aimGun.Reload();
        UpdateBulletCount();
    }
}
