using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisCacheBuilder
{
    public partial class SuggestGridSizeForm : Form
    {
        public double GridSize = 0;

        public SuggestGridSizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sz = int.Parse(this.tbxScreenSize.Text);
            int dpi = int.Parse(this.tbxDPI.Text);
            int scale = int.Parse(this.tbxScale.Text);
            int count = int.Parse(this.tbxCount.Text);
            this.GridSize = Math.Truncate( sz / dpi * 25.4 * 0.001 * scale / count);
        }
    }
}
