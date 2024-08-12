using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class GameMaster : MonoBehaviour
{
    LifePoint lifePoint;
    Timer timer;
    FirePower firepower;




    void Awake()
    {
        lifePoint = GetComponent<LifePoint>();
        timer = GetComponent<Timer>();
        firepower = GetComponent<FirePower>();

        timer.timeProperty    //�J�E���g�_�E����0�ȉ��ɂȂ�����V�[���J��
            .Where(x => x <= 0)
            .Subscribe(_ =>
            {
                OnSceneTransition();
                //�V�[���J��
                Debug.Log("�V�[���J��");
            }).AddTo(this);

        lifePoint.lifeProperty
            .Where(x => x <= 0)
            .Subscribe(_ =>
            {
                OnSceneTransition();
                Debug.Log("�V�[���J��");
                //�V�[���J��
            }).AddTo(this);
    }

    void Start()
    {
        timer.OnStart();�@//�J�E���g�_�E���X�^�[�g
        firepower.StartPowerUp(this.GetCancellationTokenOnDestroy()).Forget();
        //�p���P�[�L���쐬
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))    //�f�o�b�O�p"O"�L�[���������ƃ_���[�W
            PancakeComplete(false);
        
            
    }

    //�V�[���J�ڎ��̌㏈��
    void OnSceneTransition()
    {
        timer.OnStop();
    }

    //false�Ȃ�ł���(���s),true�Ȃ炫�ꂢ�ɏĂ���(����)
    public void PancakeComplete(bool pancakeState)
    {
        if (!pancakeState)
            lifePoint.SubtractLife();

        //�p���P�[�L�쐬
        //�g�b�s���O�쐬

    }

    //���݂̉Η͂��O���Ɍ��J
    public float GetFire()
    {
        return firepower.fireProperty.Value;
    }

    //���Ԃ�ReactiveProperty���O���Ɍ��J
    public IReadOnlyReactiveProperty<float> GetTimeProperty()
    {
        return timer.timeProperty;
    }

    //�̗͂�ReactiveProperty���O���Ɍ��J
    public IReadOnlyReactiveProperty<int> GetLifeProperty()
    {
        return lifePoint.lifeProperty;
    }

    //���̃g�b�s���O�����J(����string�^)
    public IReadOnlyReactiveProperty<string> GetNextTopping()
    {
        return default;
    }
}
