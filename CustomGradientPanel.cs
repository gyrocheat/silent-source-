using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_Design_by_Fxbrrii
{
    [DefaultProperty("GradientColor1")]
    public class CustomGradientBorderPanel : Guna2Panel
    {
        private Color gradientColor1 = Color.FromArgb(6, 246, 254); // Cyan
        private Color gradientColor2 = Color.FromArgb(0, 0, 0, 0); // Transparent
        private int borderWidth = 3;
        private Color fillColor = Color.FromArgb(25, 25, 25);

        public CustomGradientBorderPanel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.FillColor = Color.FromArgb(25, 25, 25);
            this.BorderStyle = DashStyle.Solid;
            this.Padding = new Padding(0, borderWidth, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                path.AddRectangle(rect);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Fill the background
                using (SolidBrush fillBrush = new SolidBrush(fillColor))
                {
                    e.Graphics.FillPath(fillBrush, path);
                }

                // Create the centered gradient for the top border
                int halfWidth = this.Width / 2;

                // Left half of the gradient
                using (LinearGradientBrush leftBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(halfWidth, 0),
                    gradientColor2,
                    gradientColor1))
                {
                    using (Pen leftPen = new Pen(leftBrush, borderWidth))
                    {
                        e.Graphics.DrawLine(leftPen, 0, 0, halfWidth, 0);
                    }
                }

                // Right half of the gradient
                using (LinearGradientBrush rightBrush = new LinearGradientBrush(
                    new Point(halfWidth, 0),
                    new Point(this.Width, 0),
                    gradientColor1,
                    gradientColor2))
                {
                    using (Pen rightPen = new Pen(rightBrush, borderWidth))
                    {
                        e.Graphics.DrawLine(rightPen, halfWidth, 0, this.Width, 0);
                    }
                }
            }
        }

        [Category("Appearance")]
        [Description("The main color of the gradient border (center color)")]
        public Color GradientColor1
        {
            get { return gradientColor1; }
            set
            {
                gradientColor1 = value;
                this.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The end color of the gradient border (transparent edges)")]
        public Color GradientColor2
        {
            get { return gradientColor2; }
            set
            {
                gradientColor2 = value;
                this.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The width of the top border")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                this.Padding = new Padding(0, value, 0, 0);
                this.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The fill color of the panel")]
        public new Color FillColor
        {
            get { return fillColor; }
            set
            {
                fillColor = value;
                this.Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The background color of the panel (supports transparency)")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.Invalidate();
            }
        }
    }
}
