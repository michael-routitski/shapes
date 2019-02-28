using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformEx  {

	public static Transform ActivateGameObject(this Transform _this){

		_this.gameObject.SetActive(true);
		return _this;
	}
	
	public static Transform SetX(this Transform _this, float X){

		_this.localPosition = Vector3.zero;
		_this.Translate(X, 0, 0);
		return _this;
	}
	
	public static Transform RotateZ(this Transform _this, float Z){

		_this.Rotate(0, 0, Z);
		return _this;
	}
	
	public static Transform RotateX(this Transform _this, float X){

		_this.Rotate(X, 0, 0);
		return _this;
	}
	
	public static Transform SetZ(this Transform _this, float Z){

		_this.localPosition = Vector3.zero;
		_this.Translate(0, 0, Z);
		return _this;
	}
	
	public static Transform ParentTo(this Transform _this, Transform parent){

		_this.SetParent(parent);
		return _this;
	}
}
