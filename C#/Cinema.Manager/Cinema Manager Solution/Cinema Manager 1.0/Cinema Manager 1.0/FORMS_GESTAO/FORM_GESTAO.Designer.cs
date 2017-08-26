namespace Cinema_Manager_1._0.FORMS_GESTAO
{
    partial class FORM_GESTAO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_GESTAO));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SPLITCONTAINER_GESTAO = new System.Windows.Forms.SplitContainer();
            this.PICTUREBOX_GESTAO_SESSOES = new System.Windows.Forms.PictureBox();
            this.PICTUREBOX_GESTAO_PESSOAS = new System.Windows.Forms.PictureBox();
            this.PICTUREBOX_GESTAO_FILMES = new System.Windows.Forms.PictureBox();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SPLITCONTAINER_GESTAO)).BeginInit();
            this.SPLITCONTAINER_GESTAO.Panel1.SuspendLayout();
            this.SPLITCONTAINER_GESTAO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICTUREBOX_GESTAO_SESSOES)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTUREBOX_GESTAO_PESSOAS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTUREBOX_GESTAO_FILMES)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fecharToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1089, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SPLITCONTAINER_GESTAO
            // 
            this.SPLITCONTAINER_GESTAO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SPLITCONTAINER_GESTAO.IsSplitterFixed = true;
            this.SPLITCONTAINER_GESTAO.Location = new System.Drawing.Point(0, 24);
            this.SPLITCONTAINER_GESTAO.Name = "SPLITCONTAINER_GESTAO";
            // 
            // SPLITCONTAINER_GESTAO.Panel1
            // 
            this.SPLITCONTAINER_GESTAO.Panel1.Controls.Add(this.PICTUREBOX_GESTAO_SESSOES);
            this.SPLITCONTAINER_GESTAO.Panel1.Controls.Add(this.PICTUREBOX_GESTAO_PESSOAS);
            this.SPLITCONTAINER_GESTAO.Panel1.Controls.Add(this.PICTUREBOX_GESTAO_FILMES);
            this.SPLITCONTAINER_GESTAO.Size = new System.Drawing.Size(1089, 600);
            this.SPLITCONTAINER_GESTAO.SplitterDistance = 201;
            this.SPLITCONTAINER_GESTAO.TabIndex = 1;
            // 
            // PICTUREBOX_GESTAO_SESSOES
            // 
            this.PICTUREBOX_GESTAO_SESSOES.Image = global::Cinema_Manager_1._0.Properties.Resources.Sessoes_Small;
            this.PICTUREBOX_GESTAO_SESSOES.Location = new System.Drawing.Point(6, 14);
            this.PICTUREBOX_GESTAO_SESSOES.Name = "PICTUREBOX_GESTAO_SESSOES";
            this.PICTUREBOX_GESTAO_SESSOES.Size = new System.Drawing.Size(188, 41);
            this.PICTUREBOX_GESTAO_SESSOES.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICTUREBOX_GESTAO_SESSOES.TabIndex = 14;
            this.PICTUREBOX_GESTAO_SESSOES.TabStop = false;
            this.PICTUREBOX_GESTAO_SESSOES.Click += new System.EventHandler(this.PICTUREBOX_GESTAO_SESSOES_Click);
            // 
            // PICTUREBOX_GESTAO_PESSOAS
            // 
            this.PICTUREBOX_GESTAO_PESSOAS.Image = global::Cinema_Manager_1._0.Properties.Resources.Users3;
            this.PICTUREBOX_GESTAO_PESSOAS.Location = new System.Drawing.Point(6, 155);
            this.PICTUREBOX_GESTAO_PESSOAS.Name = "PICTUREBOX_GESTAO_PESSOAS";
            this.PICTUREBOX_GESTAO_PESSOAS.Size = new System.Drawing.Size(188, 41);
            this.PICTUREBOX_GESTAO_PESSOAS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICTUREBOX_GESTAO_PESSOAS.TabIndex = 13;
            this.PICTUREBOX_GESTAO_PESSOAS.TabStop = false;
            this.PICTUREBOX_GESTAO_PESSOAS.Click += new System.EventHandler(this.PICTUREBOX_GESTAO_PESSOAS_Click);
            // 
            // PICTUREBOX_GESTAO_FILMES
            // 
            this.PICTUREBOX_GESTAO_FILMES.Image = ((System.Drawing.Image)(resources.GetObject("PICTUREBOX_GESTAO_FILMES.Image")));
            this.PICTUREBOX_GESTAO_FILMES.Location = new System.Drawing.Point(6, 84);
            this.PICTUREBOX_GESTAO_FILMES.Name = "PICTUREBOX_GESTAO_FILMES";
            this.PICTUREBOX_GESTAO_FILMES.Size = new System.Drawing.Size(188, 41);
            this.PICTUREBOX_GESTAO_FILMES.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICTUREBOX_GESTAO_FILMES.TabIndex = 12;
            this.PICTUREBOX_GESTAO_FILMES.TabStop = false;
            this.PICTUREBOX_GESTAO_FILMES.Click += new System.EventHandler(this.PICTUREBOX_GESTAO_FILMES_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Image = global::Cinema_Manager_1._0.Properties.Resources.Fechar_2;
            this.fecharToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.fecharToolStripMenuItem.Text = "Fechar";
            // 
            // FORM_GESTAO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 624);
            this.Controls.Add(this.SPLITCONTAINER_GESTAO);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FORM_GESTAO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cinema Manager: Gestão";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.SPLITCONTAINER_GESTAO.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SPLITCONTAINER_GESTAO)).EndInit();
            this.SPLITCONTAINER_GESTAO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PICTUREBOX_GESTAO_SESSOES)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTUREBOX_GESTAO_PESSOAS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTUREBOX_GESTAO_FILMES)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        public System.Windows.Forms.SplitContainer SPLITCONTAINER_GESTAO;
        private System.Windows.Forms.PictureBox PICTUREBOX_GESTAO_PESSOAS;
        private System.Windows.Forms.PictureBox PICTUREBOX_GESTAO_FILMES;
        private System.Windows.Forms.PictureBox PICTUREBOX_GESTAO_SESSOES;
    }
}