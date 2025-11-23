using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButtonOptions : MonoBehaviour, IPointerClickHandler
{
    private GameObject inputManagerObject;
    private GameObject gameStateObject;
    private GameObject collidersManagerObject;
    private GameObject map;
    private PauseButtonLogic pauseButton;
    [SerializeField] private GameObject optionsMenu;
    private AudioSource SFXSource;
    [SerializeField] private AudioClip clip;

    void Start()
    {
        SFXSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inputManagerObject = GameObject.FindWithTag("InputManager");
        gameStateObject = GameObject.FindWithTag("GameState");
        collidersManagerObject = GameObject.FindWithTag("CollidersManager");
        map = GameObject.FindWithTag("Map");

        InputManager inputManager = inputManagerObject.GetComponent<InputManager>();
        GameState gameState = gameStateObject.GetComponent<GameState>();
        pauseButton = gameStateObject.GetComponent<PauseButtonLogic>();

        SFXSource.PlayOneShot(clip);
        inputManager.ResumeGame();
        gameState.ResumeGame();
        optionsMenu.SetActive(false);

        if (collidersManagerObject != null)
        {
            CollidersManager collidersManager = collidersManagerObject.GetComponent<CollidersManager>();
            collidersManager.EnableColliders();
        }

        if (pauseButton != null)
        {
            pauseButton.ShowOptionsButton();
        }

        if (map != null)
        {
            Map mapButton = map.GetComponent<Map>();
            mapButton.ShowMapButton();
        }
    } 
}
