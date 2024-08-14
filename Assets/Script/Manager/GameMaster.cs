using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
public class GameMaster : MonoBehaviour
{
    [SerializeField]
    string gameMode = "Debug";  //Debug,Build�Ń��[�h��؂�ւ���(Ranking�ɑ��M���邩���Ȃ�������)

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
        successCount = new SuccessCount();

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
    }


    void Update()
    {
        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.O))    //"O"�L�[���������Ǝ��s
            PancakeComplete(false);
        if (Input.GetKeyDown(KeyCode.I))    //"I"���������Ɛ���
            PancakeComplete(true);

    }

    //�V�[���J�ڎ��̌㏈��
    void OnSceneTransition(Scene next, LoadSceneMode mode)
    {
        timer.OnStop(); 


        var resultMaster = GameObject.FindWithTag("Manager").GetComponent<ResultMaster>();  //resultManager���擾

        resultMaster.time = timer.timeProperty.Value;   //���݂̎��Ԃ�n��
        resultMaster.success = successCount.successProperty.Value;  //�����񐔂�n��

        if(gameMode == "Build")
            repository.SendTimeToDataStore(timer.timeProperty.Value);   //���Ԃ𑗐M

        SceneManager.sceneLoaded -= OnSceneTransition;
    }

    //false�Ȃ�ł���(���s),true�Ȃ炫�ꂢ�ɏĂ���(����)
    public void PancakeComplete(bool pancakeState)
    {
        if (!pancakeState)
            lifePoint.SubtractLife();
        else
            successCount.AddSuccessCount();


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

    public IReadOnlyReactiveProperty<int> GetSuccessProperty()
    {
        return successCount.successProperty;
    }

    //���̃g�b�s���O�����J(����string�^)
    public IReadOnlyReactiveProperty<string> GetNextTopping()
    {
        return default;
    }
}
