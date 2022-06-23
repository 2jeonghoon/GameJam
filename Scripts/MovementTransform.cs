using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTransform : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f; //이동속도
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;//이동방향

    // Update is called once per frame
    //이동방향이 설정되면 알아서 이동하도록 함.
    void Update()
    {
        //1초마다 방향 * 속도로 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    
    //외부에서 매개변수로 이동 방향을 설정
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }

}
