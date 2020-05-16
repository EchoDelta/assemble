namespace Assemble.Desktop.Components
{
    class Zoomable
    {
        public Zoomable(float maxZoom, float minZoom, float initialZoom = 1.0f)
        {
            MaxZoom = maxZoom;
            MinZoom = minZoom;
            Zoom = initialZoom;
        }

        private float _zoom = 1.0f;
        public float Zoom
        {
            get => _zoom;
            set
            {
                if (value > MaxZoom)
                {
                    _zoom = MaxZoom;
                }
                else if(value < MinZoom)
                {
                    _zoom = MinZoom;
                }
                else
                {
                    _zoom = value;
                }
            }
        }

        public float MaxZoom { get; set; }
        public float MinZoom { get; set; }
    }
}
