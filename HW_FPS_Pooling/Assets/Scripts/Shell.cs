using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shell : MonoBehaviour
{    
    public float forceMin = 50;
    public float forceMax = 200;

    float lifetime = 4f;
    float fadetime = 2f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //float force = Random.Range(forceMin, forceMax);
        //rb.AddForce(Vector3.right * force);
        //rb.AddTorque(Random.insideUnitSphere * force);  // 원안에서 나올수있는 모든벡터가 랜덤으로나옴?

        //StartCoroutine(Fade());
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        float force = Random.Range(forceMin, forceMax);
        rb.AddForce(Vector3.right * force);
        rb.AddTorque(Random.insideUnitSphere * force);  // 원안에서 나올수있는 모든벡터가 랜덤으로나옴?

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifetime);

        float percent = 0;        
        Material mat = GetComponent<Renderer>().material;
        Color initialColor = mat.color;
        while(percent < 1)
        {
            percent += (1 / fadetime) * Time.deltaTime;
            mat.color = Color.Lerp(initialColor, Color.clear, percent);

            yield return null;
        }

        Destroy(gameObject);
    }
}
