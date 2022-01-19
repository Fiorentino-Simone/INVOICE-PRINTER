using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;//import per gli XML
using System.Threading; //import per i THREAD
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Fiorentino_FatturaElettronica
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
            //carico cmb Regime Fiscale
            for (int i = 1; i <= 19; i++)
            {
                if (i <= 9) cmbRegimeFiscale.Items.Add("RF0" + i);
                else cmbRegimeFiscale.Items.Add("RF" + i);
            }

            #region COMBO BOX
            cmbFormatoTrasmissione.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;
            cmbRegimeFiscale.SelectedIndex = 0;
            cmbPaese.SelectedIndex = 0;
            cmbDivisa.SelectedIndex = 0;
            cmbIDPaeseCessionario.SelectedIndex = 0;
            cmbIdPaeseRappFisc.SelectedIndex = 0;
            cmbIDPaeseRappFiscCessi.SelectedIndex = 0;
            cmbIDPaeseTerzoIntermediario.SelectedIndex = 0;
            cmbNazione.SelectedIndex = 0;
            cmbNazioneCessOrg.SelectedIndex = 0;
            cmbNazioneOrg.SelectedIndex = 0;
            cmbNazioneRappFisc.SelectedIndex = 0;
            cmbPaese.SelectedIndex = 0;
            cmbPaesePrestatore.SelectedIndex = 0;
            cmbRegimeFiscale.SelectedIndex = 0;
            cmbSocioUnico.SelectedIndex = 0;
            cmbSoggettoEmittente.SelectedIndex = 0;
            cmbStatoLiquidazione.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;
            #endregion
        }

        #region variabili
        public XmlTextWriter xmlWrt;
        public static Thread thread1;
        #endregion

        bool[] erroriHeader = new bool[200];
        bool[] erroriBody = new bool[50];
        
        private void btnInviaFattura_Click(object sender, EventArgs e)
        {
            bool creaFattura = true;

            /*TODO:
             * uso due thread che mi controllano gli errori del body e del header
             * appena trovano un errore lo segnano nel vettore
             * controllo tramite while se almeno un errore è presente, se cosi allora non si può creare la fattura*/

            Parallel.Invoke(controlloHeader, controlloBody);

            if (creaFattura)
            {
                string pathXml = "fattura.xml"; //anche tramite la browse file dialog come con il notepad
                xmlWrt = new XmlTextWriter(pathXml, Encoding.UTF8);
                xmlWrt.Formatting = Formatting.Indented; //settiamo la formattazione indentata

                /*INTESTAZIONE*/
                #region INTESTAZIONE
                //costruisco un nodo con WriteStartElement
                xmlWrt.WriteStartElement("p:FatturaElettronica"); //passiamo una stringa con il nome del nodo

                /*ATTRIBUTI*/
                xmlWrt.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                xmlWrt.WriteAttributeString("xmlns:p", "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2");
                xmlWrt.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xmlWrt.WriteAttributeString("versione", cmbFormatoTrasmissione.Text);
                xmlWrt.WriteAttributeString("xsi:schemaLocation", "http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2 http://www.fatturapa.gov.it/export/fatturazione/sdi/fatturapa/v1.2/Schema_del_file_xml_FatturaPA_versione_1.2.xsd");
                #endregion

                /*HEADER*/
                #region HEADER
                xmlWrt.WriteStartElement("FatturaElettronicaHeader");

                DatiTrasmissione();
                CedentePrestatore();
                RappresentanteFiscale();
                CessionarioCommittente();
                TerzoIntermediario();
                SoggettoEmittente();

                xmlWrt.WriteEndElement(); //Header
                #endregion

                /*BODY*/
                #region BODY
                xmlWrt.WriteStartElement("FatturaElettronicaBody");

                DatiGenerali();
                DatiBeniServizi();

                xmlWrt.WriteEndElement(); //Body
                #endregion

                xmlWrt.WriteEndElement(); //Chiude la fattura
                xmlWrt.Close();
                Process.Start("explorer.exe", "/select," + pathXml);
            }
            else
            {
                MessageBox.Show("Ci sono molteplici errori, andare a ricontrollare ogni casella \n per andare poi a creare correttamente la fattura", "Errore!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        #region createXMLFunctions
        private void DatiBeniServizi()
        {
            xmlWrt.WriteStartElement("DatiBeniServizi");

            #region DettaglioLinee
            xmlWrt.WriteStartElement("DettaglioLinee");

            //<NumeroLinea>
            xmlWrt.WriteElementString("NumeroLinea", txtNumeroLinea.Text);

            //<Descrizione>
            xmlWrt.WriteElementString("Descrizione", txtDescrizioneDBS.Text);

            //<PrezzoUnitario>
            xmlWrt.WriteElementString("PrezzoUnitario", txtPrezzoUnitario.Text);

            //<PrezzoTotale>
            xmlWrt.WriteElementString("PrezzoTotale", txtPrezzoTotale.Text);

            //<AliquotaIVA>
            xmlWrt.WriteElementString("AliquotaIVA", txtAliquotaIva.Text);

            xmlWrt.WriteEndElement(); //DettaglioLinee
            #endregion

            #region DatiRiepilogo
            xmlWrt.WriteStartElement("DatiRiepilogo");

            //<AliquotaIVA>
            xmlWrt.WriteElementString("AliquotaIVA", txtAliquotaIVARiepilogo.Text);
            //<ImponibileImporto>
            xmlWrt.WriteElementString("ImponibileImporto", txtImponibileImporto.Text);
            //<Imposta>
            xmlWrt.WriteElementString("Imposta", txtImposta.Text);

            xmlWrt.WriteEndElement(); //DatiRiepilogo
            #endregion

            xmlWrt.WriteEndElement(); //DatiBeniServizi
        }

        private void DatiGenerali()
        {
            xmlWrt.WriteStartElement("DatiGenerali");

            #region DatiGeneraliDocumento
            xmlWrt.WriteStartElement("DatiGeneraliDocumento");

            //<TipoDocumento>
            xmlWrt.WriteElementString("TipoDocumento", cmbTipoDocumento.Text);

            //<Divisa>
            xmlWrt.WriteElementString("Divisa", cmbDivisa.Text);

            //<Data>
            xmlWrt.WriteElementString("Data", dtpData.Value.ToString("yyyy-MM-dd"));

            //<Numero>
            xmlWrt.WriteElementString("Numero", txtNumero.Text);

            xmlWrt.WriteEndElement(); //DatiGeneraliDocumento
            #endregion

            xmlWrt.WriteEndElement(); //DatiGenerali
        }

        private void SoggettoEmittente()
        {
            xmlWrt.WriteElementString("SoggettoEmittente", cmbSoggettoEmittente.Text);
        }

        private void TerzoIntermediario()
        {
            xmlWrt.WriteStartElement("TerzoIntermediarioOSoggettoEmittente");

            #region DatiAnagrafici
            xmlWrt.WriteStartElement("DatiAnagrafici");
            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", cmbIDPaeseTerzoIntermediario.Text);
            xmlWrt.WriteElementString("IdCodice", txtIDCodiceTerzoInterme.Text);
            xmlWrt.WriteEndElement(); //<IdFiscaleIVA>

            //<CodiceFiscale>
            xmlWrt.WriteElementString("CodiceFiscale", txtCFTerzoIntermediario.Text);

            //<Anagrafica>
            xmlWrt.WriteStartElement("Anagrafica");
            xmlWrt.WriteElementString("Denominazione", txtDenominazioneTerzoIntermediario.Text);
            xmlWrt.WriteElementString("Nome", txtNomeTerzoIntermediario.Text);
            xmlWrt.WriteElementString("Cognome", txtCognomeTerzoIntermediario.Text);
            xmlWrt.WriteElementString("Titolo", txtTitoloTerzoIntermediario.Text);
            xmlWrt.WriteElementString("CodEORI", txtCodEORITerzoIntermediario.Text);
            xmlWrt.WriteEndElement();//<Anagrafica>

            xmlWrt.WriteEndElement(); //DatiAnagrafici
            #endregion

            xmlWrt.WriteEndElement(); //TerzoIntermediarioOSoggettoEmittente
        }

        private void CessionarioCommittente()
        {
            xmlWrt.WriteStartElement("CessionarioCommittente");

            #region DatiAnagrafici
            xmlWrt.WriteStartElement("DatiAnagrafici");
            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", cmbIDPaeseCessionario.Text);
            xmlWrt.WriteElementString("IdCodice", txtIdCodiceCessionario.Text);
            xmlWrt.WriteEndElement(); //<IdFiscaleIVA>

            //<CodiceFiscale>
            xmlWrt.WriteElementString("CodiceFiscale", txtCFCessionario.Text);

            //<Anagrafica>
            xmlWrt.WriteStartElement("Anagrafica");
            xmlWrt.WriteElementString("Denominazione", txtDenominazioneCessionario.Text);
            xmlWrt.WriteElementString("Nome", txtNomeCessionario.Text);
            xmlWrt.WriteElementString("Cognome", txtCognomeCessionario.Text);
            xmlWrt.WriteElementString("Titolo", txtTitoloCessionario.Text);
            xmlWrt.WriteElementString("CodEORI", txtCodEORICessionario.Text);
            xmlWrt.WriteEndElement();//<Anagrafica>

            xmlWrt.WriteEndElement(); //DatiAnagrafici
            #endregion

            #region Sede
            xmlWrt.WriteStartElement("Sede");

            //<Indirizzo>
            xmlWrt.WriteElementString("Indirizzo", txtIndirizzoCessionario.Text);

            //<NumeroCivico>
            xmlWrt.WriteElementString("NumeroCivico", txtNumeroCivicoCessionario.Text);

            //<CAP>
            xmlWrt.WriteElementString("CAP", txtCAPCessionario.Text);
            //<<Comune>>
            xmlWrt.WriteElementString("Comune", txtComuneCessionario.Text);
            //<Provincia>
            xmlWrt.WriteElementString("Provincia", txtProvinciaCessionario.Text);
            //<Nazione>
            xmlWrt.WriteElementString("Nazione", cmbNazioneRappFisc.Text);

            xmlWrt.WriteEndElement(); //Sede
            #endregion

            #region StabileOrganizzazione
            xmlWrt.WriteStartElement("StabileOrganizzazione");

            //<Indirizzo>
            xmlWrt.WriteElementString("Indirizzo", txtIndirizzoCessionario.Text);

            //<NumeroCivico>
            xmlWrt.WriteElementString("NumeroCivico", txtNumeroCivicoCessionario.Text);

            //<CAP>
            xmlWrt.WriteElementString("CAP", txtCAPCessionario.Text);
            //<<Comune>>
            xmlWrt.WriteElementString("Comune", txtComuneCessionario.Text);
            //<Provincia>
            xmlWrt.WriteElementString("Provincia", txtProvinciaCessionario.Text);
            //<Nazione>
            xmlWrt.WriteElementString("Nazione", cmbNazioneRappFisc.Text);

            xmlWrt.WriteEndElement(); //StabileOrganizzazione
            #endregion

            #region RappresentanteFiscale
            xmlWrt.WriteStartElement("RappresentanteFiscale");

            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", cmbIDPaeseRappFiscCessi.Text);
            xmlWrt.WriteElementString("IdCodice", txtIDCodiceRappFiscaleCessionario.Text);
            xmlWrt.WriteEndElement(); //<IdFiscaleIVA>

            xmlWrt.WriteElementString("Denominazione", txtDenominazioneRappFiscCessionario.Text);
            xmlWrt.WriteElementString("Nome", txtNomeRapprFiscCessionario.Text);
            xmlWrt.WriteElementString("Cognome", txtCognRappFiscaleCessionario.Text);


            xmlWrt.WriteEndElement(); //RappresentanteFiscale
            #endregion

            xmlWrt.WriteEndElement(); //CessionarioCommittente
        }

        private void RappresentanteFiscale()
        {
            xmlWrt.WriteStartElement("RappresentanteFiscale");

            #region DatiAnagrafici
            xmlWrt.WriteStartElement("DatiAnagrafici");
            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", cmbIdPaeseRappFisc.Text);
            xmlWrt.WriteElementString("IdCodice", txtIDCodiceRappFiscale.Text);
            xmlWrt.WriteEndElement(); //<IdFiscaleIVA>

            //<CodiceFiscale>
            xmlWrt.WriteElementString("CodiceFiscale", txtCFRappFiscale.Text);

            //<Anagrafica>
            xmlWrt.WriteStartElement("Anagrafica");
            xmlWrt.WriteElementString("Denominazione", txtDenominazioneRappFiscale.Text);
            xmlWrt.WriteElementString("Nome", txtNomeRappFiscale.Text);
            xmlWrt.WriteElementString("Cognome", txtCognomeRappFiscale.Text);
            xmlWrt.WriteElementString("Titolo", txtTitoloRappFiscale.Text);
            xmlWrt.WriteElementString("CodEORI", txtCodEORIRappFiscale.Text);
            xmlWrt.WriteEndElement();//<Anagrafica>

            xmlWrt.WriteEndElement(); //DatiAnagrafici
            #endregion

            xmlWrt.WriteEndElement(); //RappresentanteFiscale
        }

        private void CedentePrestatore()
        {
            xmlWrt.WriteStartElement("CedentePrestatore");

            #region DatiAnagrafici
            xmlWrt.WriteStartElement("DatiAnagrafici");
            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", cmbPaesePrestatore.Text);
            xmlWrt.WriteElementString("IdCodice", txtCodice2.Text);
            xmlWrt.WriteEndElement(); //<IdFiscaleIVA>

            //<CodiceFiscale>
            xmlWrt.WriteElementString("CodiceFiscale",txtCFAnagrafica.Text);

            //<Anagrafica>
            xmlWrt.WriteStartElement("Anagrafica");
            xmlWrt.WriteElementString("Denominazione", txtDenominazione.Text);
            xmlWrt.WriteElementString("Nome", txtNome.Text);
            xmlWrt.WriteElementString("Cognome", txtCognome.Text);
            xmlWrt.WriteElementString("Titolo", txtTitolo.Text);
            xmlWrt.WriteElementString("CodEORI", txtCodEORI.Text);
            xmlWrt.WriteEndElement();//<Anagrafica>

            //<AlboProfessionale>
            xmlWrt.WriteElementString("AlboProfessionale", txtAlboProfessionale.Text);

            //<ProvinciaAlbo>
            xmlWrt.WriteElementString("ProvinciaAlbo", txtProvinciaAlbo.Text);

            //<<NumeroIscrizioneAlbo>>
            xmlWrt.WriteElementString("NumeroIscrizioneAlbo", txtNumIscrAlbo.Text);

            //<<DataIscrizioneAlbo>>
            xmlWrt.WriteElementString("DataIscrizioneAlbo", dtAlbo.Value.ToString("yyyy-MM-dd"));

            //<RegimeFiscale>
            xmlWrt.WriteElementString("RegimeFiscale", cmbRegimeFiscale.Text);
            xmlWrt.WriteEndElement(); //DatiAnagrafici
            #endregion

            #region Sede
            xmlWrt.WriteStartElement("Sede");

            //<Indirizzo>
            xmlWrt.WriteElementString("Indirizzo", txtIndirizzo.Text);

            //<NumeroCivico>
            xmlWrt.WriteElementString("NumeroCivico", txtNumeroCivico.Text);

            //<CAP>
            xmlWrt.WriteElementString("CAP", txtCAP.Text);
            //<<Comune>>
            xmlWrt.WriteElementString("Comune", txtComune.Text);
            //<Provincia>
            xmlWrt.WriteElementString("Provincia", txtProvincia.Text);
            //<Nazione>
            xmlWrt.WriteElementString("Nazione", cmbNazione.Text);
           
            xmlWrt.WriteEndElement(); //Sede
            #endregion

            #region StabileOrganizzazione
            xmlWrt.WriteStartElement("StabileOrganizzazione");

            //<Indirizzo>
            xmlWrt.WriteElementString("Indirizzo", txtIndirizzoOrg.Text);

            //<NumeroCivico>
            xmlWrt.WriteElementString("NumeroCivico", txtNumeroCivicoOrg.Text);

            //<CAP>
            xmlWrt.WriteElementString("CAP", txtCAPOrg.Text);
            //<<Comune>>
            xmlWrt.WriteElementString("Comune", txtComuneOrg.Text);
            //<Provincia>
            xmlWrt.WriteElementString("Provincia", txtProvinciaOrg.Text);
            //<Nazione>
            xmlWrt.WriteElementString("Nazione", cmbNazioneOrg.Text);

            xmlWrt.WriteEndElement(); //StabileOrganizzazione
            #endregion

            #region IscrizioneREA
            xmlWrt.WriteStartElement("IscrizioneREA");

            //<Ufficio>
            xmlWrt.WriteElementString("Ufficio", txtUfficio.Text);

            //<NumeroREA>
            xmlWrt.WriteElementString("NumeroREA", txtNumeroREA.Text);

            //<CapitaleSociale>
            xmlWrt.WriteElementString("CapitaleSociale", txtCapitaleSociale.Text);

            //<SocioUnico>
            xmlWrt.WriteElementString("SocioUnico", cmbSocioUnico.Text);

            //<StatoLiquidazione>
            xmlWrt.WriteElementString("StatoLiquidazione", cmbStatoLiquidazione.Text);

            xmlWrt.WriteEndElement(); //IscrizioneREA
            #endregion

            #region Contatti
            xmlWrt.WriteStartElement("Contatti");

            //<Telefono>
            xmlWrt.WriteElementString("Telefono", txtContattiPrestatore.Text);
            //<Fax>
            xmlWrt.WriteElementString("Fax", txtFaxPrestatore.Text);
            //<Email>
            xmlWrt.WriteElementString("Email", txtEmailPrestatore.Text);

            xmlWrt.WriteEndElement(); //Contatti
            #endregion

            xmlWrt.WriteElementString("RiferimentoAmministrazione", txtRiferimentoAmministrazione.Text);
            xmlWrt.WriteEndElement(); //DatiTrasmissione
        }

        private void DatiTrasmissione()
        {
            xmlWrt.WriteStartElement("DatiTrasmissione");

            //IdTrasmittente
            xmlWrt.WriteStartElement("IdTrasmittente");
            xmlWrt.WriteElementString("IdPaese", cmbPaese.Text);
            xmlWrt.WriteElementString("IdCodice", txtIdCodice.Text);
            xmlWrt.WriteEndElement(); //IdTrasmittente

            //progressivo invio
            xmlWrt.WriteElementString("ProgressivoInvio", txtProgressivoInvio.Text);

            //formato trasmissione
            xmlWrt.WriteElementString("FormatoTrasmissione", cmbFormatoTrasmissione.Text);

            //Codice destinatario
            xmlWrt.WriteElementString("CodiceDestinatario", txtCodiceDestinatario.Text);

            //<ContattiTrasmittente>
            xmlWrt.WriteStartElement("ContattiTrasmittente");
            xmlWrt.WriteElementString("Telefono", txtTelefono.Text);
            xmlWrt.WriteElementString("Email", txtEmail.Text);
            xmlWrt.WriteEndElement(); //<ContattiTrasmittente>

            //<PECDestinatario>
            xmlWrt.WriteElementString("PECDestinatario", txtPECDestinatario.Text);

            xmlWrt.WriteEndElement(); //DatiTrasmissione
        }

        #endregion

        private void btnProseguiBody_Click(object sender, EventArgs e)
        {
            tbControlMain.SelectTab(1); //seleziona la tabControl Body
        }

        private void txtsAlfanumerici_TextChanged(object sender, EventArgs e)
        {
            //qua gestiamo gli errori alfanumerici
            Regex alfanumerico = new Regex(@"^[0-9a-z-A-Z]+$");
            TextBox txt = (TextBox)sender;
            if (alfanumerico.IsMatch(txt.Text) && txt.Text != "")
                txt.BackColor = Color.White;
            else
                txt.BackColor = Color.FromArgb(255,77,77);
        }

        #region funzioniThread
        private void controlloHeader()
        {
            //controllo header andando tramite for a scorrere i erroriHeader
        }

        private void controlloBody()
        {
            //controllo body andando ad usare il foreach

            /*
                * bool ok = true;
            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox)
                    if (c.ForeColor == Color.Red)
                        ok = false;
            }*/
}
        #endregion

        private void txtsProvincie_TextChanged(object sender, EventArgs e)
        {
            Regex provincia = new Regex(@"^[A-Z]{2}+$");
            TextBox txt = (TextBox)sender;
            if (provincia.IsMatch(txt.Text) && txt.Text != "")
                txt.BackColor = Color.White;
            else
                txt.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void txtsNumericoCAP_TextChanged(object sender, EventArgs e)
        {
            Regex CAP=new Regex(@"^[0-9]{5}$");
            TextBox txt = (TextBox)sender;
            if (CAP.IsMatch(txt.Text) && txt.Text != "")
                txt.BackColor = Color.White;
            else
                txt.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void txtsDecimalPoint_TextChanged(object sender, EventArgs e)
        {
            Regex numeriDecimaliConPuntino = new Regex(@"^[0-9]+(\.*[0-9]+)*$");
            TextBox txt = (TextBox)sender;
            if (numeriDecimaliConPuntino.IsMatch(txt.Text) && txt.Text != "")
                txt.BackColor = Color.White;
            else
                txt.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void txtsNumerico_TextChanged(object sender, EventArgs e)
        {
            Regex numerico = new Regex(@"^\d+$");
            TextBox txt = (TextBox)sender;
            if (numerico.IsMatch(txt.Text) && txt.Text != "")
                txt.BackColor = Color.White;
            else
                txt.BackColor = Color.FromArgb(255, 77, 77);
        }
    }
}
