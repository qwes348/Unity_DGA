using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderState
{
    Closed,
    Open,
    Colliding
}

public interface IHitBoxResponder
{
    void CollisionWith(Collider collider, HitBox hitBox);
}

public class HitBox : MonoBehaviour
{
    public LayerMask mask;
    public Collider[] hitBoxes;  // 한 무기에 히트박스가 여러개일수도있음
    public Color inactiveColor;         // Closed
    public Color collisionOpenColor;    // Open
    public Color collidingColor;        // Colliding
    public bool drawGizmo = true;
    public bool updateInEditor = false;

    public ColliderState state = ColliderState.Closed;

    List<Collider> colliderList;
    IHitBoxResponder responder = null;

    [HideInInspector]
    public Dictionary<int, int> hitObjects;

    public bool enabledMultipleHit { get; set; }

    //List<Collider> overlapChecker = null;

    private void Awake()
    {
        colliderList = new List<Collider>();
        //overlapChecker = new List<Collider>();
        hitObjects = new Dictionary<int, int>();
    }
    private void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        if (updateInEditor && !Application.isPlaying)  // 씬뷰에서 테스트하기위해 사용
        {
            UpdateHitBox();
            print("UpdateHitBox");
        }

        CheckGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;  // 월드기준에서의 transfrom을 환산(사이즈, 반지름, 센터)
        foreach (var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Gizmos.DrawCube(bc.center, bc.size);
            }
            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Gizmos.DrawSphere(sc.center, sc.radius);
            }
        }
    }

    public void GetContactInfo(Vector3 from, Vector3 to, out Vector3 hitPoint, out Vector3 hitNormal, out Vector3 hitDirection, float maxDistance)
    {
        RaycastHit hit;
        hitPoint = to;
        hitNormal = from - hitPoint;
        hitNormal = hitNormal.normalized;
        hitDirection = -hitNormal;
        if (Physics.Raycast(from,
                            hitDirection,
                            out hit,
                            maxDistance,
                            mask,
                            QueryTriggerInteraction.Collide))
        {
            hitPoint = hit.point;
            hitNormal = hit.normal;
        }
        Debug.DrawLine(from, hitPoint, Color.yellow, 2f);
        Debug.DrawLine(hitPoint, hitPoint + hitNormal, Color.magenta, 2f);
        Debug.DrawLine(hitPoint, hitPoint + hitDirection, Color.cyan, 2f);
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Closed:
                Gizmos.color = inactiveColor;
                break;
            case ColliderState.Open:
                Gizmos.color = collisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
        }
    }

    public void UpdateHitBox()
    {
        if (colliderList == null)
        {
            colliderList = new List<Collider>();
            //overlapChecker = new List<Collider>();
            return;
        }
        else
            colliderList.Clear();

        if (state == ColliderState.Closed)
            return;

        foreach (var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(bc.center), bc.size * 0.5f, transform.rotation, mask, QueryTriggerInteraction.Collide);
                // transform.TransformPoint 검색해보기
                // Overlap 함수도 검색해보기

                colliderList.AddRange(colliders);
            }
            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(sc.center), sc.radius, mask, QueryTriggerInteraction.Collide);

                colliderList.AddRange(colliders);
            }

        }
        foreach (var c in colliderList)
        {
            int id = c.transform.root.gameObject.GetInstanceID();
            if (!hitObjects.ContainsKey(id))
                hitObjects[id] = 1;
            else
            {
                hitObjects[id] += 1;
                if (!enabledMultipleHit)
                    continue;    // 다단히트를 결정짓는부분 return이 없으면 다단히트들어감
            }
            // C# 6.0 문법 아래 코멘트랑 똑같은 뜻이다
            responder?.CollisionWith(c, this);
            //if (responder != null)
            //    responder.CollisionWith(c);            

        }
        state = colliderList.Count > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
        hitObjects.Clear();
    }

    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
        //overlapChecker.Clear();  ////HW
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}
