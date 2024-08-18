using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using System.Diagnostics;
using System.Threading;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    string gameMode = "Debug";  //Debug,Build�Ń��[�h��؂�ւ���(Ranking�ɑ��M���邩���Ȃ�������)

    [SerializeField][Tooltip("�p���P�[�L���[�J�[")] private PancakeMaker _pancakeMaker;
    [SerializeField] [Tooltip("�g�b�s���O���[�J�[")] private ToppingMaker _toppingMaker;
    [SerializeField][Tooltip("�X�^�[�g�܂ł̕b��")] private int _startTime;

    Subject<PancakeComment> comment = new Subject<PancakeComment>();
    public ISubject<PancakeComment> OnComment => comment;   //�R�����g������ꂽ�Ƃ�

    //Game�Ŏg�p����e�p�����[�^
    LifePoint lifePoint;
    Timer timer;
    FirePower firepower;
    SuccessCount successCount;

    //�X�R�A��ۑ����Ă����f�[�^�x�[�X(Ranking)
    [Inject]
    IRepositiory repository; 

    

    void Awake()
    {
        lifePoint = GetComponent<LifePoint>();
        timer = GetComponent<Timer>();
        firepower = GetComponent<FirePower>();
        successCount = GetComponent<SuccessCount>();

        lifePoint.lifeProperty
            .Where(x => x == 0) //�̗͂�0�ɂȂ�����
            .Subscribe(_ =>
            {
                //�V�[���J��
                SceneManager.sceneLoaded += OnSceneTransition;
                SceneManager.LoadScene(
                    SceneDictionary.TypeOfName[SceneType.Result]
                    );
            }).AddTo(this);

        lifePoint.OnTimeOut
            .Subscribe(_ =>
            {
                comment.OnNext(PancakeComment.TIMEDOUT);
            }).AddTo(this);

        GameStart(this.GetCancellationTokenOnDestroy()).Forget();

    }


    private async UniTask GameStart(CancellationToken cancellationToken) {
        await UniTask.Delay(_startTime * 1000, cancellationToken: cancellationToken);
        timer.OnStart(); //�J�E���g�A�b�v�X�^�[�g
        firepower.StartPowerUp(this.GetCancellationTokenOnDestroy()).Forget();
        //�p���P�[�L���쐬
        _pancakeMaker.PancakeMake();
        //�g�b�s���O�𒊑I
        _toppingMaker.ChangeTopping();
    }

    void Update()
    {
        //�f�o�b�O�p
        /*if (Input.GetKeyDown(KeyCode.O))    //"O"�L�[���������Əł���
            PancakeComplete(PancakeFlag.BURNT);
        if (Input.GetKeyDown(KeyCode.I))    //"I"���������Ɗ���
            PancakeComplete(PancakeFlag.PERFECT);
        if (Input.GetKeyDown(KeyCode.P))    //"P"�L�[���������Ɨ��Ƃ���
            PancakeComplete(PancakeFlag.DROPED);
        if (Input.GetKeyDown(KeyCode.J))    //"J"���������ƏĂ���
            PancakeComplete(PancakeFlag.BAKED);*/

    }

    //�V�[���J�ڎ��̌㏈��
    void OnSceneTransition(Scene next, LoadSceneMode mode)
    {
        timer.OnStop(); 


        var resultMaster = GameObject.FindWithTag("Manager").GetComponent<ResultMaster>();  //resultManager���擾

        resultMaster.SetParam(timer.timeProperty.Value, successCount.successProperty.Value);

        if(gameMode == "Build")
            repository.SendTimeToDataStore(timer.timeProperty.Value);   //���Ԃ𑗐M

        SceneManager.sceneLoaded -= OnSceneTransition;
    }

    //--------------------//
    //BURNT   �ł���
    //DROPED  ���Ƃ���
    //PERFECT ����
    //BAKED   �܂��܂�
    //-------------------//
    public void PancakeComplete(PancakeFlag flag)
    {
        PancakeComment commentEvent = PancakeComment.NONE;

        switch(flag)
        {
            case PancakeFlag.BURNT:
                lifePoint.SubtractLife();
                commentEvent = PancakeComment.BURNT;
                break;

            case PancakeFlag.DROPED:
                lifePoint.SubtractLife();
                commentEvent = PancakeComment.DROPED;
                break;

            case PancakeFlag.PERFECT:
                lifePoint.AddLifePropotionFire(firepower.fireProperty.Value);
                successCount.AddSuccessCount();
                commentEvent = PancakeComment.PERFECT;
                break;

            case PancakeFlag.BAKED:
                successCount.AddSuccessCount();
                commentEvent = PancakeComment.COMMON;
                break;
        }

        lifePoint.SetPrevLife();
        comment.OnNext(commentEvent);

        //�p���P�[�L�쐬
        _pancakeMaker.PancakeMake();
        //�g�b�s���O�쐬
        _toppingMaker.ToppingMake();
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
    public IReadOnlyReactiveProperty<float> GetLifeProperty()
    {
        return lifePoint.lifeProperty;
    }

    //�����񐔂�ReactiveProperty���O���Ɍ��J
    public IReadOnlyReactiveProperty<int> GetSuccessProperty()
    {
        return successCount.successProperty;
    }
}
