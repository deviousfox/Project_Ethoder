using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        PlayerDelegate.OnPlayerSpawnetEventHandler += SetTransform;
    }

    public void SetTransform(Transform tr)
    {
        tr.position = transform.position;
        tr.rotation = transform.rotation;
    }

    public void Start()
    {
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }
}
