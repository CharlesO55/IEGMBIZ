using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] List<Tree> trees;

    [SerializeField] GameObject treeTemplate;
    public void PlantTree()
    {
        int x  = Random.Range(0, 8);
        int y  = Random.Range(-2,2);

        GameObject o = Instantiate(treeTemplate, transform);
        o.transform.position = new Vector3(x, y, 0);

        if(o.TryGetComponent<Tree>(out Tree tree)){
            trees.Add(tree);
        }
    }

    public Tree RequestMatureTree()
    {
        return trees.Where(t => t.Data.IsMature()).First();
    }

    private void Start()
    {
        InvokeRepeating("PlantTree", 0, 5);
    }
}