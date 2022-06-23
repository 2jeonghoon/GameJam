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
        //�޸�Ǯ�� �̿��� Ȱ��ȭ ��Ȱ��ȭ, Ȱ��ȭ �� �� OnFadeEffect�޼ҵ带 ������.
        StartCoroutine("OnFadeEffect");
    }

    private void OnDisable()
    {
        //��Ȱ��ȭ
        StopCoroutine("OnFadeEffect");
    }

    private IEnumerator OnFadeEffect()
    {
        while (true)
        {
            //���������� ����
            Color color = meshRenderer.material.color;
            //�����żҵ�(0~ �ι�° �Ű��������� �Դٰ���)�� �̿��ؼ� �÷��� �ٲ���.
            color.a = Mathf.Lerp(1, 0, Mathf.PingPong(Time.time * fadeSpeed, 1));
            meshRenderer.material.color = color;

            yield return null;
        }
    }

}