using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xuly
{
    public partial class thongkechitiet : Form
    {
        public thongkechitiet()
        {
            InitializeComponent();
        }
        public int idtk;
        public decimal tygia;
        cobacEntities cobacEntities = new cobacEntities();
        private void thongkechitiet_Load(object sender, EventArgs e)
        {
            var _grv = cobacEntities.tinhtong(idtk, tygia);
            gc_thongke.DataSource = _grv;
        }

        private void gv_thongke_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
