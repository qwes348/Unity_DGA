using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 10;
    public Light muzzleFlash;

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos, smoothVel;

    public GameObject shell;
    public GameObject shell_hole;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInParent<Camera>();
        originPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;   // 초당 10발을 쏠수있다
            Shoot();
            ShellOut();
        }
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originPos, ref smoothVel, 0.1f);  // lerp함수와 비슷, 스프링처럼 동작함
    }

    private void Shoot()
    {
        // 총구화염
        muzzleFlash.enabled = true;
        Invoke("OffFlashLight", 0.05f);  // 코루틴없이 시간을주는 Invoke

        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 200f))  // 총에서부터가 아니라 카메라에서 쏨
        {
            print(hit.transform.name);            
            Debug.DrawLine(fpsCamera.transform.position, hit.point, Color.magenta, 2f);
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(fpsCamera.transform.forward * 500f);
            }
        }


        // 반동구현
        transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);
    }

    private void ShellOut()
    {
        var _shell = Instantiate(shell, shell_hole.transform);
        _shell.transform.parent = null;
        _shell.GetComponent<Rigidbody>().AddForce(Vector3.right * 0.1f);
        Destroy(_shell, 1f);
    }

    void OffFlashLight()
    {
        muzzleFlash.enabled = false;
    }
}
