using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour
{
    public Sprite idlePistol;
    public Sprite shotPistol;
    public Sprite shotPistol2;

    public Sprite realoadPistol;
    public Sprite realoadPistol2;


    public float pistolDamage;
    public float pistolRange;

    AudioSource source;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;

    public int ammoAmount;
    public int ammoClip;

    public Text ammoText;

    private int ammoLeft;
    private int ammoClipLeft;

    private bool isReloading;
    private bool isShot;

    public GameObject bulletHole;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClip;
    }

    private void Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;

        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }

    private void FixedUpdate()
    {
        Vector2 bulletOffset = Random.insideUnitCircle * 5;
        Vector3 RandomTarget = new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(RandomTarget);
        RaycastHit hit;

        if (isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");

            if (Physics.Raycast(ray, out hit, pistolRange))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.GetComponent<EnemyStates>().patrolState ||
                       hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                    }
                    hit.collider.gameObject.SendMessage("pistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                }
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
            }
        }
        else if (isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }

    void Reload()
    {
        int bulletsToReload = ammoClip - ammoClipLeft;

        if (ammoLeft >= bulletsToReload)
        {
            StartCoroutine("ReloadWeapon");

            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClip;
        }
        else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");

            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptySound);
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = realoadPistol;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().sprite = realoadPistol2;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;
        yield return new WaitForSeconds(0.15f);
        isReloading = false;
    }

    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().sprite = shotPistol2;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }

}
