using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowConvexHull {
    private Polygon P;
    private List<Vector3> convexEdges = new List<Vector3>();

    public SlowConvexHull (Polygon polygon)
    {
        this.P = polygon;
    }

    public IEnumerator Run ()
    {
		WaitForSeconds wait = new WaitForSeconds(0.005f);

        for (int p = 0; p < this.P.points.Count; p++) {
            for (int q = 1; q < this.P.points.Count; q++) {
                if (p == q) {
                    continue;
                }
                this.P.ClearEdges();
                this.P.DrawEdge(this.P.points[p], this.P.points[q], Color.red);
                bool valid = true;
                yield return wait;

                for (int r = 0; r < this.P.points.Count; r++) {
                    if ((r == p || r == q)) {
                        continue;
                    }
                    if (this.IsLeftOfLine(p, q, r)) {
                        valid = false;
                        continue;
                    }
                    this.P.DrawEdge(this.P.points[p], this.P.points[r], Color.blue);
                    this.P.DrawEdge(this.P.points[q], this.P.points[r], Color.blue);
                    yield return wait;
                }

                if (valid) {
                    this.P.DrawEdge(this.P.points[p], this.P.points[q], Color.green, Color.green, true);
                    this.convexEdges.Add(this.P.points[p]);
                    this.convexEdges.Add(this.P.points[q]);
                }
            }
        }

        this.P.ClearEdges();
    }

	private bool IsLeftOfLine (int p, int q, int r)
	{
        Vector3 P = this.P.points[p];
        Vector3 Q = this.P.points[q];
        Vector3 R = this.P.points[r];

        float determinant = (P.x - R.x) * (Q.y - R.y) - (P.y - R.y) * (Q.x - R.x);

		return determinant > 0;
	}
}