using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class HowManager : MonoBehaviour
{
    [SerializeField] GameObject btn;
    // Start is called before the first frame update
    void Start()
    {
        btn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ShowBtn());
    }
    IEnumerator ShowBtn()
    {
        yield return new WaitForSeconds(3);
        btn.SetActive(true);
    }

    public void Playlevel()
    {
        SceneManager.LoadScene(3);
    }
}
