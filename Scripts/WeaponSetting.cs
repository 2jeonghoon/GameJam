using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������ ������ ���� ������ �� �������� ����ϴ� �������� ����ü�� ��� �����ϸ�
// ������ �߰�/������ �� ����ü�� �����ϱ� ������ �߰�/������ ���� ������ ������

// ����ȭ���� ������ �ٸ� Ŭ������ ������ �����Ǿ��� �� �ν����� �信 ��� �������� ����� ���� �ʴ´�!
[System.Serializable]
public struct WeaponSetting
{
    public float attackRate; // ���� �ӵ�
    public float attackDistance; // ���� ��Ÿ�
    public bool isAutomaticAttack; // ���� ���� ����

}
