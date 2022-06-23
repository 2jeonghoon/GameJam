using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : MonoBehaviour
{
    //������Ʈ�� �������ʰ� ��Ȱ��ȭ�ؼ� �����ϴ� �޸�Ǯ

    //�޸� Ǯ�� �����Ǵ� ������Ʈ ����
    private class PoolItem
    {
        public bool isActive; //"gameObject"�� Ȱ��ȭ/��Ȱ��ȭ ����
        public GameObject gameObject; //ȭ�鿡 ���̴� ���� ���ӿ�����Ʈ
    }

    private int increaseCount = 5; //������Ʈ�� ������ �� instantiate()�� �߰� �����Ǵ� ������Ʈ ����
    private int maxCount; // ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ����
    private int activeCount; //���� ���ӿ� ���ǰ� �ִ�(Ȱ��ȭ) ������Ʈ ����

    private GameObject poolObject;// ������Ʈ Ǯ������ �����ϴ� ���ӿ�����Ʈ ������
    private List<PoolItem> poolItemList; //�����Ǵ� ��� ������Ʈ�� �����ϴ� ����Ʈ

    public int MaxCount => maxCount; // �ܺο��� ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ���� Ȯ���� ���� ������Ƽ
    public int ActiveCount => activeCount; //�ܺο��� ���� Ȱ��ȭ �Ǿ� �ִ� ������Ʈ ���� Ȯ���� ���� ������Ƽ


    public MemoryPool(GameObject gameObject)
    {
        //�����ڷ� �ʱ�ȭ
        maxCount = 0;
        activeCount = 0;
        this.poolObject = gameObject;

        poolItemList = new List<PoolItem>();

        //���� 5���� ������ ����
        InstantiateObjects();
    }

    //increaseCount������ ������Ʈ ����
    public void InstantiateObjects()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; ++i)
        {
            PoolItem poolItem = new PoolItem();

            //�ٷ� ������� ���� ���� �ֱ� ������ false�� ������ �ʰ� ��.
            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject);
            poolItem.gameObject.SetActive(false);

            //����Ʈ�� ����
            poolItemList.Add(poolItem);
        }
    }

    //���� ��������(Ȱ��/��Ȱ��) ��� ������Ʈ ����, ���� ����ǰų� ������ ����� �� �� ���� ȣ��
    public void DestroyObjects()
    {
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList[i].gameObject);
        }

        poolItemList.Clear();
    }


    //���� ��Ȱ��ȭ ������ ������Ʈ �� �ϳ��� Ȱ��ȭ�� ����� ���
    public GameObject ActivatePoolItem()
    {
        //����Ʈ�� ��������� �������� ������ƮX
        if (poolItemList == null) return null;

        //���� �����ؼ� �����ϴ� ��� ������Ʈ ������ ���� Ȱ��ȭ ������ ������Ʈ ���� ��
        //��� ������Ʈ�� Ȱ��ȭ �����̸� ���ο� ������Ʈ �ʿ�
        if (maxCount == activeCount)
        {
            //����
            InstantiateObjects();
        }

        //��Ȱ��ȭ ������ ������Ʈ ã��
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.isActive == false)
            {
                activeCount++;

                poolItem.isActive = true;
                poolItem.gameObject.SetActive(true);

                //����ϵ��� ��ȯ
                return poolItem.gameObject;
            }
        }

        return null;
    }


    //����� ���� ������Ʈ ��Ȱ��ȭ
    public void DeactivatePoolItem(GameObject removeObject)
    {
        if (poolItemList == null || removeObject == null) return;

        //����Ʈ���� �Ű������� ���� ������Ʈ�� ���� ��Ҹ� ã�� ��Ȱ��ȭ
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];
            if (poolItem.gameObject == removeObject)
            {
                activeCount--;

                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }

    }

    //���ӿ� ������� ��� ������Ʈ�� ��Ȱ��ȭ ���·� ����
    public void DeactivateAllPoolItems()
    {
        if (poolItemList == null) return;

        //����Ʈ�� ���鼭 Ȱ��ȭ ������ ������Ʈ�� ��� ��Ȱ��ȭ
        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolitem = poolItemList[i];

            if (poolitem.gameObject != null && poolitem.isActive == true)
            {
                poolitem.isActive = false;
                poolitem.gameObject.SetActive(false);
            }
        }

        activeCount = 0;
    }

}