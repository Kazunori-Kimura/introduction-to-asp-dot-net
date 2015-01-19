using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// OKボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            // 名前を取得
            string name = TextBox1.Text;

            string message = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                // 名前が未入力
                message = "お名前を入力してください！";
            }
            else
            {
                message = string.Format("こんにちは、{0} さん！", name);
            }

            // ラベルにメッセージをセット
            Label2.Text = message;
        }
    }
}