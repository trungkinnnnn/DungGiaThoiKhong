public class NpcToShop : NpcController
{
    protected override void HandleAction()
    {
        _uiController.ToShop();
    }
}
