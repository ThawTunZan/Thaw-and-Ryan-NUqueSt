using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFainted : MonoBehaviour
{
    public GameObject globalVolume;
    private ClockManager clockManager;
    public Animator transition;
    public Animator playerAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        clockManager = globalVolume.GetComponent<ClockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clockManager.hours > 23)
        {
            GoBackHome();
        }
    }

    public void GoBackHome()
    {
        StartCoroutine(WaitAnimation());
    }

    IEnumerator WaitAnimation()
    {
        playerAnimation.Play("Base Layer.player_death", 0, 0);
        transition.SetTrigger("Fainted");

        yield return new WaitForSeconds(1);

        DataPersistenceManager.instance.LoadGame();

        SceneManager.LoadScene("PlayerHouse", LoadSceneMode.Single);
    }
}
