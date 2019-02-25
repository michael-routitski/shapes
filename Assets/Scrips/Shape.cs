using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape {

	public static Shape[] List = {
		new Shape() { 
			PrefabPath = "Holes/hole_1", 
			Matrix = new int[]{
				1,0,1,
				1,1,1,
				1,0,1
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_2", 
			Matrix = new int[]{
				1,1,1,
				1,0,1,
				1,0,1
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_3", 
			Matrix = new int[]{
				0,1,1,
				0,1,0,
				1,1,1
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_4", 
			Matrix = new int[]{
				0,0,1,
				1,1,1,
				1,0,1
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_5", 
			Matrix = new int[]{
				0,1,0,
				1,1,1,
				1,1,0
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_6", 
			Matrix = new int[]{
				0,1,0,
				1,1,1,
				1,0,1
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_7", 
			Matrix = new int[]{
				0,1,1,
				1,1,1,
				1,1,0
			}
		},
		new Shape() { 
			PrefabPath = "Holes/hole_8", 
			Matrix = new int[]{
				0,1,0,
				1,1,1,
				1,1,1
			}
		}
	};

	public string PrefabPath;

	public int[] Matrix;
}
