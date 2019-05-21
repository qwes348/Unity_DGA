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
    void CollisionWith(Collider collider);
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
    List<Collider> overlapChecker = null;

    private void Awake()
    {
        colliderList = new List<Collider>();
        overlapChecker = new List<Collider>();
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
        foreach(var c in hitBoxes)
        {
            if(c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Gizmos.DrawCube(bc.center, bc.size);
            }
            if(c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Gizmos.DrawSphere(sc.center, sc.radius);
            }
        }
    }

    private void CheckGizmoColor()
    {
        switch(state)
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
            overlapChecker = new List<Collider>();
            return;
        }

        colliderList.Clear();        

        if (state == ColliderState.Closed)
            return;

        foreach(var c in hitBoxes)
        {
            if(c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(bc.center), bc.size * 0.5f, transform.rotation, mask, QueryTriggerInteraction.Collide);
                // transform.TransformPoint 검색해보기
                // Overlap 함수도 검색해보기

                colliderList.AddRange(colliders);
            }
            if(c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(sc.center), sc.radius, mask, QueryTriggerInteraction.Collide);

                colliderList.AddRange(colliders);
            }

        }
        foreach (var c in colliderList)
        {
            if (overlapChecker != null && overlapChecker.Contains(c))
            {
                continue;
            }
            overlapChecker.Add(c);
            // C# 6.0 문법 아래 코멘트랑 똑같은 뜻이다
            responder?.CollisionWith(c);
            //if (responder != null)
            //    responder.CollisionWith(c);
            print("colliding: " + c.name);

        }
        state = colliderList.Count > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
    }

    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
        overlapChecker.Clear();
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}
