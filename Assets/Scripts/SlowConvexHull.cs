using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowConvexHull {
    private Polygon poly;
    private List<Vector3> convexEdges = new List<Vector3>();

    public SlowConvexHull (Polygon polygon)
    {
        this.poly = polygon;
    }

    public IEnumerator Run ()
    {
		WaitForSeconds wait = new WaitForSeconds(0.05f);

        for (int p = 0; p < this.poly.points.Count; p++) {
            for (int q = 0; q < this.poly.points.Count; q++) {
                if (p == q) {
                    continue;
                }
                Vector3 P = this.poly.points[p];
                Vector3 Q = this.poly.points[q];
                this.poly.ClearEdges();
                this.poly.DrawEdge(P, Q, Color.red);
                bool valid = true;
                yield return wait;

                for (int r = 0; r < this.poly.points.Count; r++) {
                    if ((r == p || r == q)) {
                        continue;
                    }
                    Vector3 R = this.poly.points[r];
                    if (this.IsLeftOfLine(P, Q, R)) {
                        valid = false;
                        continue;
                    }
                    // this.poly.DrawEdge(P, R, Color.blue);
                    // this.poly.DrawEdge(Q, R, Color.blue);
                    // yield return wait;
                }

                if (valid) {
                    this.poly.DrawEdge(P, Q, Color.green, Color.green, true);
                    this.convexEdges.Add(P);
                    this.convexEdges.Add(Q);
                }
            }
        }

        this.poly.ClearEdges();
    }

	private bool IsLeftOfLine (Vector3 P, Vector3 Q, Vector3 R)
	{
        float determinant = (P.x - R.x) * (Q.y - R.y) - (P.y - R.y) * (Q.x - R.x);

		return determinant > 0;
	}
}