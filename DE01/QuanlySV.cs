using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DE01
{
    public partial class frmSV : Form
    {
        public frmSV()
        {
            InitializeComponent();
            SVContextDB context = new SVContextDB();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("BẠN MUỐN THOÁT KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void frmSV_Load(object sender, EventArgs e)
        {
            try
            {
                SVContextDB context = new SVContextDB();
                List<LOP> listLs = context.LOPs.ToList();
                List<SINHVIEN> listSVs = context.SINHVIENs.ToList();
                FillData(listLs);
                BindGrid(listSVs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FillData(List<LOP> listLs)
        {
            this.cboLop.DataSource = listLs;
            cboLop.DisplayMember = "TENLOP";
            cboLop.ValueMember = "MALOP";
            cboLop.SelectedIndex = 0;
        }

        private void BindGrid(List<SINHVIEN> listSVs)
        {
            lvSV.Items.Clear();
            foreach (var sv in listSVs)
            {
                ListViewItem lv = new ListViewItem(sv.MASV);
                lv.SubItems.Add(sv.HOTENSV);
                lv.SubItems.Add(sv.NGAYSINH.ToString());
                lv.SubItems.Add(sv.LOP.TENLOP);
                lvSV.Items.Add(lv);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SVContextDB context = new SVContextDB();
                SINHVIEN add = context.SINHVIENs.FirstOrDefault(s => s.MASV == txtMaSV.Text);

                if (add == null)
                {
                    SINHVIEN s = new SINHVIEN()
                    {
                        MASV = txtMaSV.Text,
                        HOTENSV = txtHoTenSV.Text,
                        NGAYSINH = dtNgaySinh.Value,
                        MALOP = (cboLop.SelectedItem as LOP).MALOP
                    };
                    context.SINHVIENs.Add(s);
                    context.SaveChanges();
                    BindGrid(context.SINHVIENs.ToList());
                    MessageBox.Show("THÊM SV THÀNH CÔNG!!!");
                }
                else
                {
                    MessageBox.Show("ĐÃ TỒN TẠI SV NÀY!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SVContextDB context = new SVContextDB();
                SINHVIEN delete = context.SINHVIENs.FirstOrDefault(p => p.MASV == txtMaSV.Text);
                if (delete != null)
                {
                    context.SINHVIENs.Remove(delete);
                    context.SaveChanges();
                    BindGrid(context.SINHVIENs.ToList());
                    MessageBox.Show("XÓA THÀNH CÔNG!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                if (lvSV.SelectedItems.Count > 0)
                {
                    txtMaSV.Text = lvSV.SelectedItems[0].SubItems[0].Text;
                    txtHoTenSV.Text = lvSV.SelectedItems[0].SubItems[1].Text;
                    dtNgaySinh.Text = lvSV.SelectedItems[0].SubItems[2].Text;
                    cboLop.Text = lvSV.SelectedItems[0].SubItems[3].Text;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SVContextDB context = new SVContextDB();
                SINHVIEN update = context.SINHVIENs.FirstOrDefault(p => p.MASV == txtMaSV.Text);
                if (update != null)
                {
                    update.HOTENSV = txtHoTenSV.Text;
                    update.NGAYSINH = dtNgaySinh.Value;
                    update.MALOP = (cboLop.SelectedItem as LOP).MALOP;

                    context.SaveChanges();
                    BindGrid(context.SINHVIENs.ToList());
                    MessageBox.Show("SỬA THÀNH CÔNG!!!");
                }
                else
                {
                    MessageBox.Show("KHÔNG TÌM THẤY SV!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                SVContextDB context = new SVContextDB();
                string fullName = txtTimKiem.Text.Trim();
                var query = context.SINHVIENs.AsQueryable();
                if (!string.IsNullOrEmpty(fullName))
                    query = query.Where(s => s.HOTENSV.Contains(fullName));
                var results = query.ToList();
                BindGrid(results);
                MessageBox.Show("TÌM THÀNH CÔNG !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
