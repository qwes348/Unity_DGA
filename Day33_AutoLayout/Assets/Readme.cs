using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readme : MonoBehaviour
{
}

// https://www.hallgrimgames.com/blog/2018/10/16/unity-layout-groups-explained
/* Vertical Layout Group
 
 *  Control Child Size
 *  1. 자식 객체의 크기를 부모가 제어하겠다
 *  2. 대부분 0의 초기값을 가짐, 단 Image/Text는 에외, texture를 가지면 해당 크기가 사용이 된다
 *  3. 자식의 Height Fild는 disabled가 되어서 조정 불가 상태
 *  
 *  Child Force Expand
 *  1. 사용치 않은 공간(Unused Space)이 있다면 자식을 확장해서 채우겠다.
 *  2. Unused Space : 부모 height - 모든자식들의 preferred/min height 합
 *  
 *  Layout Element
 *  1. 객체의 크기가 이렇게되면 좋겠다 라는 정보를 제공, 기종 정보를 override 시킴
 *  2. Min, Preferred, Flexible
 *  3. Flexible : 단위가 ratio(weight) == 가중치(비율)
 *  4. Image/Text Component는 prefferred 값이 자동으로 설정된다. 단 실제 Texture가 할당된 경우만 가능
 *      예를들어 Image의 Texture 없이 색상만 넣었다면 prefferred값은 0이다 즉 존재하지 않게됨(현물이 없음)
 *   5. Flexible이 제대로 동작하려면 부모의 Child Force Expand를 Off해야 한다. (서로 상반되는 개념이다)
 *   */

