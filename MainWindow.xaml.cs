using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using biblioBDDpersonel;
using BddpersonnelContext;

namespace appliBDDpersonel
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private C_BDD_personnel bddPersonnels = null;

        public object Gestionnaire { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            try {

                bddPersonnels = new C_BDD_personnel();
                List<Service> services = bddPersonnels.getAllServices();
                datagridService.ItemsSource = services;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Nom_Selected(object sender, RoutedEventArgs e)
        {
            TBNom.Visibility = Visibility;
        }

        private void Prénom_Selected(object sender, RoutedEventArgs e)
        {
            TBPrénom.Visibility = Visibility;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (datagridService.SelectedItem != null)
            {
                Service selectedService = (Service)datagridService.SelectedItem;
                FenetreAjoutServiceOuFonction fenetreAjout = new FenetreAjoutServiceOuFonction(selectedService);
                if (fenetreAjout.ShowDialog() == true)
                {
                    string nom = fenetreAjout.Nom;
                    string description = fenetreAjout.Description;
                    bool isService = fenetreAjout.IsService;

                    if (isService)
                    {
                        Service newService = new Service { Nom = nom, Description = description };
                        bddPersonnels.AddService(newService);
                    }
                    else
                    {
                        Fonction newFonction = new Fonction { Nom = nom, Description = description, Service = selectedService };
                        bddPersonnels.AddFonction(newFonction);
                    }

                    bddPersonnels.SaveChanges();

                    RefreshData();
                }
            }
        }

        private void RefreshData()
        {
            throw new NotImplementedException();
        }
    }

}   