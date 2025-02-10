using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance {get; private set;}

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.onRecipeSuccess    += DeliverManager_OnRecipeSuccess;
        DeliveryManager.Instance.onRecipeFail       += DeliverManager_OnRecipeFail;
        Player.Instance.OnPickedSomething           += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere           += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed             += TrashCounter_OnAnyObjectPlacedHere;
    }

    private void TrashCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.ObjectPickup, Player.Instance.transform.position);
    }

    private void DeliverManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void DeliverManager_OnRecipeFail(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 2f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 2f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0,audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
}
