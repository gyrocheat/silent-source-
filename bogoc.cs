using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControls
{
    [DefaultProperty("BorderRadius")]
    [DefaultEvent("Paint")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class CustomPanel : Panel
    {
        #region Properties

        private int _borderRadius = 10;
        private Color _borderColor = Color.Silver;
        private float _borderSize = 1;
        private bool _topRoundedOnly = true;
        private Color _shadowColor = Color.DarkGray;
        private int _shadowDepth = 3;
        private bool _enableShadow = false;

        [Category("Border")]
        [Description("Bo góc của panel")]
        [DefaultValue(10)]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                Invalidate();
            }
        }

        [Category("Border")]
        [Description("Màu của viền")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        [Category("Border")]
        [Description("Độ dày của viền")]
        [DefaultValue(1f)]
        public float BorderSize
        {
            get { return _borderSize; }
            set
            {
                _borderSize = value;
                Invalidate();
            }
        }

        [Category("Border")]
        [Description("Chỉ bo góc phía trên")]
        [DefaultValue(true)]
        public bool TopRoundedOnly
        {
            get { return _topRoundedOnly; }
            set
            {
                _topRoundedOnly = value;
                Invalidate();
            }
        }

        [Category("Shadow")]
        [Description("Bật/tắt hiệu ứng đổ bóng")]
        [DefaultValue(false)]
        public bool EnableShadow
        {
            get { return _enableShadow; }
            set
            {
                _enableShadow = value;
                Invalidate();
            }
        }

        [Category("Shadow")]
        [Description("Màu của đổ bóng")]
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set
            {
                _shadowColor = value;
                Invalidate();
            }
        }

        [Category("Shadow")]
        [Description("Độ sâu của đổ bóng")]
        [DefaultValue(3)]
        public int ShadowDepth
        {
            get { return _shadowDepth; }
            set
            {
                _shadowDepth = value;
                Invalidate();
            }
        }

        #endregion

        public CustomPanel()
        {
            // Thiết lập mặc định
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Size = new Size(200, 100);
            this.Padding = new Padding(0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ đổ bóng nếu được bật
            if (_enableShadow)
                DrawShadow(g);

            // Tạo đường path để vẽ viền và nền với bo góc
            using (GraphicsPath path = GetRoundedRectPath())
            {
                // Vẽ nền
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    g.FillPath(brush, path);
                }

                // Vẽ viền
                if (_borderSize > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderSize))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        private GraphicsPath GetRoundedRectPath()
        {
            int width = this.Width;
            int height = this.Height;
            int radius = _borderRadius;
            GraphicsPath path = new GraphicsPath();

            if (_topRoundedOnly)
            {
                // Chỉ bo góc phía trên (border-radius: 10px 10px 0 0)
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // Góc trên-trái
                path.AddArc(width - (radius * 2), 0, radius * 2, radius * 2, 270, 90); // Góc trên-phải
                path.AddLine(width, height, 0, height); // Đường thẳng phía dưới
                path.CloseFigure();
            }
            else
            {
                // Bo tất cả các góc
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // Góc trên-trái
                path.AddArc(width - (radius * 2), 0, radius * 2, radius * 2, 270, 90); // Góc trên-phải
                path.AddArc(width - (radius * 2), height - (radius * 2), radius * 2, radius * 2, 0, 90); // Góc dưới-phải
                path.AddArc(0, height - (radius * 2), radius * 2, radius * 2, 90, 90); // Góc dưới-trái
                path.CloseFigure();
            }

            return path;
        }

        private void DrawShadow(Graphics g)
        {
            if (_shadowDepth <= 0) return;

            // Đặt shadow offset
            for (int i = 1; i <= _shadowDepth; i++)
            {
                using (GraphicsPath shadowPath = GetRoundedRectPath())
                {
                    shadowPath.Transform(new Matrix(1, 0, 0, 1, i, i));
                    using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                    {
                        Color shadowColor = Color.FromArgb(
                            (_shadowDepth - i + 1) * 15,
                            _shadowColor.R,
                            _shadowColor.G,
                            _shadowColor.B);

                        shadowBrush.CenterColor = shadowColor;
                        Color[] colors = { Color.Transparent };
                        shadowBrush.SurroundColors = colors;

                        g.FillPath(shadowBrush, shadowPath);
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}