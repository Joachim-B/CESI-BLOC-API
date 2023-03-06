using ClientLourdBloc.API;
using ClientLourdBloc.API.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClientLourdBloc
{
    public partial class MainPage : Form
    {
        #region Variables

        private List<int> idSites = new List<int>();
        private List<int> idServices = new List<int>();

        private string currentTab = "employees";
        private int rowIndex = -1;
        private bool addRow = false;

        #endregion

        #region Constructor

        public MainPage()
        {
            InitializeComponent();

            flpMain.AutoSize = true;
            pnlFicheEmployee.Location = new Point(0, 0);
            pnlFicheService.Location = new Point(0, 0);
            pnlFicheSite.Location = new Point(0, 0);

            LoadEmployees();
            LoadSiteComboBoxes();
            LoadServiceComboBoxes();

            // Sélection de ce contrôle pour retourner en haut de la page
            pnlMain.Select();
        }

        #endregion

        #region Récupérer données GridView

        private void LoadEmployees()
        {
            List<Employee> employees = APIRequest.GetAllEmployees();

            LoadEmployeeDataGridView(employees);
        }

        private void LoadFilteredEmployees()
        {
            string firstname = tbFilterFirstname.Text;
            string lastname = tbFilterLastname.Text;
            int idService = idServices[cbFilterService.SelectedIndex];
            int idSite = idSites[cbFilterSite.SelectedIndex];

            List<string> filter = new List<string>();

            if (!string.IsNullOrEmpty(firstname))
            {
                filter.Add("firstname=" + firstname);
            }

            if (!string.IsNullOrEmpty(lastname))
            {
                filter.Add("lastname=" + lastname);
            }

            if (idService != -1)
            {
                filter.Add("service=" + idService);
            }

            if (idSite != -1)
            {
                filter.Add("site=" + idSite);
            }

            List<Employee> employees;

            if (filter.Count > 0)
            {
                employees = APIRequest.GetAllEmployeesFiltered(string.Join("&", filter));
            }
            else
            {
                employees = APIRequest.GetAllEmployees();
            }

            LoadEmployeeDataGridView(employees);
        }

        private void LoadServices()
        {
            List<Service> services = APIRequest.GetAllServices();

            LoadServiceDataGridView(services);
        }

        private void LoadSites()
        {
            List<Site> sites = APIRequest.GetAllSites();

            LoadSiteDataGridView(sites);
        }

        #endregion

        #region Remplir GridViews

        private void LoadEmployeeDataGridView(List<Employee> employees)
        {
            dgvEmployees.Rows.Add("hello");
            int rowHeight = dgvEmployees.Rows[0].Height;

            dgvEmployees.Rows.Clear();
            dgvEmployees.Height = 60;

            foreach (Employee employee in employees)
            {
                dgvEmployees.Rows.Add(employee.IDEmployee, employee.Firstname, employee.Lastname, employee.HomePhone, employee.MobilePhone, employee.Email, employee.Service.Name, employee.Site.City);
                dgvEmployees.Height += rowHeight;
            }
        }

        private void LoadServiceDataGridView(List<Service> services)
        {
            dgvServices.Rows.Add("hello");
            int rowHeight = dgvServices.Rows[0].Height;

            dgvServices.Rows.Clear();
            dgvServices.Height = 60;

            foreach (Service service in services)
            {
                dgvServices.Rows.Add(service.IDService, service.Name);
                dgvServices.Height += rowHeight;
            }
        }

        private void LoadSiteDataGridView(List<Site> sites)
        {
            dgvSites.Rows.Add("hello");
            int rowHeight = dgvSites.Rows[0].Height;

            dgvSites.Rows.Clear();
            dgvSites.Height = 60;

            foreach (Site site in sites)
            {
                dgvSites.Rows.Add(site.IDSite, site.City);
                dgvSites.Height += rowHeight;
            }
        }

        #endregion

        #region Remplir ComboBox

        private void LoadSiteComboBoxes()
        {
            cbFilterSite.Items.Clear();
            cbSiteEmployee.Items.Clear();
            idSites = new List<int>();

            List<Site> list = APIRequest.GetAllSites();

            cbFilterSite.Items.Add("<Sélectionner>");
            cbSiteEmployee.Items.Add("<Sélectionner>");
            idSites.Add(-1);

            foreach (Site site in list)
            {
                cbFilterSite.Items.Add(site.City);
                cbSiteEmployee.Items.Add(site.City);
                idSites.Add(site.IDSite);
            }

            cbFilterSite.SelectedIndex = 0;
        }

        private void LoadServiceComboBoxes()
        {
            cbFilterService.Items.Clear();
            cbServiceEmployee.Items.Clear();
            idServices = new List<int>();

            List<Service> list = APIRequest.GetAllServices();

            cbFilterService.Items.Add("<Sélectionner>");
            cbServiceEmployee.Items.Add("<Sélectionner>");
            idServices.Add(-1);

            foreach (Service site in list)
            {
                cbFilterService.Items.Add(site.Name);
                cbServiceEmployee.Items.Add(site.Name);
                idServices.Add(site.IDService);
            }

            cbFilterService.SelectedIndex = 0;
        }

        #endregion

        #region Events

        private void btnRecherche_Click(object sender, EventArgs e)
        {
            LoadFilteredEmployees();
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;

            if (rowIndex == -1)
            {
                ResetFicheEmployee();
                return;
            }

            addRow = false;

            string firstName = dgvEmployees["Firstname", rowIndex].Value.ToString();
            string lastName = dgvEmployees["Lastname", rowIndex].Value.ToString();

            lbEnteteFicheEmploye.Text = firstName + " " + lastName;

            tbPrenomEmployee.Text = firstName;
            tbNomEmployee.Text = lastName;
            tbHomePhone.Text = dgvEmployees["HomePhone", rowIndex].Value.ToString();
            tbMobilePhone.Text = dgvEmployees["MobilePhone", rowIndex].Value.ToString();
            tbEmail.Text = dgvEmployees["Email", rowIndex].Value.ToString();
            tbServiceEmployee.Text = dgvEmployees["Service", rowIndex].Value.ToString();
            cbServiceEmployee.Text = dgvEmployees["Service", rowIndex].Value.ToString();
            tbSiteEmployee.Text = dgvEmployees["Site", rowIndex].Value.ToString();
            cbSiteEmployee.Text = dgvEmployees["Site", rowIndex].Value.ToString();
        }

        private void dgvServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;

            if (rowIndex == -1)
            {
                ResetFicheService();
                return;
            }
            addRow = false;

            string service = dgvServices["NameColumn", rowIndex].Value.ToString();

            tbServiceNom.Text = service;
            lbServiceEnteteFiche.Text = service;
        }

        private void dgvSites_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;

            if (rowIndex == -1)
            {
                ResetFicheSite();
                return;
            }
            addRow = false;

            string site = dgvSites["City", rowIndex].Value.ToString();

            tbVilleSite.Text = site;
            lbSiteEnteteFiche.Text = site;
        }

        #endregion

        #region Connexion Admin

        // Source : https://stackoverflow.com/questions/400113/best-way-to-implement-keyboard-shortcuts-in-a-windows-forms-application
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F12))
            {
                AdminLoginPage adminLogin = new AdminLoginPage();

                adminLogin.AdminLogged += OnAdminLogged;
                adminLogin.ShowDialog();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OnAdminLogged(object sender, EventArgs e)
        {
            lbBtnEmployee.Visible = lbBtnServices.Visible = lbBtnSites.Visible = true;
            this.Text = "BLOC'Annuaire (mode admin)";

            tbPrenomEmployee.ReadOnly = false;
            tbNomEmployee.ReadOnly = false;

            tbHomePhone.ReadOnly = false;
            tbMobilePhone.ReadOnly = false;
            tbEmail.ReadOnly = false;
            btnAddEmployee.Visible = true;
            btnSaveEmployee.Visible = true;
            btnDeleteEmployee.Visible = true;

            // Changement champs Services / SiteColumn de la fiche Employee
            cbServiceEmployee.Location = tbServiceEmployee.Location;
            cbSiteEmployee.Location = tbSiteEmployee.Location;
            tbServiceEmployee.Visible = tbSiteEmployee.Visible = false;
            cbServiceEmployee.Visible = cbSiteEmployee.Visible = true;
        }

        #endregion

        #region Boutons onglets

        private void ResetTabData()
        {
            rowIndex = -1;
            addRow = false;

            pnlContainerEmployees.Visible = false;
            pnlContainerServices.Visible = false;
            pnlContainerSites.Visible = false;

            pnlFicheEmployee.Visible = false;
            pnlFicheService.Visible = false;
            pnlFicheSite.Visible = false;

            lbBtnEmployee.ForeColor = Color.White;
            lbBtnServices.ForeColor = Color.White;
            lbBtnSites.ForeColor = Color.White;
        }

        private void lbBtnEmployee_Click(object sender, EventArgs e)
        {
            if (currentTab == "employee")
            {
                return;
            }

            ResetTabData();
            ResetFicheEmployee();

            currentTab = "employee";
            pnlContainerEmployees.Visible = true;
            pnlFicheEmployee.Visible = true;
            lbBtnEmployee.ForeColor = Color.FromArgb(253, 238, 0);

            LoadFilteredEmployees();
            pnlMain.Select();
        }

        private void lbBtnServices_Click(object sender, EventArgs e)
        {
            if (currentTab == "service")
            {
                return;
            }

            ResetTabData();
            ResetFicheService();

            currentTab = "service";
            pnlContainerServices.Visible = true;
            pnlFicheService.Visible = true;
            lbBtnServices.ForeColor = Color.FromArgb(253, 238, 0);

            LoadServices();
            pnlMain.Select();
        }

        private void lbBtnSites_Click(object sender, EventArgs e)
        {
            if (currentTab == "site")
            {
                return;
            }

            ResetTabData();
            ResetFicheSite();

            currentTab = "site";
            pnlContainerSites.Visible = true;
            pnlFicheSite.Visible = true;
            lbBtnSites.ForeColor = Color.FromArgb(253, 238, 0);

            LoadSites();
            pnlMain.Select();
        }

        #endregion

        #region Mise à blanc données fiche

        private void ResetFicheEmployee()
        {
            rowIndex = -1;
            lbEnteteFicheEmploye.Text = "";
            tbPrenomEmployee.Text = "";
            tbNomEmployee.Text = "";
            tbHomePhone.Text = "";
            tbMobilePhone.Text = "";
            tbEmail.Text = "";
            cbServiceEmployee.SelectedIndex = 0;
            cbSiteEmployee.SelectedIndex = 0;
        }

        private void ResetFicheService()
        {
            rowIndex = -1;
            tbServiceNom.Text = "";
            lbServiceEnteteFiche.Text = "";
        }

        private void ResetFicheSite()
        {
            rowIndex = -1;
            tbVilleSite.Text = "";
            lbSiteEnteteFiche.Text = "";
        }

        #endregion

        #region Evenements boutons Ajouter

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            addRow = true;
            ResetFicheEmployee();
            lbEnteteFicheEmploye.Text = "Ajout d'un employé";
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            addRow = true;
            ResetFicheService();
            lbServiceEnteteFiche.Text = "Ajout d'un service";
        }

        private void btnAddSite_Click(object sender, EventArgs e)
        {
            addRow = true;
            ResetFicheSite();
            lbSiteEnteteFiche.Text = "Ajout d'un site";
        }

        #endregion

        #region Evenements boutons Enregistrer

        private void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            if (!addRow && rowIndex == -1)
            {
                MessageBox.Show("Sélectionnez d'abord un employé.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Employee employee = new Employee()
            {
                IDEmployee = addRow ? -1 : Convert.ToInt32(dgvEmployees["IDEmployee", rowIndex].Value),
                Firstname = tbPrenomEmployee.Text,
                Lastname = tbNomEmployee.Text,
                HomePhone = tbHomePhone.Text,
                MobilePhone = tbMobilePhone.Text,
                Email = tbEmail.Text,
                IDSite = idSites[cbSiteEmployee.SelectedIndex],
                IDService = idServices[cbServiceEmployee.SelectedIndex]
            };

            if (employee.IDService == -1
                || employee.IDSite == -1)
            {
                MessageBox.Show("Certaines données sont invalides.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (addRow)
            {
                if (APIRequest.Post(employee))
                {
                    MessageBox.Show("Nouvel employé créé.", "Création d'un nouvel employé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addRow = false;
                    ResetFicheEmployee();
                    LoadFilteredEmployees();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création d'un nouvel employé ! Vérifiez la validité des données.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (APIRequest.Put(employee))
                {
                    MessageBox.Show("Les informations de l'employé ont été enregistrées.", "Modification d'un employé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFilteredEmployees();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'enregistrement des données ! Vérifiez leur validité.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveService_Click(object sender, EventArgs e)
        {
            if (!addRow && rowIndex == -1)
            {
                MessageBox.Show("Sélectionnez d'abord un service.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Service service = new Service()
            {
                IDService = addRow ? -1 : Convert.ToInt32(dgvServices["IDService", rowIndex].Value),
                Name = tbServiceNom.Text
            };

            if (string.IsNullOrEmpty(service.Name))
            {
                MessageBox.Show("Entrez un nom de service", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (addRow)
            {
                if (APIRequest.Post(service))
                {
                    MessageBox.Show("Nouveau service créé.", "Création d'un nouveau service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addRow = false;
                    ResetFicheService();
                    LoadServices();
                    LoadServiceComboBoxes();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création d'un nouveau service ! Vérifiez la validité des données.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (APIRequest.Put(service))
                {
                    MessageBox.Show("Les informations du service ont été enregistrées.", "Modification d'un service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadServices();
                    LoadServiceComboBoxes();
                    lbServiceEnteteFiche.Text = service.Name;
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'enregistrement des données ! Vérifiez leur validité.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveSite_Click(object sender, EventArgs e)
        {
            if (!addRow && rowIndex == -1)
            {
                MessageBox.Show("Sélectionnez d'abord un site.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Site site = new Site()
            {
                IDSite = addRow ? -1 : Convert.ToInt32(dgvSites["IDSite", rowIndex].Value),
                City = tbVilleSite.Text
            };

            if (string.IsNullOrEmpty(site.City))
            {
                MessageBox.Show("Entrez un nom de site", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (addRow)
            {
                if (APIRequest.Post(site))
                {
                    MessageBox.Show("Nouveau site créé.", "Création d'un nouveau site", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addRow = false;
                    ResetFicheSite();
                    LoadSites();
                    LoadSiteComboBoxes();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création d'un nouveau site ! Vérifiez la validité des données.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (APIRequest.Put(site))
                {
                    MessageBox.Show("Les informations du site ont été enregistrées.", "Modification d'un site", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSites();
                    LoadSiteComboBoxes();
                    lbSiteEnteteFiche.Text = site.City;
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'enregistrement des données ! Vérifiez leur validité.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Evenements boutons Supprimer

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (rowIndex == -1)
            {
                MessageBox.Show("Sélectionnez d'abord un employé.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idEmployee = Convert.ToInt32(dgvEmployees["IDEmployee", rowIndex].Value);

            if (APIRequest.DeleteEmployee(idEmployee))
            {
                MessageBox.Show("Suppression réussie.", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetFicheEmployee();
                LoadFilteredEmployees();
            }
            else
            {
                MessageBox.Show("Echec de la suppression.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (rowIndex == -1)
            {
                MessageBox.Show("Sélectionnez d'abord un service.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idService = Convert.ToInt32(dgvServices["IDService", rowIndex].Value);

            if (APIRequest.DeleteService(idService))
            {
                MessageBox.Show("Suppression réussie.", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetFicheService();
                LoadServices();
                LoadServiceComboBoxes();
            }
            else
            {
                MessageBox.Show("Impossible de supprimer ce service. Vérifiez qu'il ne soit utilisé nulle part.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteSite_Click(object sender, EventArgs e)
        {
            if (rowIndex == -1)
            {
                MessageBox.Show("Sélectionnez d'abord un site.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idSite = Convert.ToInt32(dgvSites["IDSite", rowIndex].Value);

            if (APIRequest.DeleteSite(idSite))
            {
                MessageBox.Show("Suppression réussie.", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetFicheSite();
                LoadSites();
                LoadSiteComboBoxes();
            }
            else
            {
                MessageBox.Show("Impossible de supprimer ce site. Vérifiez qu'il ne soit utilisé nulle part.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } 

        #endregion
    }
}