namespace Contacts_WinForm
{
    partial class ContactsFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactsFrm));
            this.gvContacts = new System.Windows.Forms.DataGridView();
            this.cmsContactsGvRecord = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvContacts)).BeginInit();
            this.cmsContactsGvRecord.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvContacts
            // 
            this.gvContacts.AllowUserToOrderColumns = true;
            this.gvContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvContacts.Location = new System.Drawing.Point(16, 61);
            this.gvContacts.Name = "gvContacts";
            this.gvContacts.RowHeadersWidth = 51;
            this.gvContacts.RowTemplate.ContextMenuStrip = this.cmsContactsGvRecord;
            this.gvContacts.RowTemplate.Height = 26;
            this.gvContacts.Size = new System.Drawing.Size(1118, 481);
            this.gvContacts.TabIndex = 0;
            this.gvContacts.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvContacts_CellMouseDown);
            // 
            // cmsContactsGvRecord
            // 
            this.cmsContactsGvRecord.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsContactsGvRecord.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUpdate,
            this.tsmiDelete});
            this.cmsContactsGvRecord.Name = "cmsContactsGvRecord";
            this.cmsContactsGvRecord.ShowImageMargin = false;
            this.cmsContactsGvRecord.Size = new System.Drawing.Size(103, 52);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(102, 24);
            this.tsmiUpdate.Text = "Update";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(102, 24);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // btnAddContact
            // 
            this.btnAddContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAddContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddContact.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddContact.Location = new System.Drawing.Point(1048, 12);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(87, 36);
            this.btnAddContact.TabIndex = 1;
            this.btnAddContact.Text = "Add";
            this.btnAddContact.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Contacts Table";
            // 
            // ContactsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 554);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.gvContacts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContactsFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contacts";
            this.Load += new System.EventHandler(this.ContactsFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvContacts)).EndInit();
            this.cmsContactsGvRecord.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvContacts;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsContactsGvRecord;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
    }
}

