using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    string gameMode = "Debug";  //Debug,Build�Ń��[�h��؂�ւ���(Ranking�ɑ��M���邩���Ȃ�������)

    [SerializeField][Tooltip("�p���P�[�L���[�J�[")] private PancakeMaker _pancakeMaker;
    [SerializeField] [Tooltip("�g�b�s���O���[�J�[")] private ToppingMaker _toppingMaker;

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
            .Where(x => x <= 0) //�̗͂�0�ɂȂ�����
            .Subscribe(_ =>
            {
                //�V�[���J��
                Debug.Log("�V�[���J��");
                SceneManager.sceneLoaded += OnSceneTransition;
                SceneManager.LoadScene(
                    SceneDictionary.TypeOfName[SceneType.Result]
                    );
            }).AddTo(this);

    }

    void Start()
    {
        timer.OnStart(); //�J�E���g�A�b�v�X�^�[�g
        firepower.StartPowerUp(this.GetCancellationTokenOnDestroy()).Forget();
        //�p���P�[�L���쐬
        _pancakeMaker.PancakeMake();
        //�g�b�s���O�𒊑I
        _toppingMaker.ToppingMake();
    }


    void Update()
    {
        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.O))    //"O"�L�[���������Əł���
            PancakeComplete(PancakeFlag.BURNT);
        if (Input.GetKeyDown(KeyCode.I))    //"I"���������Ɗ���
            PancakeComplete(PancakeFlag.PERFECT);
        if (Input.GetKeyDown(KeyCode.P))    //"P"�L�[���������Ɨ��Ƃ���
            PancakeComplete(PancakeFlag.DROPED);
        if (Input.GetKeyDown(KeyCode.J))    //"J"���������ƏĂ���
            PancakeComplete(PancakeFlag.BAKED);


    }

    //�V�[���J�ڎ��̌㏈��
    void OnSceneTransition(Scene next, LoadSceneMode mode)
    {
        timer.OnStop(); 


        var resultMaster = GameObject.FindWithTag("Manager").GetComponent<ResultMaster>();  //resultManager���擾

        resultMaster.time = timer.timeProperty.Value;   //���݂̎��Ԃ�n��
        resultMaster.success = successCount.successProperty.Value;  //�����񐔂�n��

        Debug.Log(successCount.successProperty.Value);
        Debug.Log(resultMaster.success);

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
        switch(flag)
        {
            case PancakeFlag.BURNT:
                lifePoint.SubtractLife();
                comment.OnNext(PancakeComment.BURNT);
                break;

            case PancakeFlag.DROPED:
                lifePoint.SubtractLife();
                comment.OnNext(PancakeComment.DROPED);
                break;

            case PancakeFlag.PERFECT:
                lifePoint.AddLife();
                successCount.AddSuccessCount();
                comment.OnNext(PancakeComment.PERFECT);
                break;

            case PancakeFlag.BAKED:
                successCount.AddSuccessCount();
                comment.OnNext(PancakeComment.COMMON);
                break;
        }

        //�p���P�[�L�쐬
        _pancakeMaker.PancakeMake();
        //�g�b�s���O�쐬
        //nextTopping.Value = (ToppingList)Random.Range(1, 2);
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
