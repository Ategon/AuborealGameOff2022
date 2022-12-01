using Assets.EventSystem;

public class EquipmentPickupEventParameters : EventParameters
{
    public string equipmentName;
    public string equipmentDescription;

    public EquipmentPickupEventParameters(string equipmentName, string equipmentDescription)
    {
        this.equipmentName = equipmentName;
        this.equipmentDescription = equipmentDescription;
    }
}
