public interface IInteractable
{
    void Interact();
    void Interact2();
    void CancelInteract();
    string GetInteractText(); // opsional, misalnya untuk UI prompt

    void EnteringArea(); 
}