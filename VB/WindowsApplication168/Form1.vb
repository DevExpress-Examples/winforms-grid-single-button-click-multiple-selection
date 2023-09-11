Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository

Namespace SingleButtonClick

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            For i As Integer = 0 To 3 - 1
                dataTable1.Rows.Add(New Object() {i, "Name" & i})
            Next
        End Sub

        Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            If(ModifierKeys And Keys.Control) <> Keys.Control Then
                Dim view As GridView = TryCast(sender, GridView)
                Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
                If hi.InRowCell Then
                    If hi.Column.RealColumnEdit.GetType() Is GetType(RepositoryItemComboBox) Then
                        view.FocusedRowHandle = hi.RowHandle
                        view.FocusedColumn = hi.Column
                        view.ShowEditor()
                        'force button click 
                        Dim edit As ButtonEdit = TryCast(view.ActiveEditor, ComboBoxEdit)
                        Dim p As Point = view.GridControl.PointToScreen(e.Location)
                        p = edit.PointToClient(p)
                        Dim ehi As EditHitInfo = TryCast(edit.GetViewInfo(), ButtonEditViewInfo).CalcHitInfo(p)
                        If ehi.HitTest = EditHitTest.Button Then
                            CType(view.ActiveEditor, ComboBoxEdit).ShowPopup()
                            PerformClick(edit, New ButtonPressedEventArgs(TryCast(ehi.HitObject, EditorButton)))
                            CType(e, DevExpress.Utils.DXMouseEventArgs).Handled = True
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub PerformClick(ByVal editor As ButtonEdit, ByVal e As ButtonPressedEventArgs)
            If editor Is Nothing OrElse e Is Nothing Then Return
            Dim mi As MethodInfo = GetType(RepositoryItemButtonEdit).GetMethod("RaiseButtonClick", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly)
            If mi IsNot Nothing Then mi.Invoke(editor.Properties, New Object() {e})
        End Sub
    End Class
End Namespace
