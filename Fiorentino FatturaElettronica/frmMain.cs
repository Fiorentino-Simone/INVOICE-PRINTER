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
using Microsoft.VisualBasic;

namespace Fiorentino_FatturaElettronica
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();

            #region COMBO BOX
            //carico cmb Regime Fiscale
            for (int i = 1; i <= 19; i++)
            {
                if (i <= 9) cmbRegimeFiscale.Items.Add("RF0" + i);
                else cmbRegimeFiscale.Items.Add("RF" + i);
            }

            //cambio il selectedIndex
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
        #endregion

        #region Vettori 
        bool[] erroriHeader = new bool[70];
        bool[] erroriBody = new bool[10];
        #endregion

        #region STRUCT
        public struct dettaglioLinea  
        {
            public int numeroLinea; 
            public string descrizione;
            public string prezzoUnitario;
            public string prezzoTotale;
            public string aliquotaIva;
        }
        dettaglioLinea[] dettaglioLinee = new dettaglioLinea[200];
        int index=0;//index per i valori di dettaglio linee

        public struct datiRiepilogo
        {
            public string aliquotaIva;
            public string imponibileImporto;
            public string imposta;
        }
        datiRiepilogo[] DatiRiepilogo = new datiRiepilogo[200];
        int indexRiepilogo = 0;//index per i valori di riepilogo
        #endregion

        #region HelpFunction
        private void resetVettBool()
        {
            for (int i = 0; i < erroriBody.Length; i++)
                erroriBody[i] = false;

            for (int i = 0; i < erroriHeader.Length; i++)
                erroriHeader[i] = false;
        }
        #endregion

        #region createXMLFunctions
        private void DatiBeniServizi()
        {
            xmlWrt.WriteStartElement("DatiBeniServizi");

            #region DettaglioLinee
            for (int i = 0; i < index; i++)
            {
                xmlWrt.WriteStartElement("DettaglioLinee");

                //<NumeroLinea>
                xmlWrt.WriteElementString("NumeroLinea", dettaglioLinee[i].numeroLinea.ToString());

                //<Descrizione>
                xmlWrt.WriteElementString("Descrizione", dettaglioLinee[i].descrizione.ToString());

                //<PrezzoUnitario>
                xmlWrt.WriteElementString("PrezzoUnitario", dettaglioLinee[i].prezzoUnitario.ToString());

                //<PrezzoTotale>
                xmlWrt.WriteElementString("PrezzoTotale", dettaglioLinee[i].prezzoTotale.ToString());

                //<AliquotaIVA>
                xmlWrt.WriteElementString("AliquotaIVA", dettaglioLinee[i].aliquotaIva.ToString());

                xmlWrt.WriteEndElement(); //DettaglioLinee
            }
            #endregion

            #region DatiRiepilogo
            for (int i = 0; i < indexRiepilogo; i++)
            {
                xmlWrt.WriteStartElement("DatiRiepilogo");

                //<AliquotaIVA>
                xmlWrt.WriteElementString("AliquotaIVA", DatiRiepilogo[i].aliquotaIva.ToString());
                //<ImponibileImporto>
                xmlWrt.WriteElementString("ImponibileImporto", DatiRiepilogo[i].imponibileImporto.ToString());
                //<Imposta>
                xmlWrt.WriteElementString("Imposta", DatiRiepilogo[i].imposta.ToString());

                xmlWrt.WriteEndElement(); //DatiRiepilogo
            }
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
            if (!(txtIDCodiceTerzoInterme.Text == "" && txtCFTerzoIntermediario.Text == "" && txtDenominazioneTerzoIntermediario.Text == "" && txtNomeTerzoIntermediario.Text == "" && txtCognomeTerzoIntermediario.Text == "" && txtTitoloTerzoIntermediario.Text == "" && txtCodEORITerzoIntermediario.Text == ""))
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
        }

        private void CessionarioCommittente()
        {
            xmlWrt.WriteStartElement("CessionarioCommittente");

            #region DatiAnagrafici
            xmlWrt.WriteStartElement("DatiAnagrafici");
            //<IdFiscaleIVA>
            if (!(txtIdCodiceCessionario.Text == ""))
            {
                xmlWrt.WriteStartElement("IdFiscaleIVA");
                xmlWrt.WriteElementString("IdPaese", cmbIDPaeseCessionario.Text);
                xmlWrt.WriteElementString("IdCodice", txtIdCodiceCessionario.Text);
                xmlWrt.WriteEndElement(); //<IdFiscaleIVA>
            }

            //<CodiceFiscale>
            if (!(txtCFCessionario.Text == "")) xmlWrt.WriteElementString("CodiceFiscale", txtCFCessionario.Text);

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
            if (!(txtNumeroCivicoCessionario.Text == "")) xmlWrt.WriteElementString("NumeroCivico", txtNumeroCivicoCessionario.Text);

            //<CAP>
            xmlWrt.WriteElementString("CAP", txtCAPCessionario.Text);
            //<<Comune>>
            xmlWrt.WriteElementString("Comune", txtComuneCessionario.Text);
            //<Provincia>
            if (!(txtProvinciaCessionario.Text == "")) xmlWrt.WriteElementString("Provincia", txtProvinciaCessionario.Text);
            //<Nazione>
            xmlWrt.WriteElementString("Nazione", cmbNazioneRappFisc.Text);

            xmlWrt.WriteEndElement(); //Sede
            #endregion

            #region StabileOrganizzazione
            if (!(txtIndirizzoCessionario.Text == "" && txtNumeroCivicoCessionario.Text == "" && txtCAPCessionario.Text == "" && txtComuneCessionario.Text == "" && txtProvinciaCessionario.Text == ""))
            {
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
            }
            #endregion

            #region RappresentanteFiscale
            if (!(txtIDCodiceRappFiscaleCessionario.Text == "" && txtDenominazioneRappFiscCessionario.Text == "" && txtNomeRapprFiscCessionario.Text == "" && txtCognRappFiscaleCessionario.Text == ""))
            {
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
            }
            #endregion

            xmlWrt.WriteEndElement(); //CessionarioCommittente
        }

        private void RappresentanteFiscale()
        {
            if (!(txtIDCodiceRappFiscale.Text == "" && txtCFRappFiscale.Text == "" && txtDenominazioneRappFiscale.Text == "" && txtNomeRappFiscale.Text == "" && txtCognomeRappFiscale.Text == "" && txtTitoloRappFiscale.Text == "" && txtCodEORIRappFiscale.Text == ""))
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
            if (!(txtCFAnagrafica.Text == "")) xmlWrt.WriteElementString("CodiceFiscale",txtCFAnagrafica.Text);

            //<Anagrafica>
            xmlWrt.WriteStartElement("Anagrafica");
            xmlWrt.WriteElementString("Denominazione", txtDenominazione.Text);
            xmlWrt.WriteElementString("Nome", txtNome.Text);
            xmlWrt.WriteElementString("Cognome", txtCognome.Text);
            xmlWrt.WriteElementString("Titolo", txtTitolo.Text);
            xmlWrt.WriteElementString("CodEORI", txtCodEORI.Text);
            xmlWrt.WriteEndElement();//<Anagrafica>

            //<AlboProfessionale>
            if (!(txtAlboProfessionale.Text == "")) xmlWrt.WriteElementString("AlboProfessionale", txtAlboProfessionale.Text);

            //<ProvinciaAlbo>
            if (!(txtProvinciaAlbo.Text == "")) xmlWrt.WriteElementString("ProvinciaAlbo", txtProvinciaAlbo.Text);

            //<<NumeroIscrizioneAlbo>>
            if (!(txtNumIscrAlbo.Text == "")) xmlWrt.WriteElementString("NumeroIscrizioneAlbo", txtNumIscrAlbo.Text);

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
            if (!(txtNumeroCivico.Text == "")) xmlWrt.WriteElementString("NumeroCivico", txtNumeroCivico.Text);

            //<CAP>
            xmlWrt.WriteElementString("CAP", txtCAP.Text);
            //<<Comune>>
            xmlWrt.WriteElementString("Comune", txtComune.Text);
            //<Provincia>
            if (!(txtProvincia.Text == "")) xmlWrt.WriteElementString("Provincia", txtProvincia.Text);
            //<Nazione>
            xmlWrt.WriteElementString("Nazione", cmbNazione.Text);
           
            xmlWrt.WriteEndElement(); //Sede
            #endregion

            #region StabileOrganizzazione
            if (!(txtIndirizzoOrg.Text == "" && txtNumeroCivicoOrg.Text == "" && txtCAPOrg.Text=="" && txtComuneOrg.Text=="" && txtProvinciaOrg.Text==""))
            {
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
            }
            #endregion

            #region IscrizioneREA
            if (!(txtUfficio.Text == "" && txtNumeroREA.Text == "" && txtCapitaleSociale.Text == ""))
            {
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
            }
            #endregion

            #region Contatti
            if (!(txtContattiPrestatore.Text == "" && txtFaxPrestatore.Text == "" && txtEmailPrestatore.Text == ""))
            {
                xmlWrt.WriteStartElement("Contatti");

                //<Telefono>
                xmlWrt.WriteElementString("Telefono", txtContattiPrestatore.Text);
                //<Fax>
                xmlWrt.WriteElementString("Fax", txtFaxPrestatore.Text);
                //<Email>
                xmlWrt.WriteElementString("Email", txtEmailPrestatore.Text);

                xmlWrt.WriteEndElement(); //Contatti
            }
            #endregion

            if (!(txtRiferimentoAmministrazione.Text == "")) xmlWrt.WriteElementString("RiferimentoAmministrazione", txtRiferimentoAmministrazione.Text);
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
            if(!(txtTelefono.Text=="" && txtEmail.Text==""))
            {
                xmlWrt.WriteStartElement("ContattiTrasmittente");
                if(!(txtTelefono.Text=="")) xmlWrt.WriteElementString("Telefono", txtTelefono.Text);
                if (!(txtEmail.Text == "")) xmlWrt.WriteElementString("Email", txtEmail.Text);
                xmlWrt.WriteEndElement(); //<ContattiTrasmittente>
            }


            //<PECDestinatario>
            if (!(txtPECDestinatario.Text == "")) xmlWrt.WriteElementString("PECDestinatario", txtPECDestinatario.Text);

            xmlWrt.WriteEndElement(); //DatiTrasmissione
        }

        #endregion

        #region funzioniThread
        private void controlloHeader()
        {
            int k = 0;

            #region DatiTrasmissione tabPage
            foreach (Control element in tbDatiTrasm.Controls)
            {
                if (element is GroupBox)
                {
                    if (!((element.Name == gb1_1_5.Name) || (element.Name == gb1_1_6.Name))) 
                    {
                        foreach (Control gr in element.Controls)
                        {
                            if (gr is TextBox)
                            {
                                if ((gr.BackColor == Color.FromArgb(255, 77, 77)) || gr.Text.Length == 0)
                                {
                                    erroriHeader[k] = true;
                                    k++;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 1^Cedente prestatore pages
            //1^Parte: 2 livello
            foreach (Control element in tbCedentePrestatore1.Controls)
            {
                if (element is GroupBox)
                {
                    foreach (Control gr in element.Controls)
                    {
                        if (!((gr.Name == gb1_2_1_2.Name) || (gr.Name == gb1_2_1_4.Name) || (gr.Name == gb1_2_1_5.Name) || (gr.Name == gb1_2_1_6.Name) || (gr.Name == gb1_2_2_2.Name) || (gr.Name == gb1_2_2_5.Name)))
                        {
                            foreach (Control item in gr.Controls)
                            {
                                if (item is TextBox)
                                {
                                    if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                    {
                                        erroriHeader[k] = true;
                                        k++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region 2^Cedente prestatore pages
            //1^Parte: 2 livello
            foreach (Control element in tbCedentePrestatore2.Controls)
            {
                if (element is GroupBox)
                {
                    if (!((element.Name == gb1_2_3.Name) || (element.Name == gb1_2_4.Name))) 
                    {
                        foreach (Control gr in element.Controls)
                        {
                            foreach (Control item in gr.Controls)
                            {
                                if (item is TextBox)
                                {
                                    if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                    {
                                        erroriHeader[k] = true;
                                        k++;
                                    }
                                }
                            }
                        }
                    }
                        
                }
            }
            //2^parte: 1 livello
            foreach (Control element in tbCedentePrestatore2.Controls)
            {
                if (element is GroupBox)
                {
                    if (!((element.Name == gb1_2_5.Name) || (element.Name == gb1_2_6.Name)))
                    {
                        foreach (Control gr in element.Controls)
                        {
                            if (gr is TextBox)
                            {
                                if ((gr.BackColor == Color.FromArgb(255, 77, 77)) || gr.Text.Length == 0)
                                {
                                    erroriHeader[k] = true;
                                    k++;
                                }
                            }
                        }
                    }                        
                }
            }
            #endregion
            
            #region Rappresentante Fiscale pages
            //1^Parte: 2 livello
            foreach (Control element in tbRappFiscale.Controls)
            {
                if (element is GroupBox)
                {
                    if (!((element.Name == gb1_3.Name)))
                    {
                        foreach (Control gr in element.Controls)
                        {
                            foreach (Control item in gr.Controls)
                            {
                                if (item is TextBox)
                                {
                                    if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                    {
                                        erroriHeader[k] = true;
                                        k++;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            #endregion

            #region 1^Cessionario Committente pages
            //1^Parte: 2 livello
            foreach (Control element in tbCessionarioCommittente1.Controls)
            {
                if (element is GroupBox)
                {
                    foreach (Control gr in element.Controls)
                    {
                        if (!((gr.Name == gb1_4_1_1.Name) || (gr.Name == gb1_4_1_2.Name) || (gr.Name == gb1_4_2_2.Name) || (gr.Name == gb1_4_2_5.Name)))
                        {
                            foreach (Control item in gr.Controls)
                            {
                                if (item is TextBox)
                                {
                                    if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                    {
                                        erroriHeader[k] = true;
                                        k++;
                                    }
                                }
                            }
                        }   
                    }
                }
            }
            #endregion

            #region 2^Cessionario Committente pages
            //1^Parte: 2 livello
            foreach (Control element in tbCessionarioCommittente2.Controls)
            {
                if (element is GroupBox)
                {
                    if (!((element.Name == gb1_4_3.Name) || (element.Name == gb1_4_4.Name)))
                    {
                        foreach (Control gr in element.Controls)
                        {
                            foreach (Control item in gr.Controls)
                            {
                                if (item is TextBox)
                                {
                                    if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                    {
                                        erroriHeader[k] = true;
                                        k++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region Terzo Intermediario pages
            //1^Parte: 2 livello
            foreach (Control element in tbTerzoIntermediario.Controls)
            {
                if (element is GroupBox)
                {
                    if (!((element.Name == gb1_5_1.Name)))
                    {
                        foreach (Control gr in element.Controls)
                        {
                            foreach (Control item in gr.Controls)
                            {
                                if (item is TextBox)
                                {
                                    if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                    {
                                        erroriHeader[k] = true;
                                        k++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void controlloBody()
        {
            //controllo body andando ad usare il foreach
            int k = 0;
            #region Dati generali pages
            //1^Parte: 2 livello
            foreach (Control element in tbDatiGenerali.Controls)
            {
                if (element is GroupBox)
                {
                    foreach (Control gr in element.Controls)
                    {
                        foreach (Control item in gr.Controls)
                        {
                            if (item is TextBox)
                            {
                                if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                {
                                    erroriBody[k] = true;
                                    k++;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region Dati beni e servizi pages
            //1^Parte: 2 livello
            foreach (Control element in tbDatiBeniServizi.Controls)
            {
                if (element is GroupBox)
                {
                    foreach (Control gr in element.Controls)
                    {
                        foreach (Control item in gr.Controls)
                        {
                            if (item is TextBox)
                            {
                                if ((item.BackColor == Color.FromArgb(255, 77, 77)) || item.Text.Length == 0)
                                {
                                    erroriBody[k] = true;
                                    k++;
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region controlliTextChanged
        private void txtsAlfanumerici_TextChanged(object sender, EventArgs e)
        {
            //qua gestiamo gli errori alfanumerici
            Regex alfanumerico = new Regex(@"^[0-9a-z-A-Z]+$");
            TextBox txt = (TextBox)sender;
            if (alfanumerico.IsMatch(txt.Text) && txt.Text != "")
                txt.BackColor = Color.White;
            else
                txt.BackColor = Color.FromArgb(255, 77, 77);
        }

        private void txtsProvincie_TextChanged(object sender, EventArgs e)
        {
            Regex provincia = new Regex(@"^[A-Z]{2}$");
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
        #endregion

        #region buttonClick
        private void btnInviaFattura_Click(object sender, EventArgs e)
        {
            bool creaFattura = true;

            Parallel.Invoke(controlloHeader, controlloBody); //controllo parallelo con thread

            //siccome c# ordina per true i valori nei vettori bool, si controlla se già solo il primo vale true; se così allora vuol dire che ce un errore
            if (erroriHeader[0] == true || erroriBody[0] == true) creaFattura = false;

            if (creaFattura)
            {
                string pathXml = Interaction.InputBox("Inserire il nome della fattura", "Nome fattura");
                if (pathXml == "") pathXml = "fattura";
                xmlWrt = new XmlTextWriter(pathXml + ".xml", Encoding.UTF8);
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
                Process.Start("explorer.exe", "/select," + pathXml + ".xml");
            }
            else
                MessageBox.Show("Ci sono molteplici errori, andare a ricontrollare ogni casella \n per andare poi a creare correttamente la fattura", "Errore!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            resetVettBool(); //porto di nuovo a FALSE i vettori bool
        }

        private void btnProseguiBody_Click(object sender, EventArgs e)
        {
            tbControlMain.SelectTab(1); //seleziona la tabControl Body
        }

        private void btnAggiungiLinea_Click(object sender, EventArgs e)
        {
            if (!(txtNumeroLinea.Text == "" || txtDescrizioneDBS.Text == "" || txtPrezzoUnitario.Text == "" || txtPrezzoTotale.Text == "" || txtAliquotaIva.Text == ""))
            {
                try
                {
                    dettaglioLinee[index].numeroLinea = Convert.ToInt32(txtNumeroLinea.Text);
                    dettaglioLinee[index].descrizione = txtDescrizioneDBS.Text;
                    dettaglioLinee[index].prezzoUnitario = txtPrezzoUnitario.Text;
                    dettaglioLinee[index].prezzoTotale = txtPrezzoTotale.Text;
                    dettaglioLinee[index].aliquotaIva = txtAliquotaIva.Text;
                    index++;
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserisci bene i dati prima di premere il pulsante", "Errore!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Inserisci bene i dati prima di premere il pulsante", "Errore!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnAggiungiRiepilogo_Click(object sender, EventArgs e)
        {
            if (!(txtAliquotaIVARiepilogo.Text == "" || txtImponibileImporto.Text == "" || txtImposta.Text == ""))
            {
                try
                {
                    DatiRiepilogo[indexRiepilogo].aliquotaIva = txtAliquotaIVARiepilogo.Text;
                    DatiRiepilogo[indexRiepilogo].imponibileImporto = txtImponibileImporto.Text;
                    DatiRiepilogo[indexRiepilogo].imposta = txtImposta.Text;
                    indexRiepilogo++;
                }
                catch (Exception)
                {
                    MessageBox.Show("Inserisci bene i dati prima di premere il pulsante", "Errore!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Inserisci bene i dati prima di premere il pulsante", "Errore!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}
