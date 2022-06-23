using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //총알 컨트롤
    private MovementTransform movement;
    private float projectileDistance = 30; //발사체 최대 발사거리

    public void Setup(Vector3 position)
    {
        //여기서 받아온 위치로 지나감.
        movement = GetComponent<MovementTransform>();
        StartCoroutine("OnMove", position);
    }

    private IEnumerator OnMove(Vector3 targetPosition)
    //이동방향과 이동범위초과를 검사
    {
        Vector3 start = transform.position;

        //이동방향 설정
        movement.MoveTo((targetPosition - transform.position).normalized);

        while (true)
        {
            //최대거리를 벗어나면 총알 삭제하고 코루틴 종료
            if(Vector3.Distance(transform.position, start) >= projectileDistance)
            {
                Destroy(gameObject);

                yield break;
            }

            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    //플레이어와 발사체가 충돌하면 총알 삭제
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }
    }

}
