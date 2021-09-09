using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcuteSingleMode : MonoBehaviour {


	private void Start () {

		//调用单例对象中的函数
		SingleModeExample.Instance.Fun();

	}
	

	
}
