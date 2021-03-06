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
    private AudioClip audioClipTakeOutWeapon; // 무기 장착 사운드
    [SerializeField]
    private AudioClip audioClipFire; // 공격 사운드

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting; // 무기 설정

    private float lastAttackTime = 0; // 마지막 발사시간 체크용

    private AudioSource audioSource; // 사운드 재생 컴포넌트
    private PlayerAnimatorController animator; // 애니메이션 재생 제어

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<PlayerAnimatorController>();
    }

    private void OnEnable()
    {
        // 무기 장착 사운드 재생
        PlaySound(audioClipTakeOutWeapon);
        // 총구 이펙트 오브젝트 비활성화
        muzzleFlashEffect.SetActive(false);
    }
    // 외부에서 무기 Action을 시작하기 위해 마우스 왼쪽/ 오른쪽..
    public void StartWeaponAction(int type = 0)
    {
        if(type == 0)
        {
            // 연속 공격
            if(weaponSetting.isAutomaticAttack == true)
            {
                StartCoroutine("OnAttackLoop");
            }
            else // 단발 공격
            {
                OnAttack();
            }
        }
    }

    public void StopWeaponAction(int type = 0)
    {
        //마우스 왼쪽 클릭(공격 종료)
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
        // 공격 속도에 맞춰 공격
        if(Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            // 뛰고있을 때는 공격 불가
            if(animator.MoveSpeed > 0.5f)
            {
                return;
            }

            // 공격주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

            //무기 애니메이션 재생
            animator.Play("Fire", -1, 0);
            // 총구 이펙트 재생
            StartCoroutine("OnMuzzleFlashEffect");
            // 공격 사운드 재생
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
