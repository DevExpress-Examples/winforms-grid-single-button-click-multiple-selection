<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128629453/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1378)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/WindowsApplication168/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication168/Form1.vb))
* [Program.cs](./CS/WindowsApplication168/Program.cs) (VB: [Program.vb](./VB/WindowsApplication168/Program.vb))
<!-- default file list end -->
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


