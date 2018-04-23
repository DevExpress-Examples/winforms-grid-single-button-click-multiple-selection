# How to implement a single button click in multiple selection mode


<p>This sample illustrates how to force the ButtonClick event for an in-place editor when a user performs a single click. The grid works in CellSelect multiple selection mode and it's necessary to keep selection when a user clicks a button. <br />
To accomplish this task handle the GridView.MouseDown event. Based on the hit information it's possible to define a cell under the mouse cursor, activate it and detect if the editor's button should be clicked. Finally, the default event handler is locked using the following code:</p>

```cs
<newline/>
(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;<newline/>

```



```vb
<newline/>
TryCast(e, DevExpress.Utils.DXMouseEventArgs).Handled = True<newline/>

```

<br />
<br />


<br/>


