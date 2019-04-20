using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGraph : MonoBehaviour
{
    private Dictionary<Vector2, List<Vector2>> V = new Dictionary<Vector2, List<Vector2>>();

    public GameObject marker;
    void Start()
    {
        Collider2D[] colls = GameObject.FindObjectsOfType<Collider2D>() as Collider2D[];
        foreach (Collider2D c in colls) {
            if (c is BoxCollider2D) {
                BoxCollider2D bc = c as BoxCollider2D;
                float scaleX = c.transform.localScale.x;
                float scaleY = c.transform.localScale.y;
                float offset = 0.25f;

                Vector2 p = c.transform.TransformPoint(bc.size/2 + Vector2.one/c.transform.localScale*offset);
                V.Add(p, new List<Vector2>());

                p = c.transform.TransformPoint(-bc.size/2 - Vector2.one/c.transform.localScale*offset);
                V.Add(p, new List<Vector2>());

                p = c.transform.TransformPoint(new Vector2(-bc.size.x/2 - offset/scaleX, bc.size.y/2 + offset/scaleY));
                V.Add(p, new List<Vector2>());

                p = c.transform.TransformPoint(new Vector2(bc.size.x/2 + offset/scaleX, -bc.size.y/2 - offset/scaleY));
                V.Add(p, new List<Vector2>());
            } else {
                Bounds b = new Bounds(c.bounds.center, c.bounds.size);
                b.Expand(0.5f);
                V.Add(new Vector2(b.center.x + b.extents.x, b.center.y + b.extents.y), new List<Vector2>());
                V.Add(new Vector2(b.center.x - b.extents.x, b.center.y + b.extents.y), new List<Vector2>());
                V.Add(new Vector2(b.center.x + b.extents.x, b.center.y - b.extents.y), new List<Vector2>());
                V.Add(new Vector2(b.center.x - b.extents.x, b.center.y - b.extents.y), new List<Vector2>());
            }
        }

        foreach (Vector2 v1 in V.Keys) {
            foreach (Vector2 v2 in V.Keys) {
                RaycastHit2D hit = Physics2D.Linecast(v1, v2);
                if (hit.collider == null) {
                    V[v1].Add(v2);
                    Debug.DrawLine(v1, v2, Color.red, 10.0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<Vector2> getPath(Vector2 start, Vector2 end) {
        List<Vector2> path = new List<Vector2>();
        return path;
    }
}
