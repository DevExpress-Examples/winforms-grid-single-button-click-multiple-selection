<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128629453/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1378)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WinForms Data Grid - Implement a single button click in multiple selection mode

This sample shows how to force the `ButtonClick` event for a cell editor when the user clicks the editor button (in this example, the dropdown button). The example handles the `GridView.MouseDown` event to obtain hit information under the mouse cursor, activate the cell editor, and determine whether the editor button is pressed:

```csharp
private void gridView1_MouseDown(object sender, MouseEventArgs e) {
    if ((Control.ModifierKeys & Keys.Control) != Keys.Control) {
        GridView view = sender as GridView;
        GridHitInfo hi = view.CalcHitInfo(e.Location);
        if (hi.InRowCell) {
            if (hi.Column.RealColumnEdit.GetType() == typeof(RepositoryItemComboBox)) {
                view.FocusedRowHandle = hi.RowHandle;
                view.FocusedColumn = hi.Column;
                view.ShowEditor();
                // Forces button click. 
                ButtonEdit edit = (view.ActiveEditor as ComboBoxEdit);
                Point p = view.GridControl.PointToScreen(e.Location);
                p = edit.PointToClient(p);
                EditHitInfo ehi = (edit.GetViewInfo() as ButtonEditViewInfo).CalcHitInfo(p);
                if (ehi.HitTest == EditHitTest.Button) {
                    ((ComboBoxEdit)view.ActiveEditor).ShowPopup();
                    PerformClick(edit, new ButtonPressedEventArgs(ehi.HitObject as EditorButton));
                    ((DevExpress.Utils.DXMouseEventArgs)e).Handled = true;
                }
            }
        }
    }
}
void PerformClick(ButtonEdit editor, ButtonPressedEventArgs e) {
    if (editor == null || e == null) return;
    MethodInfo mi = typeof(RepositoryItemButtonEdit).GetMethod("RaiseButtonClick",
        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
    if (mi != null)
        mi.Invoke(editor.Properties, new object[] { e });
}
```


## Files to Review

* [Form1.cs](./CS/WindowsApplication168/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication168/Form1.vb))
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-single-button-click-multiple-selection&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-single-button-click-multiple-selection&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
