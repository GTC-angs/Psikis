public interface IInteractable
{
    void Interact();
    void CancelInteract();
    string GetInteractText(); // opsional, misalnya untuk UI prompt

    void EnteringArea(); 
}