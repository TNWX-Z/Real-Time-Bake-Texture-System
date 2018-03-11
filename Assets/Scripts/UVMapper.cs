using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace TNWX.Tools.BakeTextureSystem
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class UVMapper : MonoBehaviour
    {
        public Mesh m_mesh;
        private Vector3[] result_uv_pos1;
        private Vector3[] result_normal;
        private Vector2[] result_selfUV;
        private int[] result_triangle_index;

        public Mesh result_mesh;

        public Material result_material;
        private MeshFilter result_meshfilter;
        private MeshRenderer result_renderer;

        private Texture2D result_tex;

        private void initNewModel()
        {
            this.result_uv_pos1 = new Vector3[m_mesh.uv.Length];
            for (int i = 0; i < this.result_uv_pos1.Length; i++)
            {
                Vector2 uv_pos = m_mesh.uv[i];
                this.result_uv_pos1[i] = new Vector3(uv_pos.x, uv_pos.y, 0);
            }
            this.result_normal = this.m_mesh.normals;
            this.result_selfUV = this.m_mesh.uv;
            this.result_triangle_index = this.m_mesh.triangles;

            this.result_mesh = new Mesh();
            this.result_mesh.name = "唔哟！";
            this.result_mesh.vertices = this.result_uv_pos1;
            this.result_mesh.uv = this.result_selfUV;
            this.result_mesh.normals = this.result_normal;
            this.result_mesh.triangles = this.result_triangle_index;
        }

        void Start()
        {
            initNewModel();
            this.result_meshfilter = GetComponent<MeshFilter>();
            this.result_meshfilter.mesh = this.result_mesh;
            this.result_renderer = GetComponent<MeshRenderer>();
            this.result_renderer.material = this.result_material;

            this.result_tex = null;
        }


        public float _Radius = 0;
        void Update()
        {
            for (int i = 0; i < this.result_uv_pos1.Length; i++)
            {
                //this.result_mesh.vertices[i] *= (1 + _Radius);
            }
        }

        void Awake()
        {
            drawMesh = new Mesh();
            drawVertexs = new Vector3[0];
        }

#region Show
        [Header("Setting Values")]
        [Range(0.003f,0.02f)]
        public float pointRadius = 0.02f;
        private Mesh drawMesh;
        private Vector3[] drawVertexs;


        void drawTriangle(Vector3[] _vertexs)
        {
            for (int i = 0; i < _vertexs.Length; i+=3)
            {
                //Gizmos.DrawSphere(_vertexs[i], pointRadius);
                Gizmos.DrawLine(_vertexs[i], _vertexs[i + 1]);
                Gizmos.DrawLine(_vertexs[i + 1], _vertexs[i + 2]);
                Gizmos.DrawLine(_vertexs[i + 2], _vertexs[i]);
            }
        }
        /*
        void OnDrawGizmos()
        {
            if (!m_mesh)
                return;
            Gizmos.color = new Color(0.2f,0.3f,0.1f);
            if (drawVertexs.Length != m_mesh.triangles.Length)
            {
                drawVertexs = new Vector3[m_mesh.triangles.Length];
                for (int i = 0; i < drawVertexs.Length; i++)
                {
                    Vector2 uv_pos = this.m_mesh.uv[this.m_mesh.triangles[i]];
                    drawVertexs[i] = new Vector3(uv_pos.x, uv_pos.y, 0f);
                }
            }
            drawTriangle(drawVertexs);

        }
        //*/
#endregion 
    }
}