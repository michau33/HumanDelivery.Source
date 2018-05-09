using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> {
	Dictionary<string, List<Component>> listeners = new Dictionary<string, List<Component>>();

	public void AddListener( Component sender, string eventName ) { 
		if(!listeners.ContainsKey(eventName))
			listeners.Add(eventName, new List<Component>());
		listeners[eventName].Add(sender);
	}	

	public void RemoveListener( Component sender, string eventName ) {
		if(!listeners.ContainsKey(eventName))
			return;
		
		for(int i = listeners[eventName].Count - 1; i >= 0; i--) {
			if(listeners[eventName][i].GetInstanceID() == sender.GetInstanceID()) {
				listeners[eventName].RemoveAt(i);		
			}
		}
	}

	public void PostNotification( Component sender, string eventName ) {
		if(!listeners.ContainsKey(eventName))
			return;

		foreach(Component listener in listeners[eventName]) {
			listener.SendMessage(eventName, sender, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void ClearListeners() {
		listeners.Clear();
	}

	public void RemoveRedundancies() {
		Dictionary<string, List<Component>> tmp = new Dictionary<string, List<Component>>();

		foreach(KeyValuePair<string, List<Component>> item in listeners) {
			for(int i = item.Value.Count - 1; i >= 0; i--) {
				if(item.Value[i] == null) {
					item.Value.RemoveAt(i);
				}
			}

			if(item.Value.Count > 0) {
				tmp.Add(item.Key, item.Value);
			}
		}
		
		listeners = tmp;
	}

	void OnLevelWasLoaded() {
		RemoveRedundancies();
	}
}