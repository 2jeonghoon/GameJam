using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        // "Player" ������Ʈ �������� �ڽ� ������Ʈ��
        // "arms_assault_rifle_01" ������Ʈ�� Animator ������Ʈ�� �ִ�
        animator = GetComponentInChildren<Animator>();
    }

    public float MoveSpeed
    {
        set => animator.SetFloat("movementSpeed", value);
        get => animator.GetFloat("movementSpeed");
    }

    // animater.play method�� �ܺο��� ȣ���� �� �ֵ��� Play method ����
    public void Play(string stateName, int layer, float normalizedTime)
    {
        animator.Play(stateName, layer, normalizedTime);
    }
}
