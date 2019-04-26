using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    // 충돌은 쌍방이다. 가해자/피해자 항상존재한다!
    
    // OnCollision...() 발생 조건
    // 1. 두개의 gameObject 모두 collider component가 존재해야한다
    // 2. 둘 중 하나는 rigidbody component가 있어야 한다
    // 3. rigidbody를 가진 gameObject가 움직여 충돌되었을때 발생한다. 그 반대는 발생하지 않는다(예측불가)
    // 즉 이동하는 gameObject는 꼭 rigidbody를 가지고 있어야 충돌이 제대로 발생한다.
    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter: " + collision.gameObject.name);
        foreach(ContactPoint contact in collision.contacts)  // contacts => 닿은부분을 array형태로 가지고있다
        {
            Debug.DrawRay(contact.point, contact.normal, Color.magenta, 5f);  // normal 벡터 => 평면에 직교하는 벡터
            //           (origin, 벡터의방향, 컬러, 초)
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        print("OnCollisionStay: " + collision.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        print("OnCollisionExit: " + collision.gameObject.name);
    }
    
    // OnTrigger...()
    // 발생조건
    // 1. 두개의 gameObject 모두 collider Component가 존재해야 한다
    // 2. 둘중 하나는 rigidbody Componene가 있어야한다.
    // 3. 둘중 하나는 collider Component에 Is Trigger가 On되어야 한다.
    // 4. 그리고 어느 쪽이 움직이더라도 서로 만나면 이벤트가 발생한다.
    // 5. OnTriggerEnter 발생 시 OnCollisionEnter가 발생하지 않는다.
    // 즉 물리적인 결과과 필요한게 아니고 충돌체크만 알고싶을때 사용한다.
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnger : " + other.gameObject.name);
    }
    private void OnTriggerStay(Collider other)
    {
        print("OnTriggerStay : " + other.gameObject.name);
    }
    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit : " + other.gameObject.name);
    }
}
