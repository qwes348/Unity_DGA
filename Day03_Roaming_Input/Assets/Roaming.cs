﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : MonoBehaviour
{
    public Transform wayPointsRoot;
    List<Transform> wayPoints;

    public float moveSpeed = 1;
    private Vector3 nextPoint;
    public int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = new List<Transform>();
        foreach(Transform t in wayPointsRoot) // Transfrom은 자식에대해서 foreachable하다
        {
            wayPoints.Add(t);
        }
        nextPoint = wayPoints[n].transform.position;

        for (int i = 0; i < wayPoints.Count; i++)
        {
            wayPoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
        }
        wayPoints[n].GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, nextPoint) < 0.2f)
        {
            n++;
            //n = n % wayPoints.Count;
            n %= wayPoints.Count;  // 끝까지가면 초기화(if문대신 사용할수있음 외워두면 좋음)
            nextPoint = wayPoints[n].transform.position;

            wayPoints[n].GetComponent<MeshRenderer>().material.color = Color.yellow;
            if(n == 0)
                wayPoints[wayPoints.Count-1].GetComponent<MeshRenderer>().material.color = Color.magenta;
            else
                wayPoints[n-1].GetComponent<MeshRenderer>().material.color = Color.magenta;
        }
        Vector3 dir = nextPoint - transform.position;
        dir.y = 0;
        //transform.Rotate(Vector3.up * Vector3.Angle(transform.forward, dir) * 10f * Time.deltaTime);  // Angle은 주어진 두각중에 무조건 큰값만나옴(양의수만)
        transform.Rotate(Vector3.up * Vector3.SignedAngle(transform.forward, dir, Vector3.up) * 10f * Time.deltaTime); // 이과정을 간단하게 해주는게 Lerp함수
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.15f);  // Vector3.up 생략가능 디푤트가 up이기 때문
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.15f);

        //transform.position = transform.position + dir.normalized * moveSpeed * Time.deltaTime;
        //transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime);   // 처음에는 빠르고 갈수록 느려짐(계속 이동하는 현재위치에서 구하기때문)
        //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / dir.magnitude);      // 등속Ver.
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);      // MoveTowards 가장 실용적
        //transform.position = Vector3.Slerp(transform.position, nextPoint, moveSpeed * Time.deltaTime);      // Sphere Lerp        
    }
}