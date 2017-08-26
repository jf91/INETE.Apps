namespace Cinema_Manager_1._0
{
    partial class FORM_INICIO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_INICIO));
            this.MENUSTRIP_FORM_INICIO = new System.Windows.Forms.MenuStrip();
            this.MENUSTRIP_BUTTON_ADMIN = new System.Windows.Forms.ToolStripMenuItem();
            this.MENUSTRIP_BUTTON_GESTAO = new System.Windows.Forms.ToolStripMenuItem();
            this.bilheteiraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR = new System.Windows.Forms.ToolStripMenuItem();
            this.MENUSTRIP_FORM_INICIO.SuspendLayout();
            this.SuspendLayout();
            // 
            // MENUSTRIP_FORM_INICIO
            // 
            this.MENUSTRIP_FORM_INICIO.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR,
            this.MENUSTRIP_BUTTON_ADMIN,
            this.MENUSTRIP_BUTTON_GESTAO,
            this.bilheteiraToolStripMenuItem});
            this.MENUSTRIP_FORM_INICIO.Location = new System.Drawing.Point(0, 0);
            this.MENUSTRIP_FORM_INICIO.Name = "MENUSTRIP_FORM_INICIO";
            this.MENUSTRIP_FORM_INICIO.Size = new System.Drawing.Size(1344, 24);
            this.MENUSTRIP_FORM_INICIO.TabIndex = 0;
            this.MENUSTRIP_FORM_INICIO.Text = "menuStrip1";
            // 
            // MENUSTRIP_BUTTON_ADMIN
            // 
            this.MENUSTRIP_BUTTON_ADMIN.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MENUSTRIP_BUTTON_ADMIN.Name = "MENUSTRIP_BUTTON_ADMIN";
            this.MENUSTRIP_BUTTON_ADMIN.Size = new System.Drawing.Size(96, 20);
            this.MENUSTRIP_BUTTON_ADMIN.Text = "Administração";
            this.MENUSTRIP_BUTTON_ADMIN.Click += new System.EventHandler(this.MENUSTRIP_BUTTON_ADMIN_Click);
            // 
            // MENUSTRIP_BUTTON_GESTAO
            // 
            this.MENUSTRIP_BUTTON_GESTAO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MENUSTRIP_BUTTON_GESTAO.Name = "MENUSTRIP_BUTTON_GESTAO";
            this.MENUSTRIP_BUTTON_GESTAO.Size = new System.Drawing.Size(55, 20);
            this.MENUSTRIP_BUTTON_GESTAO.Text = "Gestão";
            this.MENUSTRIP_BUTTON_GESTAO.Click += new System.EventHandler(this.MENUSTRIP_BUTTON_GESTAO_Click);
            // 
            // bilheteiraToolStripMenuItem
            // 
            this.bilheteiraToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bilheteiraToolStripMenuItem.Name = "bilheteiraToolStripMenuItem";
            this.bilheteiraToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.bilheteiraToolStripMenuItem.Text = "Bilheteira";
            // 
            // MENUSTRIP_FORM_INICIO_BUTTON_FECHAR
            // 
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR.Image = global::Cinema_Manager_1._0.Properties.Resources.Fechar_2;
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR.Name = "MENUSTRIP_FORM_INICIO_BUTTON_FECHAR";
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR.Size = new System.Drawing.Size(125, 20);
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR.Text = "Fechar Aplicação";
            this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR.Click += new System.EventHandler(this.MENUSTRIP_FORM_INICIO_BUTTON_FECHAR_Click);
            // 
            // FORM_INICIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1344, 722);
            this.Controls.Add(this.MENUSTRIP_FORM_INICIO);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MENUSTRIP_FORM_INICIO;
            this.Name = "FORM_INICIO";
            this.Text = "Gestor do Cinema";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FORM_INICIO_FormClosed);
            this.MENUSTRIP_FORM_INICIO.ResumeLayout(false);
            this.MENUSTRIP_FORM_INICIO.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MENUSTRIP_FORM_INICIO;
        private System.Windows.Forms.ToolStripMenuItem MENUSTRIP_FORM_INICIO_BUTTON_FECHAR;
        private System.Windows.Forms.ToolStripMenuItem MENUSTRIP_BUTTON_ADMIN;
        private System.Windows.Forms.ToolStripMenuItem MENUSTRIP_BUTTON_GESTAO;
        private System.Windows.Forms.ToolStripMenuItem bilheteiraToolStripMenuItem;
    }
}

