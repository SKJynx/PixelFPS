using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    AmmoStockpile ammoStockpile;

    [SerializeField]
    AudioClip fireSFX;
    [SerializeField]
    AudioClip reloadSFX;

    [SerializeField]
    string firingAnimation;
    [SerializeField]
    string muzzleAnimation;

    public ScriptableWeapons m_scriptableWeapon;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject playerCamera;
    [SerializeField]
    GameObject muzzleFlash;

    Animator anim;
    SpriteRenderer sr;
    AudioSource audioSource;

    [SerializeField]
    bool canInput = true;
    [SerializeField]
    bool canUseWeapon = true;
    [SerializeField]
    bool isReloading;

    [SerializeField]
    float canFireTimer;
    [SerializeField]
    float reloadTimer;

    [SerializeField]
    float bulletRange;

    int ammoToLoad;
    [SerializeField]
    int ammoPerShot;
    RaycastHit bulletHit;

    public enum AmmoType
    {
        Light,
        Magnum,
        Assault,
        Heavy,
        Sniper,
        Explosive,
        Shell,
        Energy,
        Grenade,
        None
    }
    public AmmoType ammoType;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.Find("PlayerCamera");
        GetAmmoType();
        ammoStockpile = player.GetComponent<AmmoStockpile>();
        anim = GetComponent<Animator>();
        sr.sprite = m_scriptableWeapon.weaponSprite;
    }

    void FixedUpdate()
    {
        if (canFireTimer >= -1)
        {

            canFireTimer -= 1;
        }

        if (reloadTimer >= -1)
        {
            reloadTimer -= 1;
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (reloadTimer > 0)
        {
            canUseWeapon = false;
        }
        else
        {
            canUseWeapon = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoStockpile.GetCurrentMag(ammoStockpile.ammoType) < m_scriptableWeapon.maxAmmo && canUseWeapon == true && ammoStockpile.GetStockpile(ammoStockpile.ammoType) > 0 && canFireTimer < 0)
        {
            if (m_scriptableWeapon.reloadType == ScriptableWeapons.ReloadType.Magazine)
            {
                ReloadWeapon();
            }
            else if (m_scriptableWeapon.reloadType == ScriptableWeapons.ReloadType.Single && isReloading == false)
            {
                StartCoroutine("ReloadSingle");
            }

        }

        // Checks if the script can run the FireWeapon(); method
        if (canInput == true && ammoStockpile.GetCurrentMag(ammoStockpile.ammoType) > 0 && canUseWeapon == true)
        {
            if (m_scriptableWeapon.automatic == false)
            {
                if (Input.GetMouseButtonDown(0) && canFireTimer <= 0)
                {
                    FireWeapon();
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && canFireTimer <= 0)
                {
                    FireWeapon();
                }
            }
        }
    }

    void GetAmmoType()
    {
        if (ammoType == AmmoType.Light)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Light;
        }
        if (ammoType == AmmoType.Magnum)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Magnum;
        }
        if (ammoType == AmmoType.Assault)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Assault;
        }
        if (ammoType == AmmoType.Heavy)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Heavy;
        }
        if (ammoType == AmmoType.Sniper)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Sniper;
        }
        if (ammoType == AmmoType.Explosive)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Explosive;
        }
        if (ammoType == AmmoType.Shell)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Shell;
        }
        if (ammoType == AmmoType.Energy)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Energy;
        }
        if (ammoType == AmmoType.Grenade)
        {
            player.GetComponent<AmmoStockpile>().ammoType = AmmoStockpile.AmmoType.Grenade;
        }

    }

    void FireWeapon()
    {
        isReloading = false;

        StopCoroutine("ReloadSingle");
        GetAmmoType();

        ammoType = (AmmoType)ammoStockpile.ammoType;

        ammoStockpile.Spend(ammoStockpile.ammoType, ammoPerShot);

        canFireTimer = m_scriptableWeapon.fireRate;

        audioSource.PlayOneShot(fireSFX);
        muzzleFlash.GetComponent<Animator>().Play(muzzleAnimation, -1, 0);
        anim.Play(firingAnimation, -1, 0);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red, 0.5f);

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out bulletHit, bulletRange))
        {
            if (bulletHit.rigidbody != null)
            {
                bulletHit.collider.GetComponent<HealthScript>().DamageDealt(m_scriptableWeapon.weaponDamage * m_scriptableWeapon.criticalMultiplier);
                print(bulletHit.collider);
            }
        }
    }

    void ReloadWeapon()
    {
        audioSource.PlayOneShot(reloadSFX);

        int ammoToReload = m_scriptableWeapon.maxAmmo - ammoStockpile.GetCurrentMag(ammoStockpile.ammoType);

        ammoStockpile.Reload(ammoStockpile.ammoType, ammoToReload);

        reloadTimer = m_scriptableWeapon.reloadTime;
    }

    IEnumerator ReloadSingle()
    {
        if (ammoStockpile.GetCurrentMag(ammoStockpile.ammoType) < m_scriptableWeapon.maxAmmo)
        {
            isReloading = true;

            audioSource.PlayOneShot(reloadSFX);

            reloadTimer = m_scriptableWeapon.reloadTime;
            ammoStockpile.Reload(ammoStockpile.ammoType, 1);
            yield return new WaitForSeconds(m_scriptableWeapon.reloadTime);
            isReloading = false;
            StartCoroutine("ReloadSingle");

        }
    }

}

