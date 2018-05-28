using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

public class TestScript : MonoBehaviour
{

    [Inject(Optional = true)]
    private QuxSignal m_quxSignal;

    [Inject(Optional = true)]
    private FooSignal m_fooSignal;

    [Inject(Optional = true)]
    private BarSignal m_barSignal;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (SceneManager.GetActiveScene().name == "SceneFoo")
            {
                m_fooSignal.Fire(new FooSignal.Args((int) (Random.value * 1000)), new FooSignal.Args2(0));
            }

            if (SceneManager.GetActiveScene().name == "SceneBar")
            {
                m_barSignal.Fire();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            m_quxSignal.Fire(new QuxSignal.Args());
        }
    }

}