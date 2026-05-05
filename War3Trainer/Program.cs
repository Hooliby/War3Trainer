using System;
using System.Windows.Forms;

namespace War3Trainer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try 
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                DialogResult dr = MessageBox.Show(
                    "程序启动失败，可能是由于编译问题或环境不支持。\n\n是否复制错误日志到剪贴板？\n\n错误摘要：" + ex.Message, 
                    "启动失败", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Error);

                if (dr == DialogResult.Yes)
                {
                    Clipboard.SetText(ex.ToString());
                }
            }
        }
    }
}
