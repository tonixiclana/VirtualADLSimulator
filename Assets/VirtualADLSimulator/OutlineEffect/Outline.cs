using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace cakeslice
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class Outline : MonoBehaviour
    {
        public Renderer Renderer { get; private set; }

        public int color;
        public int previousColor;
        public bool eraseRenderer;
        public List<string> layers = new List<string>();

        [HideInInspector]
        public int originalLayer;
        [HideInInspector]
        public Material[] originalMaterials;

        bool isEnabled;

        private void Awake()
        {
            Renderer = GetComponent<Renderer>();
            previousColor = color;
        }

        public void changeColor(int color)
        {
            previousColor = this.color;
            this.color = color;
        }

        public void resetPreviousColor()
        {
            int c = color;
            color = previousColor;
            previousColor = c;
        }

        void OnEnable()
        {
			IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
				.Select(c => c.GetComponent<OutlineEffect>())
				.Where(e => e != null);

			foreach (OutlineEffect effect in effects)
            {
                effect.AddOutline(this);
            }
        }

        void OnDisable()
        {
			IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
				.Select(c => c.GetComponent<OutlineEffect>())
				.Where(e => e != null);

			foreach (OutlineEffect effect in effects)
            {
                effect.RemoveOutline(this);
            }
        }
    }
}