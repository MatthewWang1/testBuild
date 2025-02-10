using UnityEngine;

public interface Interactable_Interface
{
    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor);
}
