using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierGenerator : MonoBehaviour {

	// static BarrierGenerator(){
	// 	Materials = new Material[]{
	// 		Resources.Load<Material>("Holes/chocolate_dark"),
	// 		Resources.Load<Material>("Holes/chocolate_milk"),
	// 		Resources.Load<Material>("Holes/chocolate_white")
	// 	};
	// }

	const float BlockWidth = 2.4f;

	//static Material[] Materials;

	int NumLanes;

	public int Step;

	public bool NeedsGenerating;

	public bool NeedsReleasing;

	private static Dictionary<string, Stack<GameObject>> Cache;

	private Transform HolesParent;

	Material material;

	// Use this for initialization
	public void Reset (int numLanes) {
		SetNumLanes(numLanes);
		InitCache();
		HolesParent = transform.Find("Holes");
		ShapeIndexSelector = new int[NumLanes];
	}

	public void SetMaterial(Material value)
	{
		this.material = value;
	}

	public void SetNumLanes(int lanes){
		NumLanes = lanes;
	}

	public int RamdomShapeIndex = -1;

	//public int RandomRotationIndex = 0;

	private int materialIndex;

	private int[] ShapeIndexSelector;

	public void Generate(Material material){

		Release();

		int holeShapeIndex = 0;

		//RandomRotationIndex = Random.Range(0,3);

		//materialIndex = Random.Range(0,3);

		List<int> availableHoleShapeIndexes = new List<int>(Shape.List.Length);
		for (int i = 0; i < Shape.List.Length; i++)
			availableHoleShapeIndexes.Add(i);

		for (int laneIndex = 0; laneIndex < NumLanes; laneIndex++)
		{
			holeShapeIndex = availableHoleShapeIndexes[Random.Range(0, availableHoleShapeIndexes.Count - 1)];
			availableHoleShapeIndexes.Remove(holeShapeIndex);

			var holeT = GetHole(holeShapeIndex).transform;
				
			holeT.ParentTo(HolesParent);
				
			holeT.ActivateGameObject();
			holeT.SetX(GetXFor(laneIndex));
			
			holeT.RotateZ(90f * Random.Range(0,3));

			holeT.GetComponentInChildren<MeshRenderer>().sharedMaterial = material;

			ShapeIndexSelector[laneIndex] = holeShapeIndex;
		}

		RamdomShapeIndex = ShapeIndexSelector[Random.Range(0, ShapeIndexSelector.Length)];
	}

	int GetNextRandomOrdinal(){
		return Random.Range(1, Shape.List.Length);
	}

	void Release(){

 		for (int i = HolesParent.childCount - 1; i >= 0; i--){
			
			 //HolesParent.GetChild(i).localPosition = Vector3.zero;
			HolesParent.GetChild(i).localRotation = Quaternion.identity;
			HolesParent.GetChild(i).gameObject.SetActive(false);

			Cache[HolesParent.GetChild(i).name].Push(HolesParent.GetChild(i).gameObject);

		}

		while(HolesParent.childCount > 0){

			HolesParent.GetChild(0).transform.SetParent(null);
		}
	}

	float GetXFor(int index){

		return -(float)(NumLanes * BlockWidth) / 2.0f + (float)index * (BlockWidth) + BlockWidth / 2.0f;
	}
 
	GameObject LoadPrefab(int ordinal){

		var prefabData = Resources.Load(Shape.List[ordinal].PrefabPath);
		var hole = GameObject.Instantiate(prefabData) as GameObject;

		hole.name = Shape.List[ordinal].PrefabPath;
		
		return hole; 
	}

	GameObject GetHole(int ordinal){
		
		if (Cache[Shape.List[ordinal].PrefabPath].Count > 0){
			return Cache[Shape.List[ordinal].PrefabPath].Pop();
		}
		else {
			return LoadPrefab(ordinal);
		}
	}

	static void InitCache(){

		if (Cache == null){
			Cache = new Dictionary<string, Stack<GameObject>>();
		}

		for (int i = 0; i < Shape.List.Length; i++)
		{
			if (!Cache.ContainsKey(Shape.List[i].PrefabPath)){
				Cache.Add(Shape.List[i].PrefabPath, new Stack<GameObject>());
			}
		}
	}
}
