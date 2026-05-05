using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace War3Trainer
{
    public partial class MainForm : Form
    {
        private GameContext _currentGameContext;
        private GameTrainer _mainTrainer;
        private Dictionary<uint, LockedItem> _lockedItems = new Dictionary<uint, LockedItem>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lockTimer.Start();
            try
            {
                System.Diagnostics.Process.EnterDebugMode();
            }
            catch
            {
                ReportEnterDebugFailure();
                return;
            }

            FindGame();
        }

        /************************************************************************/
        /* Main functions                                                       */
        /************************************************************************/
        private void FindGame()
        {
            bool isRecognized = false;
            try
            {
                _currentGameContext = GameContext.FindGameRunning("war3", "game.dll");
                if (_currentGameContext == null)
                {
                    // netease war3 platform(dz.163.com)
                    _currentGameContext = GameContext.FindGameRunning("dzwar3", "game.dll");
                }
                if (_currentGameContext == null)
                {
                    // Warcraft III (Reforged or newer versions)
                    _currentGameContext = GameContext.FindGameRunning("Warcraft III", "game.dll");
                }
                if (_currentGameContext != null)
                {
                    // Game online
                    ReportVersionOk(_currentGameContext.ProcessId, _currentGameContext.ProcessVersion);

                    // Get a new trainer
                    GetAllObject();

                    isRecognized = true;
                }
                else
                {
                    // Game offline
                    ReportNoGameFoundFailure();
                }
            }
            catch (UnkonwnGameVersionExpection ex)
            {
                // Unknown game version
                _currentGameContext = null;
                ReportVersionFailure(ex.ProcessId, ex.GameVersion);
            }
            catch (WindowsApi.BadProcessIdException ex)
            {
                this._currentGameContext = null;
                ReportProcessIdFailure(ex.ProcessId);
            }
            catch (Exception ex)
            {
                // Why here?
                _currentGameContext = null;
                ReportUnknownFailure(ex.Message);
            }

            // Enable buttons
            if (isRecognized)
            {
                viewFunctions.Enabled = true;
                viewData.Enabled = true;
                toolStripButton2.Enabled = true;
                toolStripButton1.Enabled = true;
            }
            else
            {
                viewFunctions.Enabled = false;
                viewData.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton1.Enabled = false;
            }
        }

        // 获取所有游戏对象并更新树结构
        private void GetAllObject()
        {
            // Check paramters
            if (_currentGameContext == null)
                return;

            // 记录当前选中的节点索引，以便刷新后恢复
            string selectedNodeIndex = null;
            if (viewFunctions.SelectedNode != null)
            {
                ITrainerNode node = viewFunctions.SelectedNode.Tag as ITrainerNode;
                if (node != null) selectedNodeIndex = node.NodeIndex.ToString();
            }

            // Get a new trainer
            _mainTrainer = new GameTrainer(_currentGameContext);

            // Create function tree
            viewFunctions.Nodes.Clear();
            foreach (ITrainerNode currentFunction in _mainTrainer.GetFunctionList())
            {
                if (currentFunction.NodeType == TrainerNodeType.Introduction)
                    continue;

                TreeNode[] parentNodes = viewFunctions.Nodes.Find(currentFunction.ParentIndex.ToString(), true);
                TreeNodeCollection parentTree;
                if (parentNodes.Length < 1)
                    parentTree = viewFunctions.Nodes;
                else
                    parentTree = parentNodes[0].Nodes;

                parentTree.Add(
                    currentFunction.NodeIndex.ToString(),
                    currentFunction.NodeTypeName)
                    .Tag = currentFunction;
            }
            viewFunctions.ExpandAll();

            // 恢复之前选中的节点或切换到第一页
            TreeNode[] targetNodes = null;
            if (!string.IsNullOrEmpty(selectedNodeIndex))
            {
                targetNodes = viewFunctions.Nodes.Find(selectedNodeIndex, true);
            }

            if (targetNodes != null && targetNodes.Length > 0)
            {
                viewFunctions.SelectedNode = targetNodes[0];
                SelectFunction(targetNodes[0]);
            }
            else
            {
                TreeNode[] introductionNodes = viewFunctions.Nodes.Find("1", true);
                if (introductionNodes.Length > 0)
                {
                    viewFunctions.SelectedNode = introductionNodes[0];
                    SelectFunction(introductionNodes[0]);
                }
            }
            UpdateNodeIcons(viewFunctions.Nodes);
        }
        private void UpdateNodeIcons(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                }
                else
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                }
                if (node.Nodes.Count > 0)
                {
                    UpdateNodeIcons(node.Nodes);
                }
            }
        }
        // Re-query specific tree-node by FunctionListNode
        private void RefreshSelectedObject(ITrainerNode currentFunction)
        {
            TreeNode[] currentNodes = viewFunctions.Nodes.Find(currentFunction.NodeIndex.ToString(), true);
            TreeNode currentTree;
            if (currentNodes.Length < 1)
                return;
            else
                currentTree = currentNodes[0];

            currentTree.Text = currentFunction.NodeTypeName;
        }

        private void SelectFunction(TreeNode functionNode)
        {
            if (functionNode == null)
                return;
            ITrainerNode node = functionNode.Tag as ITrainerNode;
            if (node == null)
                return;

            FillAddressList(node.NodeIndex);
        }

        private void FillAddressList(int functionNodeId)
        {
            // To set the right window
            viewData.Items.Clear();
            foreach (IAddressNode addressLine in _mainTrainer.GetAddressList())
            {
                if (addressLine.ParentIndex != functionNodeId)
                    continue;

                string lockText = "  [  ]";
                if (_lockedItems.ContainsKey(addressLine.Address))
                {
                    lockText = " [ √ ]";
                }

                viewData.Items.Add(new ListViewItem(
                    new string[]
                    {
                        addressLine.Caption,    // Caption
                        "",                     // Original value
                        "",                     // Modified value
                        lockText                // Lock status
                    }));
                viewData.Items[viewData.Items.Count - 1].Tag = addressLine;
            }

            // To get memory content
            using (WindowsApi.ProcessMemory mem = new WindowsApi.ProcessMemory(_currentGameContext.ProcessId))
            {
                foreach (ListViewItem currentItem in viewData.Items)
                {
                    IAddressNode addressLine = currentItem.Tag as IAddressNode;
                    if (addressLine == null)
                        continue;

                    Object itemValue;
                    switch (addressLine.ValueType)
                    {
                        case AddressListValueType.Integer:
                            itemValue = mem.ReadInt32((IntPtr)addressLine.Address)
                                / addressLine.ValueScale;
                            break;
                        case AddressListValueType.Float:
                            itemValue = mem.ReadFloat((IntPtr)addressLine.Address)
                                / addressLine.ValueScale;
                            break;
                        case AddressListValueType.Char4:
                            itemValue = mem.ReadChar4((IntPtr)addressLine.Address);
                            break;
                        default:
                            itemValue = "";
                            break;
                    }
                    currentItem.SubItems[1].Text = itemValue.ToString();
                    currentItem.ImageIndex = 2;
                }
            }
        }

        // To apply the modifications
        private void ApplyModify()
        {
            if (_currentGameContext == null)
                return;

            using (WindowsApi.ProcessMemory mem = new WindowsApi.ProcessMemory(_currentGameContext.ProcessId))
            {
                foreach (ListViewItem currentItem in viewData.Items)
                {
                    string itemValueString = currentItem.SubItems[2].Text;
                    if (String.IsNullOrEmpty(itemValueString))
                    {
                        // Not modified
                        continue;
                    }

                    IAddressNode addressLine = currentItem.Tag as IAddressNode;
                    if (addressLine == null)
                        continue;

                    WriteValueToMemory(mem, addressLine.Address, addressLine.ValueType, addressLine.ValueScale, itemValueString);

                    if (_lockedItems.ContainsKey(addressLine.Address))
                    {
                        _lockedItems[addressLine.Address].ValueString = itemValueString;
                    }

                    // 写入后，更新界面的“原始值”列，并清空“修改”列
                    currentItem.SubItems[1].Text = itemValueString;
                    currentItem.SubItems[2].Text = "";
                }
            }
        }

        private void WriteValueToMemory(WindowsApi.ProcessMemory mem, uint address, AddressListValueType valueType, int valueScale, string valueString)
        {
            switch (valueType)
            {
                case AddressListValueType.Integer:
                    Int32 intValue;
                    if (!Int32.TryParse(valueString, out intValue))
                        intValue = 0;
                    intValue = unchecked(intValue * valueScale);
                    mem.WriteInt32((IntPtr)address, intValue);
                    break;
                case AddressListValueType.Float:
                    float floatValue;
                    if (!float.TryParse(valueString, out floatValue))
                        floatValue = 0;
                    floatValue = unchecked(floatValue * valueScale);
                    mem.WriteFloat((IntPtr)address, floatValue);
                    break;
                case AddressListValueType.Char4:
                    mem.WriteChar4((IntPtr)address, valueString);
                    break;
            }
        }

        /************************************************************************/
        /* Exception UI                                                         */
        /************************************************************************/
        private void ReportEnterDebugFailure()
        {
            labGameScanState.Text = "请以管理员身份运行";
        }

        private void ReportNoGameFoundFailure()
        {
            labGameScanState.Text = "游戏未运行，运行游戏后单击“查找游戏”";
        }

        private void ReportUnknownFailure(string message)
        {
            labGameScanState.Text = "发生未知错误：" + message;
        }

        private void ReportProcessIdFailure(int processId)
        {
            labGameScanState.Text = "错误的进程ID："
                + processId.ToString();
        }

        private void ReportVersionFailure(int processId, string version)
        {
            labGameScanState.Text = "游戏已运行，但版本（"
                + version
                + "）不被支持";
        }

        private void ReportVersionOk(int processId, string version)
        {
            labGameScanState.Text = "游戏已运行("
                + processId.ToString()
                + ")，版本："
                + version
                + "（支持）";
        }

        /************************************************************************/
        /* GUI                                                                  */
        /************************************************************************/
        private void MenuHelpAbout_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Warcraft III 内存修改器"
                 + Application.ProductVersion + System.Environment.NewLine
                 + System.Environment.NewLine
                 + "暴徒修改：https://github.com/Hooliby/War3Trainer" + System.Environment.NewLine
                 + "",
                 "War3Trainer",
                 MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Information);

            if (r == DialogResult.OK)
            {
                try
                {
                    Process.Start("https://github.com/Hooliby/War3Trainer");
                }
                catch { }
            }

        }

        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdScanGame_Click(object sender, EventArgs e)
        {
            FindGame();
        }

        private void viewFunctions_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            // 如果用户正在编辑输入框，先强制保存当前输入
            if (txtInput.Visible)
            {
                txtInput_Leave(txtInput, null);
                viewData.Focus();
            }

            // Check whether modification is not saved
            bool isSaved = true;
            foreach (ListViewItem currentItem in viewData.Items)
            {
                if (!String.IsNullOrEmpty(currentItem.SubItems[2].Text))
                {
                    isSaved = false;
                    break;
                }
            }

            // Save all if not saved
            if (!isSaved)
            {
                ApplyModify();
            }

            // Select another function
            try
            {
                SelectFunction(e.Node);
            }
            catch (WindowsApi.BadProcessIdException ex)
            {
                ReportProcessIdFailure(ex.ProcessId);
            }
        }

        private enum RightFunction
        {
            Empty,
            Introduction,
            EditTable,
        }

        private void lockTimer_Tick(object sender, EventArgs e)
        {
            if (_currentGameContext == null || _lockedItems.Count == 0) return;
            try
            {
                using (WindowsApi.ProcessMemory mem = new WindowsApi.ProcessMemory(_currentGameContext.ProcessId))
                {
                    foreach (var item in _lockedItems.Values)
                    {
                        WriteValueToMemory(mem, item.Address, item.ValueType, item.ValueScale, item.ValueString);
                    }
                }
            }
            catch { }
        }

        private void ToggleLock(ListViewItem item)
        {
            IAddressNode addressLine = item.Tag as IAddressNode;
            if (addressLine == null) return;

            uint addr = addressLine.Address;
            if (_lockedItems.ContainsKey(addr))
            {
                _lockedItems.Remove(addr);
                item.SubItems[3].Text = "  [  ]";
            }
            else
            {
                // 获取要锁定的数值，优先取修改框内的值，没有则取原数值
                string valToLock = item.SubItems[2].Text;
                if (string.IsNullOrEmpty(valToLock)) valToLock = item.SubItems[1].Text;

                if (!string.IsNullOrEmpty(valToLock))
                {
                    _lockedItems[addr] = new LockedItem
                    {
                        Address = addr,
                        ValueType = addressLine.ValueType,
                        ValueScale = addressLine.ValueScale,
                        ValueString = valToLock
                    };
                    item.SubItems[3].Text = " [ √ ]";
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////       
        // Make the ListView editable
        // 将输入框精确覆盖到选中项的“修改”列，以便用户输入修改数值
        private void ReplaceInputTextbox()
        {
            if (viewData.SelectedItems.Count < 1)
                return;
            ListViewItem currentItem = viewData.SelectedItems[0];

            Rectangle rect = currentItem.SubItems[2].Bounds;
            int textY = rect.Y + (rect.Height - txtInput.Height) / 2;
            txtInput.Location = new Point(rect.X + 1, textY);
            txtInput.Width = rect.Width - 2;
        }

        private void viewData_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((Keys)e.KeyChar)
            {
                case Keys.Enter:
                    viewData_MouseUp(sender, null);
                    e.Handled = true;
                    break;
            }
        }
        private void viewData_MouseUp(object sender, MouseEventArgs e)
        {
            //Get item
            if (viewData.SelectedItems.Count < 1) return;
            ListViewItem currentItem = viewData.SelectedItems[0];

            if (e != null)
            {
                ListViewHitTestInfo hitInfo = viewData.HitTest(e.X, e.Y);
                if (hitInfo.Item != null && hitInfo.SubItem != null)
                {
                    int colIndex = hitInfo.Item.SubItems.IndexOf(hitInfo.SubItem);
                    if (colIndex == 3) // 点击了锁定列
                    {
                        ToggleLock(hitInfo.Item);
                        return;
                    }
                }
            }

            //Determine the content of edit box
            ReplaceInputTextbox();
            txtInput.Tag = currentItem;

            int textToEdit = string.IsNullOrEmpty(currentItem.SubItems[2].Text) ? 1 : 2;
            string originalText = currentItem.SubItems[textToEdit].Text;
            string itemName = currentItem.SubItems[0].Text;

            txtInput.Text = CalculateInputValue(itemName, originalText);
            //txtInput.Text = currentItem.SubItems[textToEdit].Text;

            //Enable editing
            txtInput.Visible = true;
            txtInput.Focus();
            // 光标定位到末尾
            txtInput.Select(txtInput.Text.Length, 0);
        }

        private string CalculateInputValue(string itemName, string originalText)
        {
            if (itemName == "攻击① - 间隔") return "0.01";
            if (itemName.Contains("金币") || itemName.Contains("木材")) return "900000";
            if (itemName.Contains("最大人口")) return "100";

            int multiplier = xToolStripMenuItem1.Checked ? 2 :
                xToolStripMenuItem2.Checked ? 3 :
                toolStripMenuItem7.Checked ? 4 :
                xToolStripMenuItem3.Checked ? 5 : 1;

            double val;
            if (multiplier > 1 && double.TryParse(originalText, out val))
            {
                return ((int)Math.Round(val, MidpointRounding.AwayFromZero) * multiplier).ToString();
            }

            return originalText;
        }


        private void viewData_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (txtInput.Visible)
            {
                ReplaceInputTextbox();
            }
        }

        private void viewData_Scrolling(object sender, EventArgs e)
        {
            viewData.Focus();
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            if (!txtInput.Visible) return;
            
            txtInput.Visible = false;
            ListViewItem currentItem = txtInput.Tag as ListViewItem;
            if (currentItem == null)
                return;

            IAddressNode addressLine = currentItem.Tag as IAddressNode;
            if (addressLine == null) return;

            string inputText = txtInput.Text;
            bool isValid = true;

            // 校验输入格式，确保输入的格式正确，否则认为未修改
            if (!string.IsNullOrEmpty(inputText))
            {
                switch (addressLine.ValueType)
                {
                    case AddressListValueType.Integer:
                        Int32 intValue;
                        if (!Int32.TryParse(inputText, out intValue)) isValid = false;
                        break;
                    case AddressListValueType.Float:
                        float floatValue;
                        if (!float.TryParse(inputText, out floatValue)) isValid = false;
                        break;
                }
            }

            if (!isValid)
            {
                // 如果输入无效，则清空修改列，表示不修改
                currentItem.SubItems[2].Text = "";
                return;
            }

            if (currentItem.SubItems[1].Text != inputText)
                currentItem.SubItems[2].Text = inputText;
            else
                currentItem.SubItems[2].Text = "";
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CommitEditAndMoveNext(sender, 1);
                    e.Handled = true;
                    break;
                case Keys.Up:
                    CommitEditAndMoveNext(sender, -1);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    CommitEditAndMoveNext(sender, 1);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    DiscardEdit(sender);
                    e.Handled = true;
                    break;
                case Keys.F5:
                    toolStripButton2_Click(sender, null);
                    e.Handled = true;
                    break;
                case Keys.F6:
                    toolStripButton1_Click(sender, null);
                    e.Handled = true;
                    break;
            }
        }

        private void DiscardEdit(object editBox)
        {
            // Roll back content of the edit box
            viewData_MouseUp(editBox, null);

            // Hide edit box
            txtInput_Leave(editBox, null);

            // Restore focus
            viewData.Focus();
        }

        private void CommitEditAndMoveNext(object editBox, int delta)
        {
            // Commit
            txtInput_Leave(editBox, null);

            // Move to another line
            viewData.Focus();
            if (viewData.SelectedItems.Count > 0)
            {
                int nextIndex = viewData.SelectedItems[0].Index + delta;
                if (nextIndex < viewData.Items.Count &&
                    nextIndex >= 0)
                {
                    viewData.Items[nextIndex].Selected = true;
                    viewData.Items[nextIndex].Focused = true;
                    viewData.Items[nextIndex].EnsureVisible();
                }
                viewData_MouseUp(editBox, null);
            }
        }

        /************************************************************************/
        /* Debug                                                                */
        /************************************************************************/
        private void menuDebug1_Click(object sender, EventArgs e)
        {
            string strIndex = Microsoft.VisualBasic.Interaction.InputBox(
                "nIndex = 0x?",
                "War3Common.ReadFromGameMemory(nIndex)",
                "0", -1, -1);
            if (String.IsNullOrEmpty(strIndex))
                return;

            Int32 nIndex;
            if (!Int32.TryParse(
                strIndex,
                System.Globalization.NumberStyles.HexNumber,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out nIndex))
            {
                nIndex = 0;
            }

            try
            {
                UInt32 result = 0;
                using (WindowsApi.ProcessMemory mem = new WindowsApi.ProcessMemory(_currentGameContext.ProcessId))
                {
                    NewChildrenEventArgs args = new NewChildrenEventArgs();
                    War3Common.GetGameMemory(
                        _currentGameContext, ref args);
                    result = War3Common.ReadFromGameMemory(
                        mem, _currentGameContext, args,
                        nIndex);
                }
                MessageBox.Show(
                    "0x" + result.ToString("X"),
                    "War3Common.ReadFromGameMemory(0x" + strIndex + ")");
            }
            catch (WindowsApi.BadProcessIdException ex)
            {
                ReportProcessIdFailure(ex.ProcessId);
            }
        }

        private void 启用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = 启用ToolStripMenuItem.Checked;
        }

        private void 解除修改限制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (解除修改限制ToolStripMenuItem == null || txtInput == null)
                return;
            txtInput.MaxLength = 解除修改限制ToolStripMenuItem.Checked ? 35 : 7;
        }

        private void viewFunctions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                toolStripButton2_Click(sender, null);
                e.Handled = true;
            }

        }

        private void viewData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F6)
            {
                toolStripButton1_Click(sender, null);
                e.Handled = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // 确保焦点从输入框移开，以触发 txtInput_Leave 将数据写入 ListViewItem
            if (txtInput.Visible)
            {
                txtInput_Leave(txtInput, null);
                viewData.Focus();
            }

            try
            {
                ApplyModify();

                // 去除这里冗余的刷新左侧和刷新右侧逻辑
                // 因为 ApplyModify() 现在会自动更新 ListViewItem 的显示
                // 如果调用 RefreshSelectedObject 和 SelectFunction 会导致整个列表重建，体验不佳（例如选中状态丢失）
            }
            catch (WindowsApi.BadProcessIdException ex)
            {
                ReportProcessIdFailure(ex.ProcessId);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                GetAllObject();
            }
            catch (WindowsApi.BadProcessIdException ex)
            {
                ReportProcessIdFailure(ex.ProcessId);
            }
        }

        private void GroupedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem currentItem = sender as ToolStripMenuItem;
            if (currentItem == null) return;

            ToolStripMenuItem[] allItems = new ToolStripMenuItem[]
            {
                xToolStripMenuItem,
                xToolStripMenuItem1,
                xToolStripMenuItem2,
                toolStripMenuItem7,
                xToolStripMenuItem3
            };

            foreach (var item in allItems)
            {
                item.Checked = (item == currentItem);
            }
        }
    }
}
