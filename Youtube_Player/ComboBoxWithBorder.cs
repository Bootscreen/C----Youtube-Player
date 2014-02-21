using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Player
{
	public class ComboBoxWithBorder : ComboBox
	{
		/*private Color _borderColor = Color.Black;
		private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
		private static int WM_PAINT = 0x000F; 

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == WM_PAINT)
			{
				Graphics g = Graphics.FromHwnd(Handle);
				Rectangle bounds = new Rectangle(0, 0, Width, Height);
				ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle);
			}
		}

		[Category("Appearance")]
		public Color BorderColor
		{
			get { return _borderColor; }
			set 
			{ 
				_borderColor = value;
				Invalidate(); // causes control to be redrawn
			}
		}

		[Category("Appearance")]
		public ButtonBorderStyle BorderStyle
		{
			get { return _borderStyle; }
			set 
			{ 
				_borderStyle = value;
				Invalidate();
			}
		}     */
		
        private Brush BorderBrush = new SolidBrush(SystemColors.WindowFrame);
        private Brush ArrowBrush = new SolidBrush(SystemColors.ControlText);
        private Brush DropButtonBrush = new SolidBrush(SystemColors.Control);

        private Color _ButtonColor = SystemColors.Control;
        public Color ButtonColor
        {
            get { return _ButtonColor; }
            set
            {
                _ButtonColor = value;
                DropButtonBrush = new SolidBrush(this.ButtonColor);
                this.Invalidate();
            }
        }

        private Color _borderColor = Color.DimGray;
        private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        //private static int WM_PAINT = 0x000F;

        [Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate(); // causes control to be redrawn
            }
        }

        [Category("Appearance")]
        public ButtonBorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                this.Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);



            /*if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle);
            }*/

            switch (m.Msg)
            {
                case 0xf:
                    //Paint the background. Only the borders
                    //will show up because the edit
                    //box will be overlayed
                    //Graphics g = Graphics.FromHwnd(Handle);
                    Graphics g = this.CreateGraphics();
                    Rectangle bounds = new Rectangle(0, 0, Width, Height);
                    ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle);

                    //Pen p = new Pen(Color.White, 2);
                    //g.FillRectangle(BorderBrush, this.ClientRectangle);

                    //Draw the background of the dropdown button
                    Rectangle rect = new Rectangle(this.Width - 18, 0, 18, this.Height);
                    g.FillRectangle(DropButtonBrush, rect);

                    //Create the path for the arrow
                    System.Drawing.Drawing2D.GraphicsPath pth = new System.Drawing.Drawing2D.GraphicsPath();
                    PointF TopLeft = new PointF(this.Width - 13, (this.Height - 5) / 2);
                    PointF TopRight = new PointF(this.Width - 6, (this.Height - 5) / 2);
                    PointF Bottom = new PointF(this.Width - 9, (this.Height + 2) / 2);
                    pth.AddLine(TopLeft, TopRight);
                    pth.AddLine(TopRight, Bottom);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //Determine the arrow's color.
                    if (this.DroppedDown)
                    {
                        ArrowBrush = new SolidBrush(SystemColors.HighlightText);
                    }
                    else
                    {
                        ArrowBrush = new SolidBrush(SystemColors.ControlText);
                    }

                    //Draw the arrow
                    g.FillPath(ArrowBrush, pth);

                    g.Dispose();

                    break;
            }
        }

        //Override mouse and focus events to draw
        //proper borders. Basically, set the color and Invalidate(),
        //In general, Invalidate causes a control to redraw itself.
        //#region "Mouse and focus Overrides"
        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            //BorderBrush = new SolidBrush(SystemColors.Highlight);
            //EnableDoubleBuffering();
            this.Invalidate();
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            /*if (this.Focused)
                return;*/
            BorderBrush = new SolidBrush(SystemColors.WindowFrame);
            //EnableDoubleBuffering();
            this.Invalidate();
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            //BorderBrush = new SolidBrush(SystemColors.Window);
            //EnableDoubleBuffering();
            this.Invalidate();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            BorderBrush = new SolidBrush(SystemColors.WindowFrame);
            //EnableDoubleBuffering();
            this.Invalidate();
        }

        protected override void OnMouseHover(System.EventArgs e)
        {
            base.OnMouseHover(e);
            BorderBrush = new SolidBrush(SystemColors.WindowFrame);
            //EnableDoubleBuffering();
            this.Invalidate();
        }
        //#endregion
    }
}
