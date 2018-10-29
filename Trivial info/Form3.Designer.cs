namespace Trivial_info
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.txt_regles = new System.Windows.Forms.TextBox();
            this.btn_fermer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_quitter = new System.Windows.Forms.Button();
            this.btn_gauche = new System.Windows.Forms.Button();
            this.btn_droite = new System.Windows.Forms.Button();
            this.lbl_numPage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_regles
            // 
            this.txt_regles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txt_regles.Font = new System.Drawing.Font("AR BLANCA", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_regles.ForeColor = System.Drawing.Color.SaddleBrown;
            this.txt_regles.Location = new System.Drawing.Point(9, 62);
            this.txt_regles.Margin = new System.Windows.Forms.Padding(11);
            this.txt_regles.Multiline = true;
            this.txt_regles.Name = "txt_regles";
            this.txt_regles.ReadOnly = true;
            this.txt_regles.Size = new System.Drawing.Size(507, 409);
            this.txt_regles.TabIndex = 0;
            this.txt_regles.TabStop = false;
            // 
            // btn_fermer
            // 
            this.btn_fermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_fermer.ForeColor = System.Drawing.Color.Chocolate;
            this.btn_fermer.Location = new System.Drawing.Point(755, 1737);
            this.btn_fermer.Margin = new System.Windows.Forms.Padding(11);
            this.btn_fermer.Name = "btn_fermer";
            this.btn_fermer.Size = new System.Drawing.Size(510, 142);
            this.btn_fermer.TabIndex = 1;
            this.btn_fermer.Text = "Fermer";
            this.btn_fermer.UseVisualStyleBackColor = true;
            this.btn_fermer.Click += new System.EventHandler(this.btn_fermer_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("AR BLANCA", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Chocolate;
            this.label1.Location = new System.Drawing.Point(102, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 50);
            this.label1.TabIndex = 2;
            this.label1.Text = "Règles du jeu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_quitter
            // 
            this.btn_quitter.Font = new System.Drawing.Font("AR BLANCA", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_quitter.ForeColor = System.Drawing.Color.Chocolate;
            this.btn_quitter.Location = new System.Drawing.Point(12, 485);
            this.btn_quitter.Name = "btn_quitter";
            this.btn_quitter.Size = new System.Drawing.Size(140, 37);
            this.btn_quitter.TabIndex = 3;
            this.btn_quitter.Text = "Fermer";
            this.btn_quitter.UseVisualStyleBackColor = true;
            this.btn_quitter.Click += new System.EventHandler(this.btn_quitter_Click);
            // 
            // btn_gauche
            // 
            this.btn_gauche.Font = new System.Drawing.Font("AR BLANCA", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_gauche.ForeColor = System.Drawing.Color.Chocolate;
            this.btn_gauche.Location = new System.Drawing.Point(334, 485);
            this.btn_gauche.Name = "btn_gauche";
            this.btn_gauche.Size = new System.Drawing.Size(87, 37);
            this.btn_gauche.TabIndex = 4;
            this.btn_gauche.Text = "<";
            this.btn_gauche.UseVisualStyleBackColor = true;
            this.btn_gauche.Click += new System.EventHandler(this.btn_gauche_Click);
            // 
            // btn_droite
            // 
            this.btn_droite.Font = new System.Drawing.Font("AR BLANCA", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_droite.ForeColor = System.Drawing.Color.Chocolate;
            this.btn_droite.Location = new System.Drawing.Point(427, 485);
            this.btn_droite.Name = "btn_droite";
            this.btn_droite.Size = new System.Drawing.Size(87, 37);
            this.btn_droite.TabIndex = 5;
            this.btn_droite.Text = ">";
            this.btn_droite.UseVisualStyleBackColor = true;
            this.btn_droite.Click += new System.EventHandler(this.btn_droite_Click);
            // 
            // lbl_numPage
            // 
            this.lbl_numPage.Font = new System.Drawing.Font("AR BLANCA", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_numPage.ForeColor = System.Drawing.Color.Chocolate;
            this.lbl_numPage.Location = new System.Drawing.Point(158, 485);
            this.lbl_numPage.Name = "lbl_numPage";
            this.lbl_numPage.Size = new System.Drawing.Size(170, 37);
            this.lbl_numPage.TabIndex = 6;
            this.lbl_numPage.Text = "Page: ";
            this.lbl_numPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(23F, 46F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(526, 534);
            this.Controls.Add(this.lbl_numPage);
            this.Controls.Add(this.btn_droite);
            this.Controls.Add(this.btn_gauche);
            this.Controls.Add(this.btn_quitter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_fermer);
            this.Controls.Add(this.txt_regles);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(11);
            this.Name = "Form3";
            this.Text = "Trivial Informatic";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_regles;
        private System.Windows.Forms.Button btn_fermer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_quitter;
        private System.Windows.Forms.Button btn_gauche;
        private System.Windows.Forms.Button btn_droite;
        private System.Windows.Forms.Label lbl_numPage;
    }
}