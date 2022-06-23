using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssaultRifle : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Fire Effects")]
    [SerializeField]
    private GameObject muzzleFlashEffect;
    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon; // ���� ���� ����
    [SerializeField]
    private AudioClip audioClipFire; // ���� ����

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting; // ���� ����

    private float lastAttackTime = 0; // ������ �߻�ð� üũ��

    private AudioSource audioSource; // ���� ��� ������Ʈ
    private PlayerAnimatorController animator; // �ִϸ��̼� ��� ����

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<PlayerAnimatorController>();
    }

    private void OnEnable()
    {
        // ���� ���� ���� ���
        PlaySound(audioClipTakeOutWeapon);
        // �ѱ� ����Ʈ ������Ʈ ��Ȱ��ȭ
        muzzleFlashEffect.SetActive(false);
    }
    // �ܺο��� ���� Action�� �����ϱ� ���� ���콺 ����/ ������..
    public void StartWeaponAction(int type = 0)
    {
        if(type == 0)
        {
            // ���� ����
            if(weaponSetting.isAutomaticAttack == true)
            {
                StartCoroutine("OnAttackLoop");
            }
            else // �ܹ� ����
            {
                OnAttack();
            }
        }
    }

    public void StopWeaponAction(int type = 0)
    {
        //���콺 ���� Ŭ��(���� ����)
        if(type == 0)
        {
            StopCoroutine("OnAttackLoop");
        }
    }

    private IEnumerator OnAttackLoop()
    {
        while (true)
        {
            OnAttack();

            yield return null;
        }
    }

    public void OnAttack()
    {
        // ���� �ӵ��� ���� ����
        if(Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            // �ٰ����� ���� ���� �Ұ�
            if(animator.MoveSpeed > 0.5f)
            {
                return;
            }

            // �����ֱⰡ �Ǿ�� ������ �� �ֵ��� �ϱ� ���� ���� �ð� ����
            lastAttackTime = Time.time;

            //���� �ִϸ��̼� ���
            animator.Play("Fire", -1, 0);
            // �ѱ� ����Ʈ ���
            StartCoroutine("OnMuzzleFlashEffect");
            // ���� ���� ���
            PlaySound(audioClipFire);
        }
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlashEffect.SetActive(true);

        yield return new WaitForSeconds(weaponSetting.attackRate * 0.3f);

        muzzleFlashEffect.SetActive(false);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
