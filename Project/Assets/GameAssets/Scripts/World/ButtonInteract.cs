
public class ButtonInteract : InteractObj
{
    public InteractObj InteractObject;

    public override void OnInteractObject()
    {
        base.OnInteractObject();
        InteractObject?.OnInteractObject();
    }
}
