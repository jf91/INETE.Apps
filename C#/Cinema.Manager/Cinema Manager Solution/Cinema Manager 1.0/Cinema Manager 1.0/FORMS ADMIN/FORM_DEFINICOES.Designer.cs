namespace Cinema_Manager_1._0.FORMS_ADMIN
{
    partial class FORM_DEFINICOES
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_DEFINICOES));
            this.MENUSTRIP_DEFINICOES = new System.Windows.Forms.MenuStrip();
            this.MENUSTRIP_DEFINICOES_BUTTON_FECHAR = new System.Windows.Forms.ToolStripMenuItem();
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO = new System.Windows.Forms.ToolStripMenuItem();
            this.MENUSTRIP_DEFINICOES.SuspendLayout();
            this.SuspendLayout();
            // 
            // MENUSTRIP_DEFINICOES
            // 
            this.MENUSTRIP_DEFINICOES.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MENUSTRIP_DEFINICOES_BUTTON_FECHAR,
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO});
            this.MENUSTRIP_DEFINICOES.Location = new System.Drawing.Point(0, 0);
            this.MENUSTRIP_DEFINICOES.Name = "MENUSTRIP_DEFINICOES";
            this.MENUSTRIP_DEFINICOES.Size = new System.Drawing.Size(846, 24);
            this.MENUSTRIP_DEFINICOES.TabIndex = 0;
            this.MENUSTRIP_DEFINICOES.Text = "menuStrip1";
            // 
            // MENUSTRIP_DEFINICOES_BUTTON_FECHAR
            // 
            this.MENUSTRIP_DEFINICOES_BUTTON_FECHAR.Image = global::Cinema_Manager_1._0.Properties.Resources.Fechar_2;
            this.MENUSTRIP_DEFINICOES_BUTTON_FECHAR.Name = "MENUSTRIP_DEFINICOES_BUTTON_FECHAR";
            this.MENUSTRIP_DEFINICOES_BUTTON_FECHAR.Size = new System.Drawing.Size(70, 20);
            this.MENUSTRIP_DEFINICOES_BUTTON_FECHAR.Text = "Fechar";
            // 
            // MENUSTRIP_DEFINICOES_BUTTON_LIGACAO
            // 
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO.Name = "MENUSTRIP_DEFINICOES_BUTTON_LIGACAO";
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO.Size = new System.Drawing.Size(60, 20);
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO.Text = "Ligação";
            this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO.Click += new System.EventHandler(this.MENUSTRIP_DEFINICOES_BUTTON_LIGACAO_Click);
            // 
            // FORM_DEFINICOES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 461);
            this.Controls.Add(this.MENUSTRIP_DEFINICOES);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MENUSTRIP_DEFINICOES;
            this.Name = "FORM_DEFINICOES";
            this.Text = "Cinema Manager: Definições";
            this.MENUSTRIP_DEFINICOES.ResumeLayout(false);
            this.MENUSTRIP_DEFINICOES.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MENUSTRIP_DEFINICOES;
        private System.Windows.Forms.ToolStripMenuItem MENUSTRIP_DEFINICOES_BUTTON_FECHAR;
        private System.Windows.Forms.ToolStripMenuItem MENUSTRIP_DEFINICOES_BUTTON_LIGACAO;
    }
}