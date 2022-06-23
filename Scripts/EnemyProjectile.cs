using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //�Ѿ� ��Ʈ��
    private MovementTransform movement;
    private float projectileDistance = 30; //�߻�ü �ִ� �߻�Ÿ�

    public void Setup(Vector3 position)
    {
        //���⼭ �޾ƿ� ��ġ�� ������.
        movement = GetComponent<MovementTransform>();
        StartCoroutine("OnMove", position);
    }

    private IEnumerator OnMove(Vector3 targetPosition)
    //�̵������ �̵������ʰ��� �˻�
    {
        Vector3 start = transform.position;

        //�̵����� ����
        movement.MoveTo((targetPosition - transform.position).normalized);

        while (true)
        {
            //�ִ�Ÿ��� ����� �Ѿ� �����ϰ� �ڷ�ƾ ����
            if(Vector3.Distance(transform.position, start) >= projectileDistance)
            {
                Destroy(gameObject);

                yield break;
            }

            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    //�÷��̾�� �߻�ü�� �浹�ϸ� �Ѿ� ����
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }
    }

}
