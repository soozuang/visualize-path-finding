using UnityEngine;
using System.Collections;

public class NodeVisualization : MonoBehaviour {

	public TextMesh gScore;
	public TextMesh hScore;
	public TextMesh fScore;
	public MeshRenderer color;
	public Transform parent;
	private Node onNode;

	void Start () {
		onNode = Map.map.GetNodeFromLocation(transform.position);
		InvokeRepeating("Visualize", 0, .25f);
	}


	void Visualize () {

		DisableEnableRenderers();
		if(Map.map.selectedNode == onNode){
			gScore.text = "";
			hScore.text = "";
			fScore.text = "";
			color.GetComponent<Renderer>().material.color = new Color(1,.5f,.25f); // orange
			return;
		}



		if(!onNode.isWalkable)
		{
			color.GetComponent<Renderer>().material.color = Color.black;
			gScore.text = "";
			hScore.text = "";
			fScore.text = "";
			parent.position = new Vector3(0,-22,0);

			return;
		}

		if(Map.map.RunSequentialAStar)
		{
			gScore.text = "G:"+onNode.gScore;
			hScore.text = "H:"+(onNode.fScore-onNode.gScore);
			fScore.text = "F:"+onNode.fScore;

			if (onNode.isInClosedSet)
				color.GetComponent<Renderer>().material.color = Color.red;
			else if(onNode.isInOpenSet)
				color.GetComponent<Renderer>().material.color = Color.magenta;

			if(onNode.parent != null){
				parent.LookAt(onNode.parent.unityPosition);
				parent.rotation = Quaternion.Euler( new Vector3(90,parent.rotation.eulerAngles.y,parent.rotation.eulerAngles.z));
				parent.position = new Vector3 (onNode.unityPosition.x, onNode.unityPosition.y+2, onNode.unityPosition.z);
			}

		}
		

		if(onNode.isCurrent){
			color.GetComponent<Renderer>().material.color = Color.yellow;
			return;
		}


	}

	public void DisableEnableRenderers()
	{
		bool isInThreadOpenSet = false;

		if(!onNode.isInOpenSet && !isInThreadOpenSet && !onNode.isInClosedSet && onNode.isWalkable && !onNode.isCurrent && !(Map.map.selectedNode == onNode))
		{
			gScore.GetComponent<Renderer>().enabled = false;
			hScore.GetComponent<Renderer>().enabled = false;
			fScore.GetComponent<Renderer>().enabled = false;
			color.GetComponent<Renderer>().enabled = false;
			parent.position = new Vector3(0,-22,0);
		}
		else{
			gScore.GetComponent<Renderer>().enabled = true;
			hScore.GetComponent<Renderer>().enabled = true;
			fScore.GetComponent<Renderer>().enabled = true;
			color.GetComponent<Renderer>().enabled = true;
		}
	}
}
