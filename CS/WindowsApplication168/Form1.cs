using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Controls;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace SingleButtonClick
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                dataTable1.Rows.Add(new object[] { i, "Name" + i });
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (hi.InRowCell)
                {
                    if (hi.Column.RealColumnEdit.GetType() == typeof(RepositoryItemComboBox))
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        view.FocusedColumn = hi.Column;
                        view.ShowEditor();
                        //force button click 
                        ButtonEdit edit = (view.ActiveEditor as ComboBoxEdit);
                        Point p = view.GridControl.PointToScreen(e.Location);
                        p = edit.PointToClient(p);
                        EditHitInfo ehi = (edit.GetViewInfo() as ButtonEditViewInfo).CalcHitInfo(p);
                        if (ehi.HitTest == EditHitTest.Button)
                        {
                            ((ComboBoxEdit)view.ActiveEditor).ShowPopup();
                            PerformClick(edit, new ButtonPressedEventArgs(ehi.HitObject as EditorButton));
                            ((DevExpress.Utils.DXMouseEventArgs)e).Handled = true;
                        }
                    }
                }
            }
        }

        void PerformClick(ButtonEdit editor, ButtonPressedEventArgs e)
        {
            if (editor == null || e == null) return;
            MethodInfo mi = typeof(RepositoryItemButtonEdit).GetMethod("RaiseButtonClick",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            if (mi != null)
                mi.Invoke(editor.Properties, new object[] { e });
        }
    }
}