namespace Fiorentino_FatturaElettronica
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbHeader = new System.Windows.Forms.TabPage();
            this.tbControlHeader = new System.Windows.Forms.TabControl();
            this.tbDatiTrasm = new System.Windows.Forms.TabPage();
            this.tbCedentePrest = new System.Windows.Forms.TabPage();
            this.tbControlMain = new System.Windows.Forms.TabControl();
            this.tbBody = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdTrasmittente = new System.Windows.Forms.TextBox();
            this.gb1_1_1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.gb1_1_2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProgressivoInvio = new System.Windows.Forms.TextBox();
            this.gb1_1_3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFormatoTrasmissione = new System.Windows.Forms.TextBox();
            this.gb1_1_4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodiceDestinatario = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtContattiTrasmittente = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPECDestinatario = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnProsegui = new System.Windows.Forms.Button();
            this.tbHeader.SuspendLayout();
            this.tbControlHeader.SuspendLayout();
            this.tbDatiTrasm.SuspendLayout();
            this.tbControlMain.SuspendLayout();
            this.gb1_1_1.SuspendLayout();
            this.gb1_1_2.SuspendLayout();
            this.gb1_1_3.SuspendLayout();
            this.gb1_1_4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbHeader
            // 
            this.tbHeader.Controls.Add(this.tbControlHeader);
            this.tbHeader.Location = new System.Drawing.Point(4, 25);
            this.tbHeader.Margin = new System.Windows.Forms.Padding(4);
            this.tbHeader.Name = "tbHeader";
            this.tbHeader.Padding = new System.Windows.Forms.Padding(4);
            this.tbHeader.Size = new System.Drawing.Size(1304, 658);
            this.tbHeader.TabIndex = 0;
            this.tbHeader.Text = "HEADER ";
            this.tbHeader.UseVisualStyleBackColor = true;
            // 
            // tbControlHeader
            // 
            this.tbControlHeader.Controls.Add(this.tbDatiTrasm);
            this.tbControlHeader.Controls.Add(this.tbCedentePrest);
            this.tbControlHeader.Location = new System.Drawing.Point(0, 3);
            this.tbControlHeader.Name = "tbControlHeader";
            this.tbControlHeader.SelectedIndex = 0;
            this.tbControlHeader.Size = new System.Drawing.Size(1301, 653);
            this.tbControlHeader.TabIndex = 1;
            // 
            // tbDatiTrasm
            // 
            this.tbDatiTrasm.Controls.Add(this.btnProsegui);
            this.tbDatiTrasm.Controls.Add(this.label12);
            this.tbDatiTrasm.Controls.Add(this.groupBox2);
            this.tbDatiTrasm.Controls.Add(this.groupBox1);
            this.tbDatiTrasm.Controls.Add(this.gb1_1_4);
            this.tbDatiTrasm.Controls.Add(this.gb1_1_3);
            this.tbDatiTrasm.Controls.Add(this.gb1_1_2);
            this.tbDatiTrasm.Controls.Add(this.gb1_1_1);
            this.tbDatiTrasm.Controls.Add(this.label1);
            this.tbDatiTrasm.Location = new System.Drawing.Point(4, 25);
            this.tbDatiTrasm.Name = "tbDatiTrasm";
            this.tbDatiTrasm.Padding = new System.Windows.Forms.Padding(3);
            this.tbDatiTrasm.Size = new System.Drawing.Size(1293, 624);
            this.tbDatiTrasm.TabIndex = 0;
            this.tbDatiTrasm.Text = "DATI TRASMISSIONE";
            this.tbDatiTrasm.UseVisualStyleBackColor = true;
            // 
            // tbCedentePrest
            // 
            this.tbCedentePrest.Location = new System.Drawing.Point(4, 25);
            this.tbCedentePrest.Name = "tbCedentePrest";
            this.tbCedentePrest.Padding = new System.Windows.Forms.Padding(3);
            this.tbCedentePrest.Size = new System.Drawing.Size(1224, 615);
            this.tbCedentePrest.TabIndex = 1;
            this.tbCedentePrest.Text = "CEDENTE PRESTATORE";
            this.tbCedentePrest.UseVisualStyleBackColor = true;
            // 
            // tbControlMain
            // 
            this.tbControlMain.Controls.Add(this.tbHeader);
            this.tbControlMain.Controls.Add(this.tbBody);
            this.tbControlMain.Location = new System.Drawing.Point(4, 4);
            this.tbControlMain.Margin = new System.Windows.Forms.Padding(4);
            this.tbControlMain.Name = "tbControlMain";
            this.tbControlMain.SelectedIndex = 0;
            this.tbControlMain.Size = new System.Drawing.Size(1312, 687);
            this.tbControlMain.TabIndex = 1;
            // 
            // tbBody
            // 
            this.tbBody.Location = new System.Drawing.Point(4, 25);
            this.tbBody.Name = "tbBody";
            this.tbBody.Size = new System.Drawing.Size(1228, 647);
            this.tbBody.TabIndex = 1;
            this.tbBody.Text = "BODY";
            this.tbBody.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(376, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(621, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "SCHEDA PER INSERIRE I DATI TRASMISSIONE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID TRASMITTENTE:";
            // 
            // txtIdTrasmittente
            // 
            this.txtIdTrasmittente.Location = new System.Drawing.Point(147, 27);
            this.txtIdTrasmittente.Name = "txtIdTrasmittente";
            this.txtIdTrasmittente.Size = new System.Drawing.Size(214, 22);
            this.txtIdTrasmittente.TabIndex = 4;
            // 
            // gb1_1_1
            // 
            this.gb1_1_1.Controls.Add(this.label4);
            this.gb1_1_1.Controls.Add(this.textBox2);
            this.gb1_1_1.Controls.Add(this.label3);
            this.gb1_1_1.Controls.Add(this.textBox1);
            this.gb1_1_1.Controls.Add(this.label2);
            this.gb1_1_1.Controls.Add(this.txtIdTrasmittente);
            this.gb1_1_1.Location = new System.Drawing.Point(28, 67);
            this.gb1_1_1.Name = "gb1_1_1";
            this.gb1_1_1.Size = new System.Drawing.Size(383, 120);
            this.gb1_1_1.TabIndex = 5;
            this.gb1_1_1.TabStop = false;
            this.gb1_1_1.Text = "SCHEDA 1.1.1: IDENTIFICAZIONE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "ID PAESE:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(214, 22);
            this.textBox1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "ID CODICE:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(147, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(214, 22);
            this.textBox2.TabIndex = 8;
            // 
            // gb1_1_2
            // 
            this.gb1_1_2.Controls.Add(this.label5);
            this.gb1_1_2.Controls.Add(this.txtProgressivoInvio);
            this.gb1_1_2.Location = new System.Drawing.Point(443, 67);
            this.gb1_1_2.Name = "gb1_1_2";
            this.gb1_1_2.Size = new System.Drawing.Size(383, 120);
            this.gb1_1_2.TabIndex = 8;
            this.gb1_1_2.TabStop = false;
            this.gb1_1_2.Text = "SCHEDA 1.1.2.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "PROGRESSIVO INVIO:";
            // 
            // txtProgressivoInvio
            // 
            this.txtProgressivoInvio.Location = new System.Drawing.Point(164, 49);
            this.txtProgressivoInvio.Name = "txtProgressivoInvio";
            this.txtProgressivoInvio.Size = new System.Drawing.Size(214, 22);
            this.txtProgressivoInvio.TabIndex = 9;
            // 
            // gb1_1_3
            // 
            this.gb1_1_3.Controls.Add(this.label6);
            this.gb1_1_3.Controls.Add(this.txtFormatoTrasmissione);
            this.gb1_1_3.Location = new System.Drawing.Point(860, 67);
            this.gb1_1_3.Name = "gb1_1_3";
            this.gb1_1_3.Size = new System.Drawing.Size(383, 120);
            this.gb1_1_3.TabIndex = 9;
            this.gb1_1_3.TabStop = false;
            this.gb1_1_3.Text = "SCHEDA 1.1.3.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "FORMATO TRASMISSIONE:";
            // 
            // txtFormatoTrasmissione
            // 
            this.txtFormatoTrasmissione.Location = new System.Drawing.Point(198, 49);
            this.txtFormatoTrasmissione.Name = "txtFormatoTrasmissione";
            this.txtFormatoTrasmissione.Size = new System.Drawing.Size(180, 22);
            this.txtFormatoTrasmissione.TabIndex = 9;
            // 
            // gb1_1_4
            // 
            this.gb1_1_4.Controls.Add(this.label7);
            this.gb1_1_4.Controls.Add(this.txtCodiceDestinatario);
            this.gb1_1_4.Location = new System.Drawing.Point(28, 237);
            this.gb1_1_4.Name = "gb1_1_4";
            this.gb1_1_4.Size = new System.Drawing.Size(383, 120);
            this.gb1_1_4.TabIndex = 10;
            this.gb1_1_4.TabStop = false;
            this.gb1_1_4.Text = "SCHEDA 1.1.4.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "CODICE DESTINATARIO:";
            // 
            // txtCodiceDestinatario
            // 
            this.txtCodiceDestinatario.Location = new System.Drawing.Point(198, 49);
            this.txtCodiceDestinatario.Name = "txtCodiceDestinatario";
            this.txtCodiceDestinatario.Size = new System.Drawing.Size(180, 22);
            this.txtCodiceDestinatario.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtContattiTrasmittente);
            this.groupBox1.Location = new System.Drawing.Point(443, 237);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 120);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SCHEDA 1.1.5: CONTATTI";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "EMAIL:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(147, 84);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(214, 22);
            this.txtEmail.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "TELEFONO:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(147, 56);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(214, 22);
            this.txtTelefono.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(191, 17);
            this.label10.TabIndex = 3;
            this.label10.Text = "CONTATTI TRASMITTENTE:";
            // 
            // txtContattiTrasmittente
            // 
            this.txtContattiTrasmittente.Location = new System.Drawing.Point(203, 27);
            this.txtContattiTrasmittente.Name = "txtContattiTrasmittente";
            this.txtContattiTrasmittente.Size = new System.Drawing.Size(158, 22);
            this.txtContattiTrasmittente.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtPECDestinatario);
            this.groupBox2.Location = new System.Drawing.Point(860, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 120);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SCHEDA 1.1.6.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "PEC DESTINATARIO:";
            // 
            // txtPECDestinatario
            // 
            this.txtPECDestinatario.Location = new System.Drawing.Point(156, 49);
            this.txtPECDestinatario.Name = "txtPECDestinatario";
            this.txtPECDestinatario.Size = new System.Drawing.Size(222, 22);
            this.txtPECDestinatario.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(62, 406);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1133, 35);
            this.label12.TabIndex = 12;
            this.label12.Text = "BENE, ADESSO PUOI PROSEGUIRE CON LA SUCCESSIVA SCHEDA: CEDENTE PRESTATORE";
            // 
            // btnProsegui
            // 
            this.btnProsegui.Image = global::Fiorentino_FatturaElettronica.Properties.Resources.arrowNext;
            this.btnProsegui.Location = new System.Drawing.Point(506, 469);
            this.btnProsegui.Name = "btnProsegui";
            this.btnProsegui.Size = new System.Drawing.Size(298, 103);
            this.btnProsegui.TabIndex = 13;
            this.btnProsegui.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 697);
            this.Controls.Add(this.tbControlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "FATTURA ELETTRONICA";
            this.tbHeader.ResumeLayout(false);
            this.tbControlHeader.ResumeLayout(false);
            this.tbDatiTrasm.ResumeLayout(false);
            this.tbDatiTrasm.PerformLayout();
            this.tbControlMain.ResumeLayout(false);
            this.gb1_1_1.ResumeLayout(false);
            this.gb1_1_1.PerformLayout();
            this.gb1_1_2.ResumeLayout(false);
            this.gb1_1_2.PerformLayout();
            this.gb1_1_3.ResumeLayout(false);
            this.gb1_1_3.PerformLayout();
            this.gb1_1_4.ResumeLayout(false);
            this.gb1_1_4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tbHeader;
        private System.Windows.Forms.TabControl tbControlMain;
        private System.Windows.Forms.TabControl tbControlHeader;
        private System.Windows.Forms.TabPage tbDatiTrasm;
        private System.Windows.Forms.TabPage tbCedentePrest;
        private System.Windows.Forms.TabPage tbBody;
        private System.Windows.Forms.Button btnProsegui;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPECDestinatario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtContattiTrasmittente;
        private System.Windows.Forms.GroupBox gb1_1_4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodiceDestinatario;
        private System.Windows.Forms.GroupBox gb1_1_3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFormatoTrasmissione;
        private System.Windows.Forms.GroupBox gb1_1_2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProgressivoInvio;
        private System.Windows.Forms.GroupBox gb1_1_1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdTrasmittente;
        private System.Windows.Forms.Label label1;
    }
}

