using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMemoryPool : MonoBehaviour
{
    //��հ� ���� ������ �� Ȱ��ȭ
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject enemySpawnPointPrefab; // ���� �����ϱ� �� ���� ���� ��ġ�� �˷��ִ� ������
    [SerializeField]
    private GameObject enemyPrefab; // �����Ǵ� �� ������
    [SerializeField]
    private float enemySpawnTime = 1; //�� ���� �ֱ�
    [SerializeField]
    private float enemySpawnLatency = 1; //Ÿ�� ���� �� ���� �����ϱ���� ��� �ð�

    private MemoryPool spawnPointMemoryPool; //�� ���� ��ġ�� �˷��ִ� ������Ʈ ����, Ȱ��/��Ȱ�� ����
    private MemoryPool enemyMemoryPool; //�� ����, Ȱ��/��Ȱ�� ����

    private int numberOfEnemiesSpawnedAtOnce = 1; //���ÿ� �����Ǵ� ���� ����
    private Vector2Int mapSize = new Vector2Int(30, 30); //�� ũ��

    private void Awake()
    {
        //��հ� ���� ���� �Ҵ��ϰ�
        spawnPointMemoryPool = new MemoryPool(enemySpawnPointPrefab);
        enemyMemoryPool = new MemoryPool(enemyPrefab);
        //�ڷ�ƾ ����
        StartCoroutine("SpawnTile");
    }

    private IEnumerator SpawnTile()
    {
        //�� ���� ������ ��ġ�� ���� ��� ����
        int currentNumber = 0;
        int maximunNumber = 50;

        while (true)
        {
            //ó������ �ϳ��� ����. 
            //���ÿ� numberOfEnemiesSpawnedAtOnce ���ڸ�ŭ ���� �����ǵ��� �ݺ���
            for (int i = 0; i < numberOfEnemiesSpawnedAtOnce; ++i)
            {
                //��� ������Ʈ ����
                GameObject item = spawnPointMemoryPool.ActivatePoolItem();
                //��տ�����Ʈ ��ġ�� ���Ƿ� ����
                item.transform.position = new Vector3(Random.Range(-mapSize.x * 0.49f, mapSize.x * 0.49f), -1, Random.Range(-mapSize.y * 0.49f, mapSize.y * 0.49f));
                //�ð��� ������ ���ͻ��� �ڷ�ƾ ȣ��
                StartCoroutine("SpawnEnemy", item);
            }

            //�ð��� �������� �þ
            currentNumber++;

            //���� ���� �Ѿ�� Ȯ��
            if (currentNumber >= maximunNumber)
            {
                currentNumber = 0;
                numberOfEnemiesSpawnedAtOnce++;
            }

            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    private IEnumerator SpawnEnemy(GameObject point)
    {
        //���ð�
        yield return new WaitForSeconds(enemySpawnLatency);

        //�� ������Ʈ�� �����ϰ�, ���� ��ġ�� point�� ��ġ�� ����
        GameObject item = enemyMemoryPool.ActivatePoolItem();
        item.transform.position = point.transform.position;

        item.GetComponent<EnemyFSM>().Setup(target);

        //Ÿ�� ������Ʈ�� ��Ȱ��ȭ
        spawnPointMemoryPool.DeactivatePoolItem(point);
    }

}