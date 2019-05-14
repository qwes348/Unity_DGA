using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 10;
    public Light muzzleFlash;
    public GameObject shell;
    //public GameObject shell_hole;
    public Transform shellEjection;
    public GameObject impactFX;
    public GameObject bulletHolePrefab;
    public int maxBullets;
    public AudioClip[] clips;

    AudioSource audiosource;
    private bool isReloading = false;
    private int currentBullets;
    Animator gunAnim;

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos, smoothVel;

    float recoilAngle;
    float recoilVel;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInParent<Camera>();
        originPos = transform.localPosition;
        gunAnim = GetComponent<Animator>();
        currentBullets = maxBullets;
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBullets > 0 && !isReloading)
        {
            fpsCamera.transform.localRotation *= Quaternion.Euler(Vector3.left * recoilAngle);

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;   // 초당 10발을 쏠수있다
                Shoot();
                //ShellOut();
            }
            // kick damping
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originPos, ref smoothVel, 0.1f);  // lerp함수와 비슷, 스프링처럼 동작함

            // recoil damping
            recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilVel, 0.2f);
        }
        if(currentBullets == 0 && !isReloading)
        {
            if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                audiosource.clip = clips[1];
                nextTimeToFire = Time.time + 1f / fireRate; 
                audiosource.Play();
            }
        }
        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reloading());
        }
    }

    private void Shoot()
    {
        currentBullets--;
        // 총구화염
        muzzleFlash.enabled = true;
        Invoke("OffFlashLight", 0.05f);  // 코루틴없이 시간을주는 Invoke

        MakeShell();

        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 200f))  // 총에서부터가 아니라 카메라에서 쏨
        {
            print(hit.transform.name);            
            Debug.DrawLine(fpsCamera.transform.position, hit.point, Color.magenta, 2f);
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(fpsCamera.transform.forward * 500f);
            }
            var bs = hit.transform.GetComponent<BulletSound>();
            if (bs != null)
                bs.Play();
            var brs = hit.transform.GetComponent<BulletRandomSound>();
            if (brs != null)
                brs.Play();
        }

        audiosource.clip = clips[0];
        //GetComponent<AudioSource>().Play();
        audiosource.Play();

        GameObject fx = Instantiate(impactFX, hit.point, Quaternion.identity);
        Destroy(fx, 0.3f);

        MakeBulletHole(hit.point, hit.normal, hit.transform);

        // 반동구현(kick)
        transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);

        // recoil 반동구현
        recoilAngle += UnityEngine.Random.Range(2f, 5f);
        recoilAngle = Mathf.Clamp(recoilAngle, 0, 25);
    }

    private void MakeBulletHole(Vector3 point, Vector3 normal, Transform parent)
    {
        var clone = Instantiate(bulletHolePrefab, point+normal*0.01f, Quaternion.FromToRotation(-Vector3.forward, normal));
        clone.transform.parent = parent;
        Destroy(clone, 3f);
    }

    private void MakeShell()
    {
        GameObject clone = Instantiate(shell, shellEjection);
        clone.transform.parent = null;
    }

    //private void ShellOut()
    //{
    //    var _shell = Instantiate(shell, shell_hole.transform);
    //    _shell.transform.parent = null;
    //    _shell.GetComponent<Rigidbody>().AddForce(Vector3.right * 0.1f);
    //    Destroy(_shell, 1f);
    //}

    void OffFlashLight()
    {
        muzzleFlash.enabled = false;
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        currentBullets = maxBullets;
        //gunAnim.SetBool("isReloading", true);
        gunAnim.SetTrigger("isReloading");
        audiosource.clip = clips[2];
        audiosource.Play();
        yield return new WaitForSeconds(1f);
        isReloading = false;
        //gunAnim.SetBool("isReloading", false);
        
    }
}
