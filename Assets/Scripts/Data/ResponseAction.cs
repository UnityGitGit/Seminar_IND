using UnityEngine;
using UnityEngine.Events;

//als de response een text response is, dan geeft ie eerst een text terug en vervolgens kan de speler weer antwoorden
[CreateAssetMenu(fileName = "ResponseAction", menuName = "DataObject/ResponseAction", order = 1)]
public class ResponseAction : ScriptableObject {
	
	public int characterID;
	public string dialogText;

	public bool isFinalResponse;
	public bool showCharacter = true;

	public ResponseType responseType;
	public ResponseAction nextResponse;
	[SerializeField]private int nextPlayerChoiceStep = 1;

	//[SerializeField]private EventInvoker[] myEvents;
	[SerializeField]protected string[] eventNames;
	[SerializeField]protected string[] eventParams;

	public virtual void RespondAction (){
		if (isFinalResponse) {
			ResponseManager.instance.onDialogEnded += ActivateMyEvents;
			return;
		}

		if (responseType == ResponseType.PlayerResponse)
			ResponseManager.instance.onNPCResponseClickedAway += ActivatePlayerResponseChoice;
		else
			ResponseManager.instance.onNPCResponseClickedAway += ActivateNPCResponse;
	}

	void ActivatePlayerResponseChoice(){
		ResponseManager.instance.onNPCResponseClickedAway -= ActivatePlayerResponseChoice;
		ResponseManager.instance.ActivateNextPlayerChoice (nextPlayerChoiceStep);
	}

	void ActivateNPCResponse(){
		ResponseManager.instance.onNPCResponseClickedAway -= ActivateNPCResponse;
		ResponseManager.instance.ShowDialogTextNPC (nextResponse);
	}

	protected void ActivateMyEvents(){
		ResponseManager.instance.onDialogEnded -= ActivateMyEvents;
		for (int i = 0; i < eventNames.Length; i++) {
			EventManager.TriggerEvent (eventNames [i], eventParams [i]);
		}
	}
}

public enum ResponseType{
	ScriptedResponse,
	PlayerResponse
}