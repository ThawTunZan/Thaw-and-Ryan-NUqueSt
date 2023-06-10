using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerFainted : MonoBehaviour
{
    public GameObject globalVolume;
    private ClockManager clockManager;
    public Animator transition;
    public Animator playerAnimation;
    GameObject originalGameObject;
    private float volumeHealthSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        clockManager = globalVolume.GetComponent<ClockManager>();
        originalGameObject = GameObject.Find("HealthBar");
        volumeHealthSlider = originalGameObject.GetComponent<Slider>().value;
        
    }

    // Update is called once per frame
    void Update()
    {
        volumeHealthSlider = originalGameObject.GetComponent<Slider>().value;
        if (clockManager.hours > 23 || volumeHealthSlider <= 0)
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
