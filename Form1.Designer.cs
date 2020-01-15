namespace UIAutomationFormTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.StartFindElement = new System.Windows.Forms.Button();
            this.ShowElementText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // StartFindElement
            // 
            this.StartFindElement.Location = new System.Drawing.Point(12, 12);
            this.StartFindElement.Name = "StartFindElement";
            this.StartFindElement.Size = new System.Drawing.Size(75, 23);
            this.StartFindElement.TabIndex = 0;
            this.StartFindElement.Text = "查看属性";
            this.StartFindElement.UseVisualStyleBackColor = true;
            this.StartFindElement.Click += new System.EventHandler(this.StartFindElement_Click);
            // 
            // ShowElementText
            // 
            this.ShowElementText.Location = new System.Drawing.Point(12, 41);
            this.ShowElementText.Name = "ShowElementText";
            this.ShowElementText.Size = new System.Drawing.Size(394, 404);
            this.ShowElementText.TabIndex = 1;
            this.ShowElementText.Text = "";
            this.ShowElementText.TextChanged += new System.EventHandler(this.ShowElementText_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 457);
            this.Controls.Add(this.ShowElementText);
            this.Controls.Add(this.StartFindElement);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartFindElement;
        private System.Windows.Forms.RichTextBox ShowElementText;
        private System.Windows.Forms.DataGridViewRow Row1;
        private System.Windows.Forms.DataGridViewRow Row2;
    }
}

