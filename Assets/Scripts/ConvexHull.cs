using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexHull {
    private Polygon poly;
    private List<Vector3> convexEdges = new List<Vector3>();
    private List<Vector3> upperHull = new List<Vector3>();

    public ConvexHull (Polygon polygon)
    {
        this.poly = polygon;
    }

    public IEnumerator Run ()
    {
        this.poly.points.Sort((a, b) => a.x.CompareTo(b.x));

        List<Vector3> LUpper = new List<Vector3>() {
            this.poly.points[0],
            this.poly.points[1],
        };

        for (int i = 2; i < this.poly.points.Count; i++) {
            LUpper.Add(this.poly.points[i]);

            foreach (Vector3 v in LUpper) {
                this.poly.DrawVertex(v, Color.green);
                yield return new WaitForSeconds(0.33f);
            }

            if (LUpper.Count > 2 && !this.MakesRightTurn(LUpper)) {
                LUpper.Remove(LUpper[i - 1]);
                this.poly.ClearVertex(i - 1);
                // this.poly.DrawVertex(LUpper[i - 1], Color.red);
                yield return new WaitForSeconds(1f);
            }
        }

        this.poly.ClearVertices();

        yield return new WaitForSeconds(1f);
    }

	private bool IsLeftOfLine (Vector3 P, Vector3 Q, Vector3 R)
	{
        float determinant = (P.x - R.x) * (Q.y - R.y) - (P.y - R.y) * (Q.x - R.x);

		return determinant > 0;
	}

    private bool MakesRightTurn (List<Vector3> V)
    {
        int i = V.Count;
        Debug.Log(i);

        return this.IsLeftOfLine(V[i - 3], V[i - 2], V[i - 1]);
    }
}