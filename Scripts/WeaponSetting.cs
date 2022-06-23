using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 무기의 종류가 여러 종류일 떄 공용으로 사용하는 변수들은 구조체로 묶어서 정의하면
// 변수가 추가/삭제될 때 구조체에 선언하기 떄문에 추가/삭제에 대한 관리가 용이함

// 직렬화하지 않으면 다른 클래스의 변수로 생성되었을 떄 인스펙터 뷰에 멤버 변수들의 목록이 뜨지 않는다!
[System.Serializable]
public struct WeaponSetting
{
    public float attackRate; // 공격 속도
    public float attackDistance; // 공격 사거리
    public bool isAutomaticAttack; // 연속 공격 여부

}
