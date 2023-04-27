using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Global.Helper
{
    public class MPSTab : TabControl
    {
        private int IconWOrH=14;
        private int offset=3;
        public MPSTab()
            : base()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            ////资源管理"typeof(Form1)"指定为Form1.resx,可以改成其他的  
            ////ResourceManager rm = new ResourceManager(typeof(MPS_FrmMain));
            ////icon = (Bitmap)(rm.GetObject("MPSImage1"));
            ////类型转换   
            //#region
            //int width = 16;
            //Bitmap b = new Bitmap(width, width);
            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < width; j++)
            //    {
            //        if (i == 0 || i == width - 1 || j == 0 || j == width - 1)
            //        {
            //            b.SetPixel(i, j, Color.FromArgb(0, 0, 0));
            //        }
            //    }

            //    for (int j = 0; j < width; j++)
            //    {
            //        if (i == j || i + j == width - 1)
            //        {
            //            if (i != 1 && i != 2 && j != 1 && j != 2 && i != width - 3 && i != width - 2)
            //                b.SetPixel(i, j, Color.FromArgb(0, 0, 0));
            //        }
            //    }
            //}
            //#endregion

            //icon = b;
            //IconWOrH = icon.Width + 3;
            ////IconWOrH = icon.Height;
            ////IconWOrH = 8;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = GetTabRect(e.Index);
            if (e.Index == this.SelectedIndex)    //当前选中的Tab页，设置不同的样式以示选中
            {
                //Brush selected_color = Brushes.LightBlue; //选中的项的背景色
                //selected_color = Color.FromArgb(175, 210, 255) as Brush; //选中的项的背景色
                //Color color = Color.FromArgb(194, 217, 247);
                ////g.DrawRectangle(new Pen(color), r);
                //g.FillRectangle(new SolidBrush(color), r); //改变选项卡标签的背景色
                Brush selected_color = Brushes.SteelBlue; //选中的项的背景色
                g.FillRectangle(selected_color, r); //改变选项卡标签的背景色
                string title = this.TabPages[e.Index].Text;
                g.DrawString(title, this.Font, new SolidBrush(Color.White), new PointF(r.X, r.Y + 2));//PointF选项卡标题的位置
                r.Offset(r.Width - IconWOrH-offset-2, offset);
                //g.DrawImage(icon, new Point(r.X, r.Y));//选项卡上的图标的位置 fntTab = new System.Drawing.Font(e.Font, FontStyle.Bold);
                Pen Selected = new Pen(Brushes.White);
                g.DrawLine(Selected, new Point(r.X, r.Y), new Point(r.X+ IconWOrH, r.Y));
                g.DrawLine(Selected, new Point(r.X, r.Y), new Point(r.X , r.Y + IconWOrH));
                g.DrawLine(Selected, new Point(r.X+ IconWOrH, r.Y), new Point(r.X+ IconWOrH, r.Y + IconWOrH));
                g.DrawLine(Selected, new Point(r.X , r.Y + IconWOrH), new Point(r.X + IconWOrH, r.Y + IconWOrH));
                g.DrawLine(Selected, new Point(r.X+2, r.Y+2), new Point(r.X + IconWOrH-2, r.Y + IconWOrH-2));
                g.DrawLine(Selected, new Point(r.X + IconWOrH - 2, r.Y + 2), new Point(r.X + 2, r.Y + IconWOrH - 2));
            }
            else//非选中的
            {
                Brush selected_color = Brushes.LightGray; //选中的项的背景色
                g.FillRectangle(selected_color, r); //改变选项卡标签的背景色
                string title = this.TabPages[e.Index].Text;
                g.DrawString(title, this.Font, new SolidBrush(Color.Black), new PointF(r.X, r.Y + 2));//PointF选项卡标题的位置
                r.Offset(r.Width - IconWOrH - offset-2, offset);
                //g.DrawImage(icon, new Point(r.X, r.Y));//选项卡上的图标的位置 fntTab = new System.Drawing.Font(e.Font, FontStyle.Bold);
                Pen Selected = new Pen(Brushes.Black);
                g.DrawLine(Selected, new Point(r.X, r.Y), new Point(r.X + IconWOrH, r.Y));
                g.DrawLine(Selected, new Point(r.X, r.Y), new Point(r.X, r.Y + IconWOrH));
                g.DrawLine(Selected, new Point(r.X + IconWOrH, r.Y), new Point(r.X + IconWOrH, r.Y + IconWOrH));
                g.DrawLine(Selected, new Point(r.X, r.Y + IconWOrH), new Point(r.X + IconWOrH, r.Y + IconWOrH));
                g.DrawLine(Selected, new Point(r.X + 2, r.Y + 2), new Point(r.X + IconWOrH - 2, r.Y + IconWOrH - 2));
                g.DrawLine(Selected, new Point(r.X + IconWOrH - 2, r.Y + 2), new Point(r.X + 2, r.Y + IconWOrH - 2));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point point = e.Location;
            Rectangle r = GetTabRect(this.SelectedIndex);
            r.Offset(r.Width - IconWOrH-offset-2, offset);
            r.Width = IconWOrH;
            r.Height = IconWOrH;
            if (r.Contains(point))
            {
                this.TabPages.RemoveAt(this.SelectedIndex);
            }
        }
    }
}
