using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace WinFormsHtmlNotification
{
    public class FloatingHtmlWindow : FloatingWindow
    {
        private Bitmap _bitmap = null;
        private Image _image = null;
        private System.Windows.Forms.Timer _viewClock;

        public void SetImage(Bitmap image)
        {
            _bitmap = image;
        }

        public void SetHtml(string html, bool autoSize = false)
        {
            var image = HtmlRender.RenderToImage(html);
            if (autoSize == true)
                _image = HtmlRender.RenderToImage(html, image.Height, image.Width);
            else
                _image = HtmlRender.RenderToImage(html, _size);
        }

        public int GetHtmlHeight(string html) => HtmlRender.RenderToImage(html).Height;

        public void Show(int time)
        {
            if (_viewClock != null)
            {
                _viewClock.Stop();
                _viewClock.Dispose();
            }

            base.Show();

            _viewClock = new System.Windows.Forms.Timer();
            _viewClock.Tick += (o, e) =>
            {
                _viewClock.Stop();
                _viewClock.Dispose();
                Close();
            };
            _viewClock.Interval = time;
            _viewClock.Start();
        }

        protected override void PerformPaint(PaintEventArgs e)
        {
            if (base.Handle == IntPtr.Zero)
                return;

            e.Graphics.DrawImage(_image, 0, 0);
        }

        //protected void viewTimer(object sender, System.EventArgs e)
        //{
        //    this._viewClock.Stop();
        //    this._viewClock.Dispose();
        //    this.Close();
        //}
    }
}