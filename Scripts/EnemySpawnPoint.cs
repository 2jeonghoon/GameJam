using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 4;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        //메모리풀을 이용해 활성화 비활성화, 활성화 될 때 OnFadeEffect메소드를 실행함.
        StartCoroutine("OnFadeEffect");
    }

    private void OnDisable()
    {
        //비활성화
        StopCoroutine("OnFadeEffect");
    }

    private IEnumerator OnFadeEffect()
    {
        while (true)
        {
            //색상정보를 저장
            Color color = meshRenderer.material.color;
            //핑퐁매소드(0~ 두번째 매개변수까지 왔다갔다)를 이용해서 컬러를 바꿔줌.
            color.a = Mathf.Lerp(1, 0, Mathf.PingPong(Time.time * fadeSpeed, 1));
            meshRenderer.material.color = color;

            yield return null;
        }
    }

}