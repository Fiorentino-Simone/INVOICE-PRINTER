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
using System.Threading;
using System.Diagnostics;

namespace Fiorentino_FatturaElettronica
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            cmbFormatoTrasmissione.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;

            //carico cmb Regime Fiscale
            for (int i = 1; i <= 19; i++)
            {
                if (i <= 9) cmbRegimeFiscale.Items.Add("RF0" + i);
                else cmbRegimeFiscale.Items.Add("RF" + i);
            }

            cmbRegimeFiscale.SelectedIndex = 0;

            /*foreach (Control c in groupBox7.Controls)
                    {
                        if (c is TextBox)
                            c.Text = "";
                    }*/
        }

        /*VARIBILE: */
        public XmlTextWriter xmlWrt;

        private void btnInviaFattura_Click(object sender, EventArgs e)
        {
            string pathXml = "fattura.xml"; //anche tramite la browse file dialog come con il notepad
            xmlWrt = new XmlTextWriter(pathXml, Encoding.UTF8);

            xmlWrt.Formatting = Formatting.Indented; //settiamo la formattazione indentata

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
            
            
            
            /*TIPS: 
             * 
             * utilizzare classi apposite per migliorare la creazione dinamica del xml
             * Uso dei thread per i controlli
             * */
        }

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
            xmlWrt.WriteElementString("Divisa", txtDivisa.Text);

            //<Data>
            xmlWrt.WriteElementString("Data", txtData.Text);

            //<Numero>
            xmlWrt.WriteElementString("Numero", txtNumero.Text);

            xmlWrt.WriteEndElement(); //DatiGeneraliDocumento
            #endregion

            xmlWrt.WriteEndElement(); //DatiGenerali
        }

        private void SoggettoEmittente()
        {
            xmlWrt.WriteElementString("SoggettoEmittente", txtSoggettoEmittente.Text);
        }

        private void TerzoIntermediario()
        {
            xmlWrt.WriteStartElement("TerzoIntermediarioOSoggettoEmittente");

            #region DatiAnagrafici
            xmlWrt.WriteStartElement("DatiAnagrafici");
            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", txtIdPaeseTerzoInterme.Text);
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
            xmlWrt.WriteElementString("IdPaese", txtIdPaeseCessionario.Text);
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
            xmlWrt.WriteElementString("Nazione", txtNazioneCessionario.Text);

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
            xmlWrt.WriteElementString("Nazione", txtNazioneCessionario.Text);

            xmlWrt.WriteEndElement(); //StabileOrganizzazione
            #endregion

            #region RappresentanteFiscale
            xmlWrt.WriteStartElement("RappresentanteFiscale");

            //<IdFiscaleIVA>
            xmlWrt.WriteStartElement("IdFiscaleIVA");
            xmlWrt.WriteElementString("IdPaese", txtIDPaeseRappFiscaleCessionario.Text);
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
            xmlWrt.WriteElementString("IdPaese", txtIDPaeseRappFiscale.Text);
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
            xmlWrt.WriteElementString("IdPaese", txtIdPaese2.Text);
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
            xmlWrt.WriteElementString("DataIscrizioneAlbo", txtDataIscrizioneAlbo.Text);

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
            xmlWrt.WriteElementString("Nazione", txtNazione.Text);
           
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
            xmlWrt.WriteElementString("Nazione", txtNazioneOrg.Text);

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
            xmlWrt.WriteElementString("SocioUnico", txtSocioUnico.Text);

            //<StatoLiquidazione>
            xmlWrt.WriteElementString("StatoLiquidazione", txtStatoLiquidazione.Text);

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
            xmlWrt.WriteElementString("IdPaese", txtIdPaese.Text);
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
    }
}
