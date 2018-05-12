using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Polygon : MonoBehaviour {
	public List<Vector3> vertices = new List<Vector3>();
	public GameObject vertex;
	public GameObject edge;

	private void Awake () {
	}

	private void Start () {


		for (int i = 0; i < this.vertices.Count; i++) {
			Vector3 p = this.vertices[i] + this.transform.position;
			Vector3 pSub1 = this.vertices[Mathf.Max(0, i - 1)] + this.transform.position;

			this.DrawVertex(p);

			if (i > 0) {
				this.DrawEdge(pSub1, p);
			}
		}
	}

	private GameObject DrawVertex (Vector3 point)
	{
		GameObject vertex = Instantiate<GameObject>(this.vertex, this.transform);
		vertex.transform.position = point;

		return vertex;
	}

	private GameObject DrawEdge (Vector3 start, Vector3 end)
	{
		GameObject edge = Instantiate<GameObject>(this.edge, this.transform);
		edge.transform.position = this.transform.position;

		LineRenderer lr = edge.GetComponent<LineRenderer>();
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);

		return edge;
	}

	private void SlowConvexHull () {
		
	}

	private void OnDrawGizmos () {
		if (this.vertices == null) {
			return;
		}

		for (int i = 0; i < this.vertices.Count; i++) {
			Vector3 p = this.vertices[i];
			Vector3 pSub1 = this.vertices[Mathf.Max(0, i - 1)];

			Gizmos.color = Color.black;
			Gizmos.DrawSphere(p, 0.3f);
			// Gizmos.color = Color.red;
			// Gizmos.DrawLine(pSub1, p);
		}

	}
}
