using System;
using UnityEngine;

public class Change
{
	private int gameObjectId;
	private string changeType;
	private Vector3 moveChangeValue;

	public Change (int gameObjectId, string changeType) {
		this.gameObjectId = gameObjectId;
		this.changeType = changeType;
		moveChangeValue = Vector3.zero;
	}

	public Change (int gameObjectId, string changeType, Vector3 moveChangeValue) {
		this.gameObjectId = gameObjectId;
		this.changeType = changeType;
		this.moveChangeValue = moveChangeValue;
	}

}

