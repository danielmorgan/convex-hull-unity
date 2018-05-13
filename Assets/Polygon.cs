using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Polygon : MonoBehaviour {
	public List<Vector3> points = new List<Vector3>();
	public GameObject vertex;
	public GameObject edge;

	private List<GameObject> vertices = new List<GameObject>();
    private List<GameObject> edges = new List<GameObject>();
    private List<GameObject> permanentEdges = new List<GameObject>();

	private void Awake () {
		this.points.Clear();
		for (int i = 0; i < 10; i++) {
			this.points.Add(new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-5f, 5f)));
		}
	}

	private void Start ()
	{
		for (int i = 0; i < this.points.Count; i++) {
			Vector3 p = this.points[i] + this.transform.position;
			Vector3 pSub1 = this.points[Mathf.Max(0, i - 1)] + this.transform.position;

			this.DrawVertex(p);
		}

		SlowConvexHull algo = new SlowConvexHull(this);
		StartCoroutine(algo.Run());
	}

	public GameObject DrawVertex (Vector3 point)
	{
		GameObject vertex = Instantiate<GameObject>(this.vertex, this.transform);
		vertex.transform.position = point;

		this.vertices.Add(vertex);

		return vertex;
	}

	public GameObject DrawEdge (Vector3 start, Vector3 end, Color? endColor = null, Color? startColor = null, bool permanent = false)
	{
		GameObject edge = Instantiate<GameObject>(this.edge, this.transform);
		edge.transform.position = this.transform.position;

		LineRenderer lr = edge.GetComponent<LineRenderer>();
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);

		lr.startColor = startColor ?? Color.white;
		lr.endColor = endColor ?? Color.red;

		if (permanent) {
			this.permanentEdges.Add(edge);
		} else {
			this.edges.Add(edge);
		}

		return edge;
	}

	public void ClearEdges (int offset = 0)
	{
		for (int i = 0; i < this.edges.Count - offset; i++) {
			Destroy(this.edges[i]);
		}
	}

	private void OnDrawGizmos ()
	{
		if (this.points == null) {
			return;
		}

		for (int i = 0; i < this.points.Count; i++) {
			Vector3 p = this.points[i] + this.transform.position;
			Vector3 pSub1 = this.points[Mathf.Max(0, i - 1)] + this.transform.position;

			Gizmos.color = Color.black;
			Gizmos.DrawSphere(p, 0.3f);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pSub1, p);
		}

	}
}
