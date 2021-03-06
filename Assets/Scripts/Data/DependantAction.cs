using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DependantAction", menuName = "DataObject/DependantAction", order = 1)]
public class DependantAction : ResponseAction {

	[SerializeField]private string requiredItem;

	[SerializeField]private string alternateEventName;
	[SerializeField]private string alternateEventParam;

	public override void RespondAction (){
		if (Inventory.instance.ItemInInventory (requiredItem)) {
			ResponseManager.instance.onDialogEnded += ActivateAlternateEvent;
		} else {
			ResponseManager.instance.onDialogEnded += ActivateMyEvents;
		}
	}

	void ActivateAlternateEvent(){
		ResponseManager.instance.onDialogEnded -= ActivateMyEvents;
		EventManager.TriggerEvent (alternateEventName, alternateEventParam);
	}
}